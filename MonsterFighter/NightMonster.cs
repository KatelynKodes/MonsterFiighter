using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MonsterFighter
{
    class NightMonster: Monster
    {
        //Properties
        public override string GetName => name;
        public override float GetHealth => health;
        public override float GetMaxHealth => maxHealth;
        public override float GetAttackPwr => attackpower;
        public override float GetDefensePwr => defensepower;
        public override type GetMonsterType => MonsterType;

        //Constructor
        public NightMonster(string monstrname, float hp, float attk, float def)
        {
            name = monstrname;
            health = hp;
            maxHealth = health;
            attackpower = attk;
            defensepower = def;
            MonsterType = type.NIGHT;
        }

        public override bool GetAdvantage(Monster Opponent)
        {
            advantage = false;
            if (Opponent.GetMonsterType == type.DAY || Opponent.GetMonsterType == type.DUSK)
            {
                advantage = true;
            }

            return advantage;
        }

        public override void WritePetStats()
        {
            Console.WriteLine("Name:" + GetName);
            Console.WriteLine("Type: Night");
            Console.WriteLine("HP:" + GetHealth +  "/" + GetMaxHealth);
            Console.WriteLine("Attack:" + GetAttackPwr);
            Console.WriteLine("Defense:" + GetDefensePwr);
        }

        public override void Save(StreamWriter writer)
        {
            base.Save(writer);
            writer.WriteLine(name);
            writer.WriteLine(health);
            writer.WriteLine(maxHealth);
            writer.WriteLine(attackpower);
            writer.WriteLine(defensepower);
        }

        public bool Load(StreamReader reader)
        {
            bool success = true;
            if (reader.ReadLine() != name)
            {
                return success = false;
            }
            if (!float.TryParse(reader.ReadLine(), out health))
            {
                return success = false;
            }
            if (!float.TryParse(reader.ReadLine(), out maxHealth))
            {
                return success = false;
            }
            if (!float.TryParse(reader.ReadLine(), out attackpower))
            {
                return success = false;
            }
            if (!float.TryParse(reader.ReadLine(), out defensepower))
            {
                return success = false;
            }

            return success;
        }
    }
}
