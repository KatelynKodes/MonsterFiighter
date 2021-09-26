using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    abstract class Monster
    {
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
        protected float attackpower;
        protected float defensepower;
        protected bool advantage;

        //Properties so that members outside of the inherited classes
        //can read the variables, but not change them
        public abstract string GetName { get; }
        public abstract float GetHealth { get; }
        public abstract float GetAttackPwr { get; }
        public abstract float GetDefensePwr { get; }
        public abstract type GetMonsterType { get; }

        //Methods
        public abstract float DoDamage(Monster attackingMonster, Monster DefendingMonster);
        public abstract bool GetAdvantage(Monster Opponent);
        public abstract void WritePetStats();
        public abstract void DecreaseHealth(float decreasevar);
        public abstract void IncreaseHealth(float increaseVar);
    }
}
