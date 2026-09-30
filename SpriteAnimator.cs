using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ElementalGUI
{
    internal static class SpriteAnimator
    {
        // one Anim per image keeps matching sprites in sync
        private sealed class Anim
        {
            public Image Image = null!;
            public int FrameCount;
            public int FrameIndex;
            public int SlowEvery;
            public readonly List<PictureBox> Boxes = new();
        }

        private static readonly Dictionary<Image, Anim> animations = new();
        private static readonly Dictionary<PictureBox, (Anim Anim, PaintEventHandler Handler)> bindings = new();

        // attach a sprite; static images stay plain
        public static void SetSprite(PictureBox box, Image? image)
        {
            Untrack(box);

            if (image == null)
            {
                return;
            }

            int frames;
            try
            {
                frames = image.GetFrameCount(FrameDimension.Time);
            }
            catch
            {
                box.Image = image;
                return;
            }

            if (frames <= 1)
            {
                box.Image = image;
                return;
            }

            box.Image = null;

            if (!animations.TryGetValue(image, out Anim? anim))
            {
                anim = new Anim
                {
                    Image = image,
                    FrameCount = frames,
                    SlowEvery = frames > 90 ? 2 : 1
                };
                animations[image] = anim;
            }

            PaintEventHandler handler = (s, e) => PaintSprite(box, anim, e);
            bindings[box] = (anim, handler);
            box.Paint += handler;
            anim.Boxes.Add(box);

            box.Invalidate();
        }

        public static void Untrack(PictureBox box)
        {
            if (bindings.TryGetValue(box, out var binding))
            {
                box.Paint -= binding.Handler;
                binding.Anim.Boxes.Remove(box);
                bindings.Remove(box);
            }
        }

        public static void Tick()
        {
            List<Image>? dead = null;

            foreach (var pair in animations)
            {
                Anim anim = pair.Value;

                anim.Boxes.RemoveAll(b => b.IsDisposed);

                // drop animations nobody displays anymore
                if (anim.Boxes.Count == 0)
                {
                    (dead ??= new List<Image>()).Add(pair.Key);
                    continue;
                }

                int next = anim.FrameIndex + 1;

                if (anim.SlowEvery > 1 && next % anim.SlowEvery != 0)
                {
                    continue;
                }

                if (next >= anim.FrameCount)
                {
                    next = 0;
                }

                anim.FrameIndex = next;

                try
                {
                    anim.Image.SelectActiveFrame(FrameDimension.Time, next);
                }
                catch
                {
                    continue;
                }

                foreach (PictureBox box in anim.Boxes)
                {
                    box.Invalidate();
                }
            }

            if (dead != null)
            {
                foreach (Image image in dead)
                {
                    animations.Remove(image);
                }
            }
        }

        // zoom-fit the current frame
        private static void PaintSprite(PictureBox box, Anim anim, PaintEventArgs e)
        {
            if (box.IsDisposed)
            {
                return;
            }

            var g = e.Graphics;

            float scale = Math.Min(
                box.ClientSize.Width / (float)anim.Image.Width,
                box.ClientSize.Height / (float)anim.Image.Height);

            int w = Math.Max(1, (int)(anim.Image.Width * scale));
            int h = Math.Max(1, (int)(anim.Image.Height * scale));

            var dest = new Rectangle(
                (box.ClientSize.Width - w) / 2,
                (box.ClientSize.Height - h) / 2,
                w,
                h);

            g.DrawImage(anim.Image, dest);
        }
    }
}
