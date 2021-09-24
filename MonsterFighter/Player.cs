using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    class Player
    {
        private string _name;
        private Monster[] _team;
        private int _monsterTeamLength;

        public Player()
        {
            _name = "Priest";
            _team = new Monster[0];
            _monsterTeamLength = _team.Length;
        }

        public void AddToTeam( Monster MonsterToAdd)
        {
            if (_team.Length != 0)
            {
                Monster[] newTeam = new Monster[_team.Length + 1];
                for (int i = 0; i < _team.Length; i++)
                {
                    newTeam[i] = _team[i];
                }
            }
            else
            {
                _team[0] = MonsterToAdd;
            }
        }

    }
}

