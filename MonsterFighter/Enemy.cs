using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    class Enemy:Priest
    {
        //Properties
        public override string GetName => _name;
        public override Monster[] GetTeam => _team;
        public override int GetCurrMonIndex => _currMonsterIndex;

        // Constructor
        public Enemy(string name, Monster[] Team)
        {
            _name = name;
            _team = Team;
        }
    }
}
