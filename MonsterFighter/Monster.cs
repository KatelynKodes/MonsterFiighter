using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    abstract class Monster
    {
        public enum MonsterType
        {
            DAY, 
            NIGHT,
            DUSK,
            DAWN
        }

        private float _health;
        private float _attack;
        private float _defense;
        private MonsterType _type;
        private bool _Advantage;

        public abstract MonsterType GetMonsterType { get;}
        public abstract bool GetAdvantage();

        public abstract void Fight();
        
    }
}
