using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MonsterFighter
{
    abstract class Monster
    {
        //The types a monster can have
        public enum type
        {
            DAY,
            NIGHT,
            DUSK,
            DAWN
        }

        //Variables all classes will need, marked as protected so that only the
        //inherited classes can change them
        protected string name;
        protected type MonsterType;
        protected float health;
        protected float maxHealth;
        protected float attackpower;
        protected float defensepower;
        protected bool advantage;

        //Properties so that members outside of the inherited classes
        //can read the variables, but not change them
        public abstract string GetName { get; }
        public abstract float GetHealth { get; }
        public abstract float GetMaxHealth { get; }
        public abstract float GetAttackPwr { get; }
        public abstract float GetDefensePwr { get; }
        public abstract type GetMonsterType { get; }

        //Abstract Methods
        public abstract bool GetAdvantage(Monster Opponent);
        public abstract void WritePetStats();


        //Virtual Methods 
        public virtual void DecreaseHealth(float decreasevar)
        {
            health -= decreasevar;
        }
        public virtual void IncreaseHealth(float increaseVar)
        {
            health -= increaseVar;
        }
        public virtual float DoDamage(Monster DefendingMonster)
        {
            float damagedealt = GetAttackPwr - DefendingMonster.GetDefensePwr;

            //Checks advantage against the defender
            if (GetAdvantage(DefendingMonster))
            {
                damagedealt += 5;
            }

            //Checks if the damage dealt is <= 0;
            //Even with advantage
            if (damagedealt <= 0)
            {
                damagedealt = 0;
            }

            return damagedealt;
        }

        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(name);
            writer.WriteLine(MonsterType);
            writer.WriteLine(health);
            writer.WriteLine(maxHealth);
            writer.WriteLine(attackpower);
            writer.WriteLine(defensepower);
        }

        public virtual bool Load(StreamReader reader)
        {
            bool success = true;
            if (reader.ReadLine() != name)
            {
                return success = false;
            }
            if (reader.ReadLine() != MonsterType.ToString())
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

            reader.Close();
            return success;
        }
    }
}
