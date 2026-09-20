using System;
using System.Collections.Generic;
using System.Text;


        namespace ElementalGUI
    {
        public class Character
        {
            private string name;
            private string element;
            private int health;
            private int attackPower;
            private int specialUses;
            private bool isDefending;

            public Character(string name, string element, int attackPower)
            {
                this.name = name;
                this.element = element;
                this.health = 100;
                this.attackPower = attackPower;
                this.specialUses = 3;
                this.isDefending = false;
            }

            public string GetName()
            {
                return name;
            }

            public string GetElement()
            {
                return element;
            }

            public int GetHealth()
            {
                return health;
            }

            public int BasicAttack()
            {
                return attackPower;
            }

            public int SpecialAttack()
            {
                if (specialUses > 0)
                {
                    specialUses--;
                    return attackPower * 2;
                }

                return 0;
            }

            public void Defend()
            {
                isDefending = true;
            }

            public void TakeDamage(int damage)
            {
                if (isDefending)
                {
                    damage = damage / 2;
                    isDefending = false;
                }

                health -= damage;

                if (health < 0)
                {
                    health = 0;
                }
            }

            public bool IsAlive()
            {
                return health > 0;
            }

            public int GetSpecialUses()
            {
                return specialUses;
            }

            public bool IsDefending()
            {
                return isDefending;
            }
        }
    }
