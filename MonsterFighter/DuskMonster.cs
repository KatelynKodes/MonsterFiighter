using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    class DuskMonster : Monster
    {
        // Properties 
        public override string GetName => name;
        public override float GetHealth => health;
        public override float GetMaxHealth => maxHealth;
        public override float GetAttackPwr => attackpower;
        public override float GetDefensePwr => defensepower;
        public override type GetMonsterType => MonsterType;

        // Constructor
        public DuskMonster(string monsterName, float hp, float attk, float def)
        {
            name = monsterName;
            health = hp;
            maxHealth = health;
            attackpower = attk;
            defensepower = def;
            MonsterType = type.DAWN;
        }

        public override bool GetAdvantage(Monster Opponent)
        {
            advantage = false;
            if (Opponent.GetMonsterType == type.DAY || Opponent.GetMonsterType == type.DAWN)
            {
                advantage = true;
            }

            return advantage;
        }

        public override void WritePetStats()
        {
            Console.WriteLine("Name:" + GetName);
            Console.WriteLine("Type: Dusk");
            Console.WriteLine("HP:" + GetHealth + "/" + GetMaxHealth);
            Console.WriteLine("Attack:" + GetAttackPwr);
            Console.WriteLine("Defense:" + GetDefensePwr);
        }
    }
}
