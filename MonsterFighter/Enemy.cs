using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    class Enemy
    {
        private string _Enemyname;
        private Monster[] _EnemyTeam;
        private int _currentMonsterIndex;

        public Monster[] _GetEnemyTeam 
        {
            get
            {
                return _EnemyTeam;
            }
        }

        public string GetEnemyName
        {
            get { return _Enemyname; }
        }

        public int GetCurrentMonsterIndex
        {
            get
            {
                return _currentMonsterIndex;
            }
        }


        public Enemy(string name, Monster[] Team)
        {
            _Enemyname = name;
            _EnemyTeam = Team;
        }

        /// <summary>
        /// Checks if you can increase the current monster index by passing in a bool
        /// to see if increasing is NOT possible.
        /// </summary>
        /// <param name="cantIncrease"> The bool the method must check if the index cant be increased</param>
        /// <returns></returns>
        public int IncreaseEnemyMonsterIndex(bool cantIncrease)
        {
            if (!cantIncrease)
            {
                _currentMonsterIndex++;
            }

            return _currentMonsterIndex;
        }

        /// <summary>
        /// Checks if the current monster index is too large
        /// </summary>
        /// <returns></returns>
        public bool IndexTooLarge()
        {
            bool checkindex = _currentMonsterIndex >= this._GetEnemyTeam.Length;

            return checkindex;
        }
    }
}
