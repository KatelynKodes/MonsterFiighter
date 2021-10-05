using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

        /// <summary>
        /// Checks the monsters advantage over an opponent
        /// </summary>
        /// <param name="Opponent"> The opposing monster </param>
        /// <returns></returns>
        public override bool GetAdvantage(Monster Opponent)
        {
            advantage = false;
            if (Opponent.GetMonsterType == type.NIGHT || Opponent.GetMonsterType == type.DUSK)
            {
                advantage = true;
            }

            return advantage;
        }

        /// <summary>
        /// Writes Monster stats to a text file.
        /// </summary>
        /// <param name="writer"> The writer used to write to the text file</param>
        public override void Save(StreamWriter writer)
        {
            base.Save(writer);
            writer.WriteLine(name);
            writer.WriteLine(health);
            writer.WriteLine(maxHealth);
            writer.WriteLine(attackpower);
            writer.WriteLine(defensepower);
        }

        /// <summary>
        /// Reads Monster stats from a text file.
        /// </summary>
        /// <param name="reader"> The reader used to read from the text file</param>
        public bool Load(StreamReader reader)
        {
            bool success = true;
            name = reader.ReadLine();
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
