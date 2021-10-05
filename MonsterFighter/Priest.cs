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

        /// <summary>
        /// Sets the Current Monster index to a number
        /// </summary>
        /// <param name="numtoSet"> number the current monster index is set to</param>
        public void SetCurrentIndex(int numtoSet)
        {
            _currMonsterIndex = numtoSet;
        }

        /// <summary>
        /// Writes the priests data to a txt file using a writer
        /// </summary>
        /// <param name="writer"> The writer beinng used to write to the text file.s</param>
        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_currMonsterIndex);
        }

        /// <summary>
        /// Reads the priests data from a text file using a reader
        /// changes the values to match the values read in the .txt file
        /// </summary>
        /// <param name="reader"> The reader used to read the text file</param>
        /// <returns></returns>
        public virtual bool Load(StreamReader reader)
        {
            bool loadSuccess = true;
            _name = reader.ReadLine();

            if (!int.TryParse(reader.ReadLine(), out _currMonsterIndex))
            {
                return loadSuccess = false;
            }
            return loadSuccess;
        }
    }
}
