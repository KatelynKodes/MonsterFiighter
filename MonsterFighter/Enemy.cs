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
        private Monster _currentMonster;

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

        public void IncreaseEnemyMonsterIndex(bool canIncrease)
        {
            if (canIncrease)
            {
                _currentMonsterIndex++;
            }
        }

        public bool CanIncreaseEnemyIndex()
        {
            bool checkindex = _currentMonsterIndex >= _EnemyTeam.Length;

            return checkindex;
        }
    }
}
