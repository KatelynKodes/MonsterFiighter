using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    class DawnMonster: Monster
    {
        // Properties 
        public override string GetName => name;
        public override float GetHealth => health;
        public override float GetMaxHealth => maxHealth;
        public override float GetAttackPwr => attackpower;
        public override float GetDefensePwr => defensepower;
        public override type GetMonsterType => MonsterType;

        // Constructor
        public DawnMonster(string monsterName, float hp, float attk, float def)
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
            if (Opponent.GetMonsterType == type.NIGHT || Opponent.GetMonsterType == type.DUSK)
            {
                advantage = true;
            }

            return advantage;
        }

        public override void WritePetStats()
        {
            Console.Clear();
            Console.WriteLine("Name:" + GetName);
            Console.WriteLine("Type: Dawn");
            Console.WriteLine("HP:" + GetHealth + "/" + GetMaxHealth);
            Console.WriteLine("Attack:" + GetAttackPwr);
            Console.WriteLine("Defense:" + GetDefensePwr);
            Console.ReadKey(true);
        }
    }
}
