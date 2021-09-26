using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    class DayMonster : Monster
    {
        public override string GetName => name;
        public override float GetHealth => health;
        public override type GetMonsterType => MonsterType;
        public override float GetAttackPwr => attackpower;
        public override float GetDefensePwr => defensepower;

        public DayMonster(string monstrname, float hp, float attk, float def)
        {
            name = monstrname;
            health = hp;
            attackpower = attk;
            defensepower = def;
            MonsterType = type.DAY;
        }

        /// <summary>
        /// Returns a float based on how much damage the attacking monster does to
        /// the defending monster
        /// </summary>
        /// <param name="Attacker"> The attacking monster </param>
        /// <param name="Defender"> The Defending Monster</param>
        /// <returns></returns>
        public override float DoDamage(Monster Attacker, Monster Defender)
        {
            float damagedealt = Attacker.GetAttackPwr - Defender.GetDefensePwr;
            if (damagedealt <= 0)
            {
                damagedealt = 0;
            }
            else
            {
                //Checks advantage against the defender
                if (GetAdvantage(Defender))
                {
                    damagedealt += 5;
                }
            }

            return damagedealt;
        }

        /// <summary>
        /// Increases the health of a monster by a certain variable
        /// </summary>
        /// <param name="increaseNum"> Number the variable is increased by</param>
        public override void IncreaseHealth(float increaseNum)
        {
            health += increaseNum;
        }

        /// <summary>
        /// Decreases the health of the monster by a certain variable
        /// </summary>
        /// <param name="decreasevar"> Number the variable is decreased by</param>
        public override void DecreaseHealth(float decreasevar)
        {
            health -= decreasevar;
        }

        /// <summary>
        /// Checks if the monster has an advantage over it's opponent
        /// </summary>
        /// <param name="Opponent"></param>
        /// <returns></returns>
        public override bool GetAdvantage(Monster Opponent)
        {
            if (Opponent.GetMonsterType == type.NIGHT || Opponent.GetMonsterType == type.DAWN)
            {
                advantage = true;
            }
            else
            {
                advantage = false;
            }

            return advantage;
        }

        /// <summary>
        /// Writes the stats of the pet out in the console.
        /// </summary>
        public override void WritePetStats()
        {
            Console.Clear();
            Console.WriteLine("Name:" + GetName);
            Console.WriteLine("Type: Day");
            Console.WriteLine("HP:" + GetHealth);
            Console.WriteLine("Attack:" + GetAttackPwr);
            Console.WriteLine("Defense:" + GetDefensePwr);
            Console.ReadKey(true);
        }
    }
}
