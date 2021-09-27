using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    class DayMonster : Monster
    {
        public override string GetName => name;
        public override float GetHealth => health;
        public override float GetMaxHealth => maxHealth;
        public override type GetMonsterType => MonsterType;
        public override float GetAttackPwr => attackpower;
        public override float GetDefensePwr => defensepower;

        public DayMonster(string monstrname, float hp, float attk, float def)
        {
            name = monstrname;
            health = hp;
            maxHealth = hp;
            attackpower = attk;
            defensepower = def;
            MonsterType = type.DAY;
        }

        /// <summary>
        /// Checks if the monster has an advantage over it's opponent
        /// </summary>
        /// <param name="Opponent"></param>
        /// <returns></returns>
        public override bool GetAdvantage(Monster Opponent)
        {
            advantage = false;
            if (Opponent.GetMonsterType == type.NIGHT || Opponent.GetMonsterType == type.DAWN)
            {
                advantage = true;
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
            Console.WriteLine("HP:" + GetHealth + "/" + GetMaxHealth);
            Console.WriteLine("Attack:" + GetAttackPwr);
            Console.WriteLine("Defense:" + GetDefensePwr);
            Console.ReadKey(true);
        }
    }
}
