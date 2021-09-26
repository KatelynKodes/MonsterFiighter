﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    class NightMonster: Monster
    {
        //Properties
        public override string GetName => name;
        public override float GetHealth => health;
        public override float GetAttackPwr => attackpower;
        public override float GetDefensePwr => defensepower;
        public override type GetMonsterType => MonsterType;

        public NightMonster(string monstrname, float hp, float attk, float def)
        {
            name = monstrname;
            health = hp;
            attackpower = attk;
            defensepower = def;
            MonsterType = type.NIGHT;
        }

        public override float DoDamage(Monster attackingMonster, Monster DefendingMonster)
        {
            
        }

        public override bool GetAdvantage(Monster Opponent)
        {
            if (Opponent.GetMonsterType == type.DAY || Opponent.GetMonsterType == type.DUSK)
            {
                advantage = true;
            }
            else
            {
                advantage = false;
            }

            return advantage;
        }

        public override void WritePetStats()
        {
            Console.Clear();
            Console.WriteLine("Name:" + GetName);
            Console.WriteLine("Type: Night");
            Console.WriteLine("HP:" + GetHealth);
            Console.WriteLine("Attack:" + GetAttackPwr);
            Console.WriteLine("Defense:" + GetDefensePwr);
            Console.ReadKey(true);
        }

        public override void DecreaseHealth(float decreasevar)
        {
            health -= decreasevar;
        }

        public override void IncreaseHealth(float increaseVar)
        {
            throw new NotImplementedException();
        }
    }
}