using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MonsterFighter
{
    abstract class Priest
    {
        //Protected vars;
        protected string _name;
        protected Monster[] _team;
        protected int _currMonsterIndex = 0;

        //Properties
        public abstract string GetName { get; }
        public abstract Monster[] GetTeam {get;}

        public abstract int GetCurrMonIndex { get;  }

        /// <summary>
        /// Checks if you can increase the current monster index by passing in a bool
        /// to see if increasing is NOT possible.
        /// </summary>
        /// <param name="cantIncrease"> The bool the method must check if the index cant be increased</param>
        /// <returns></returns>
        public virtual int IncreaseCurrMonsterIndex(bool cantIncrease)
        {
            if (!cantIncrease)
            {
                _currMonsterIndex++;
            }

            return _currMonsterIndex;
        }

        /// <summary>
        /// Checks if the current monster index is too large
        /// </summary>
        /// <returns></returns>
        public virtual bool IndexTooLarge()
        {
            bool checkIndex = _currMonsterIndex >= this._team.Length;

            return checkIndex;
        }

        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_name);
            for (int i = 0; i < _team.Length; i++)
            {
                _team[i].Save(writer);
            }
            writer.WriteLine(_currMonsterIndex);
        }

        public virtual bool Load(StreamReader reader)
        {
            bool loadSuccess = true;
            if (reader.ReadLine() != _name)
            {
                return loadSuccess = false;
            }

            for (int i = 0; i < _team.Length; i++)
            {
                if (!_team[i].Load(reader))
                {
                    return loadSuccess = false;
                }
            }

            if (!int.TryParse(reader.ReadLine(), out _currMonsterIndex))
            {
                return loadSuccess = false;
            }
            reader.Close();
            return loadSuccess;
        }
    }
}
