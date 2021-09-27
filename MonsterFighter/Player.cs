using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    class Player
    {
        //Private Vars
        private string _name;
        private Monster[] _team;
        private int _monsterTeamLength;
        private int _currentMonsterIndex;

        //Properties
        public string GetName
        {
            get
            {
                return _name;
            }
        }
        public Monster[] GetTeam
        {
            get
            { return _team; }
        }

        public int GetCurrentMonstrIndex
        {
            get
            { return _currentMonsterIndex; }
        }

        public Player()
        {
            _name = "Young Seminary";
            _team = new Monster[0];
            _monsterTeamLength = _team.Length;
        }

        /// <summary>
        /// Changes the players name
        /// </summary>
        /// <param name="NewName"> Name the player changes to</param>
        public void ChangeName(string NewName)
        {
            _name = NewName;
        }

        /// <summary>
        /// Adds a monster to the players team
        /// </summary>
        /// <param name="MonsterToAdd"></param>
        public void AddToTeam(Monster MonsterToAdd)
        {
            Monster[] newTeam = new Monster[_team.Length + 1];
            for (int i = 0; i < _team.Length; i++)
            {
                newTeam[i] = _team[i];
            }
            newTeam[newTeam.Length - 1] = MonsterToAdd;
            _team = newTeam;
            _monsterTeamLength = _team.Length;
        }

        public void IncreaseCurrentMonIndex(bool CanIncrease)
        {
            if (CanIncrease)
            {
                _currentMonsterIndex++;
            }
        }

    }
}