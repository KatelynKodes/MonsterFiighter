using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MonsterFighter
{
    class Player : Priest
    {
        //private variables
        private int _monsterTeamLength;

        //Properties
        public override string GetName => _name;
        public override Monster[] GetTeam => _team;
        public override int GetCurrMonIndex => _currMonsterIndex;


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

        public override void Save(StreamWriter writer)
        {
            base.Save(writer);
            writer.WriteLine(_monsterTeamLength);
        }

        public override bool Load(StreamReader reader)
        {
            bool success = true;
            if (!base.Load(reader))
            {
                return success = false;
            }
            if (!int.TryParse(reader.ReadLine(), out _monsterTeamLength))
            {
                return success = false;
            }
            reader.Close();
            return success;
        }
    }
}