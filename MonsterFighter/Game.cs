using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MonsterFighter
{
    public enum Scene
    {
        MAINMENU,
        CHOOSESTARTERPET,
        DAWNDISTRICT,
        DAWNBATTLE,
        DAWNDUSKSELECT,
        REPLAYMENU
    }


    class Game
    {
        private Scene _currentScene;
        private bool _gameOver;
        private Player _player;
        bool ExitDistrictMenu;
        private Enemy _currentEnemy;
        private Monster _currentEnemyMonster;
        private Monster _currentPlayerMonster;

        //Enemies
        private Enemy _dawnEnemy;

        //Monsters for player
        private DayMonster _brightling;
        private NightMonster _moonHush;
        private DawnMonster _SunnySide;
        private DuskMonster _Dopey;

        //EnemyMonsters
        //Dawns Team
        private Monster[] _dawnMonsters;
        private DuskMonster _whispurn;
        private DuskMonster _Snoozem;
        private DawnMonster _EarlyBird;

        public void Run()
        {
            Start();
            while (!_gameOver)
            {
                Update();
            }
            End();
        }

        void Start()
        {
            _gameOver = false;
            _currentScene = Scene.MAINMENU;
            _player = new Player();
            InitializePets();
            InitializeEnemies();
        }

        void Update()
        {
            DisplayCurrentScene();
        }

        void End()
        {
            Console.WriteLine("You have ended the demo, please close the console window");
        }

        public void Save()
        {
            StreamWriter writer = new StreamWriter("DawnOfNightSaveData.txt");
            _player.Save(writer);
            writer.WriteLine(_currentScene);
            writer.Close();
        }

        public bool Load()
        {
            StreamReader reader = new StreamReader("DawnOfNightSaveData.txt");
            bool LoadSuccess = true;
            if(!_player.Load(reader))
            {
                return LoadSuccess = false;
            }
            if (reader.ReadLine() != _currentScene.ToString())
            {
                return LoadSuccess = false;
            }
            reader.Close();
            return LoadSuccess;
        }

        void InitializePets()
        {
            //Starters
            _brightling = new DayMonster("Brightling", 300, 30, 40);
            _moonHush = new NightMonster("Moonhush", 200, 50, 30);
            _Dopey = new DuskMonster("Dopey", 200, 30, 40);
            _SunnySide = new DawnMonster("SunnySide", 400, 20, 30);

            //Enemy monsters
            //Dawn Team
            _whispurn = new DuskMonster("Whispurn", 300, 30, 20);
            _Snoozem = new DuskMonster("Snoozem", 200, 30, 40);
            _EarlyBird = new DawnMonster("EarlyBird", 400, 20, 30);
            _dawnMonsters = new Monster[] { _whispurn, _Snoozem, _EarlyBird };
            
        }

        void InitializeEnemies()
        {
            _dawnEnemy = new Enemy("Dawn", _dawnMonsters);
        }


        /// <summary>
        /// Asks the player a quetion and allows game to recieve input from the player 
        /// based on a set of options it provides
        /// </summary>
        /// <param name="desc"> The prompt the player must answer</param>
        /// <param name="options"> The select amount of options a player must choose from.</param>
        /// <returns></returns>
        public int GetInput(string desc, params string[] options)
        {
            int InputRecieved = -1;
            string PlayerInput;

            //Checks to see if the input the player selected is a valid input
            while (InputRecieved == -1)
            {
                Console.Clear();
                //Writes out prompt and options
                Console.WriteLine(desc);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine("[" + (i + 1) + "] " + options[i]);
                }
                Console.Write(">");
                PlayerInput = Console.ReadLine();

                //Checks to see if input is a number
                bool NumInput = int.TryParse(PlayerInput, out InputRecieved);
                if (NumInput)
                {
                    //If it is a number, checks to see if its less than zero or greater than the length of options
                    if (InputRecieved < 0 || InputRecieved > options.Length)
                    {
                        InputRecieved = -1;
                    }
                }
                else
                {
                    //If it's not a number, compares input to every option in the array to see if the lowercase input is
                    //equal to the lowercase options value
                    for (int i = 0; i < options.Length; i++)
                    {
                        if (PlayerInput.ToLower() == options[i].ToLower())
                        {
                            InputRecieved = i + 1;
                            break;
                        }
                        else
                        {
                            InputRecieved = -1;
                        }
                    }

                    //Checks to see if inputrecieved is still -1 after loop to give player feedback
                    if (InputRecieved == -1)
                    {
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
            }

            return InputRecieved;
        }

        /// <summary>
        /// Displays the current scene the player should be on based on the variable _currentscene
        /// </summary>
        void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case Scene.MAINMENU:
                    DisplayMainMenu();
                    break;
                case Scene.CHOOSESTARTERPET:
                    ChoosePetScene();
                    break;
                case Scene.DAWNDISTRICT:
                    District1Scene();
                    break;
                case Scene.DAWNBATTLE:
                    MonsterBattle();
                    UpdateMonsters();
                    if (CheckBattleState(_player).ToLowerInvariant() == "lost" && CheckBattleState(_currentEnemy).ToLowerInvariant() == "lost")
                    {
                        Console.WriteLine("LADY DAWN: Hm, well this is quite the predicament.");
                        Console.ReadKey(true);
                        Console.WriteLine("LADY DAWN: We're fairly evenly matched wouldn't you think?");
                        Console.ReadKey(true);
                        Console.WriteLine("LADY DAWN: Alright fair enough, I'll allow it.");
                        Console.ReadKey(true);
                        Console.WriteLine("LADY DAWN: Allow me tell you a bit about Dawn and Dusk Types.");
                        _currentScene = Scene.DAWNDUSKSELECT;

                    }
                    else if (CheckBattleState(_player).ToLowerInvariant() == "lost")
                    {
                        Console.WriteLine("LADY DAWN: It seems as if you haven't entirely honed your skills yet...");
                        Console.ReadKey(true);
                        Console.WriteLine("LADY DAWN: It's alright, failure is the best form of learning.");
                        Console.ReadKey(true);
                        Console.WriteLine("LADY DAWN: return back to me once you have gotten stronger.");
                        _currentScene = Scene.DAWNDISTRICT;
                    }
                    else if (CheckBattleState(_currentEnemy).ToLowerInvariant() == "lost")
                    { 
                        Console.WriteLine("LADY DAWN: Wonderfully done, " + _player.GetName + " I can see you have potential");
                        Console.ReadKey(true);
                        Console.WriteLine("LADY DAWN: I do find it peculiar how the ministry refuses to give students like you a full team, rather than just one monster.");
                        Console.ReadKey(true);
                        Console.WriteLine("LADY DAWN: How on earth do they expect you to survive a fight with one Etherian alone I do not know.");
                        Console.ReadKey(true);
                        Console.WriteLine("LADY DAWN: No matter though, I'll give you an Etherian that can help you out a bit so that your " + _player.GetTeam[0].GetName
                            + " Doesn't have to fight alone ^^");
                        Console.ReadKey(true);
                        Console.WriteLine("LADY DAWN: Allow me tell you a bit about Dawn and Dusk Types.");
                        _currentScene = Scene.DAWNDUSKSELECT;
                    }
                    break;
                case Scene.DAWNDUSKSELECT:
                    DawnAndDuskSelect();
                    break;
                case Scene.REPLAYMENU:
                    DisplayReplayMenu();
                    break;
            }
        }
        
        /// <summary>
        /// Displays the main menu, allowing the player to
        /// Start a new game,
        /// load from a previous session,
        /// or quit the game entirely
        /// </summary>
        void DisplayMainMenu()
        {
            int PlayGame = GetInput("THE DAWN OF NIGHT: A Monster Fighter Demo", "New Game", "Load Game", "Quit");
            switch (PlayGame)
            {
                case 1:
                    _currentScene = Scene.CHOOSESTARTERPET;
                    break;
                case 2:
                    if (!Load())
                    {
                        Console.WriteLine("There has been an issue loading your save data.");
                        Console.ReadKey(true);
                    }
                    break;
                case 3:
                    _gameOver = true;
                    break;
            }
        }

        /// <summary>
        /// Allows player to replay the game after game has been fully completed.
        /// </summary>
        void DisplayReplayMenu()
        {
            Console.Clear();
            int playAgain = GetInput("Would you like to play again?", "Yes", "No");
            switch (playAgain)
            {
                case 1:
                    _player = new Player();
                    InitializePets();
                    InitializeEnemies();
                    _currentScene = Scene.CHOOSESTARTERPET;
                    break;
                case 2:
                    _gameOver = true;
                    break;
            }
        }

        void ChoosePetScene()
        {
            Console.Clear();
            Console.WriteLine("PRIEST: Welcome, young Seminary! It's a pleasure to have you here");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: Could I perhaps know your name?");
            string Playername = Console.ReadLine();
            if (Playername == "" || Playername == " ")
            {
                Console.WriteLine("PRIEST: Oh...well that's alright I guess");
                Console.ReadKey(true);
                Console.WriteLine("PRIEST: I'll just refer to you as " + _player.GetName + " then.");
            }
            else
            {
                _player.ChangeName(Playername);
                Console.WriteLine("PRIEST: A Pleasure to meet you, " + _player.GetName);

            }
            Console.WriteLine("PRIEST: You're quite lucky, The ministry chooses only " +
                "the best of it's students to come here...Especially students as young as yourself");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: But I suppose having young students in the field isn't a bad thing");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: However, enough of my rambling, I should tell you why you're here");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: As a seminary you have been studying creatures that go by the name Etherians");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: Etherians are magical creatures that gain their powers from the sky");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: There are four types, Etherians of Day, Night, Dawn, and Dusk");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: As a future Priest you should know that the ministry holds these creatures " +
                "in high regard due to their origins tying back to our faith.");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: You are here because you have been chosen by the ministry " +
                "to recieve a special oppertunity that allows you to not only learn about Etherians " +
                "but witness their behavior and power for yourself");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: I will grant you an Etherian that is either a Day type or a Night type " +
                "Based on your choosing.");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: You'll have the oppertunity to choose a Dusk or Dawn " +
                "Etherial later on.");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: You will take your Etherian with you to different districts and " +
                "You will prove your worth to other Priests");
            Console.ReadKey(true);
            ChoosePet("PRIEST: Now that you have a decent understanding as to why you're here " +
                "Tell me, which Etherian would you like?", _moonHush, _brightling);
            Console.WriteLine("PRIEST: Ah! a " + _player.GetTeam[0].GetName + ", wonderful choice!");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: Before you head off to the first district there is one more thing I'd like you to know");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: There should be 2 churches in each district, one being the church of whomever you're battling and The Church of Ether");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: You can SAVE, LOAD, and QUIT in the Churches of Ether");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: That is all the information I have for you, " + _player.GetName + ", go forth and do well. May the Etherians bless you.");
            Console.ReadKey(true);
            _currentScene = Scene.DAWNDISTRICT;
        }

        void District1Scene()
        {
            Console.Clear();
            Console.WriteLine("You venture along until you reach the first district, a city known for it's plentiful variety of dawn and dusk Etherians");
            Console.WriteLine("It's in this district a Priestess by the name of Dawn resides, her name fitting for the types that she wields.");
            Console.ReadKey(true);
            ExitDistrictMenu = false;
            while (!ExitDistrictMenu) 
            {
                int GoToArea = GetInput("Where would you like to go?", "Etherian Hospital", "Church of Sunrise", "Church of Ether");
                switch (GoToArea)
                {
                    case 1:
                        Hospital(_player.GetTeam);
                        break;
                    case 2:
                        if (CheckBattleState(_player).ToLower() == "lose")
                        {
                            Console.WriteLine("You cannot go into battle as all of your party members are incapacitated");
                        }
                        else
                        {
                            Console.WriteLine("You make your way towards the church of sunrise, it's glistening beauty standing before you.");
                            Console.ReadKey(true);
                            Console.WriteLine("It looks calm, peaceful even, much like a quiet morning.");
                            Console.ReadKey(true);
                            Console.WriteLine("You walk into the chapel to be greeted by a priestess.");
                            Console.ReadKey(true);
                            Console.WriteLine("LADY DAWN: Hello there, " + _player.GetName + ", its a pleasure to meet you. I'm Lady Dawn, the priestess of this district");
                            Console.ReadKey(true);
                            Console.WriteLine("LADY DAWN: You must be here to prove yourself to me so you can further yourself as a priest.");
                            Console.ReadKey(true);
                            Console.WriteLine("LADY DAWN: Very well then, let me see what you're capable of, if you win against me I'll share " +
                                "my knowledge of Dawn and Dusk types with you.");
                            Console.ReadKey(true);
                            _currentEnemy = _dawnEnemy;
                            _currentEnemyMonster = _dawnEnemy.GetTeam[_dawnEnemy.GetCurrMonIndex];
                            _currentPlayerMonster = _player.GetTeam[_player.GetCurrMonIndex];
                            ExitDistrictMenu = true;
                            _currentScene = Scene.DAWNBATTLE;
                        }
                        break;
                    case 3:
                        SaveChurch();
                        break;
                }
            }
            
        }

        void DawnAndDuskSelect()
        {
            Console.WriteLine("LADY DAWN: Dawn and Dusk types are bred Etherians, they are only born when a Day Type Etherian and a Night Type Etherian" +
                " are bred together.");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: They may seem like boring subtypes on the surface, but they can actually be incredibly useful");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: They primarily exist to create a sense of balance when battling Etherians, since both Day types and Night types" +
                " have an advantage against each other, the Dawn and Dusk types attempt to balance that extreme duality.");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: They allow for a chance to have a single Etherian have more advantage than the other.");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: Dawn types have an advantage over Night types and Dusk types have an advantage over Day types");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: However, Dusk and Dawn types have advantage against each other much like Day and Night types.");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: This is because advantage is determined by which time of day comes before the other. and since Dusk and Dawn types" +
                " contain both Day and Night type genes, the advantage determinator is a bit more complicated.");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: Dawn types have more Day type genetics, thus making them more powerful against Night Types. vice versa with Dawn types.");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: And that's all you need to know.");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: To recap: Dawn types have an advantage against Night types and Dusk Types have an advantage against Day types. " +
                "Dawn and Dusk types exist to create a balance between Day and Night types.");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: With that, I'll let you pick from one of my two Dawn and Dusk types, " +
                "I just recently hatched them and they're duplicates, so I have more than enough.");
            ChoosePet("LADY DAWN: So tell me, " + _player.GetName + ", Which Etherian would you prefer?\nWARNING: YOU ARE APPROACHING THE END OF THE DEMO",
                _SunnySide, _Dopey);
            Console.WriteLine("LADY DAWN: Ah! a " + _player.GetTeam[1] + ", wondeful!");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: Well I'll go ahead and send you on your way, run along now. Good luck beating the others, they're quite strong");
            Console.ReadKey(true);
            Console.WriteLine("LADY DAWN: I'm rooting for you," + _player.GetName);
            Console.ReadKey(true);
            _currentScene = Scene.REPLAYMENU;
        }

        /// <summary>
        /// Allows a player to choose between two monsters
        /// </summary>
        /// <param name="desc"> The prompt the player must answer</param>
        /// <param name="monster1"> 1st monster player must choose from</param>
        /// <param name="monster2"> 2nd monster player must choose from</param>
        void ChoosePet(string desc, Monster monster1, Monster monster2)
        {
            bool starterchosen = false;
            bool onStatsScreen = false;
            while (!starterchosen)
            {
                int choosepet = GetInput(desc, monster1.GetName, monster2.GetName, "Check stats");
                if (choosepet == 1)
                {
                    _player.AddToTeam(monster1);
                    starterchosen = true;
                }
                if (choosepet ==  2)
                {
                    _player.AddToTeam(monster2);
                    starterchosen = true;
                }
                if (choosepet == 3)
                {
                    onStatsScreen = true;
                    while (onStatsScreen)
                    {
                        int _stats = GetInput("Who's stats would you like to view?", monster1.GetName, monster2.GetName, "Return to selection");
                        if (_stats == 1)
                        {
                            Console.Clear();
                            monster1.WritePetStats();
                            Console.ReadKey(true);
                        }
                        if (_stats == 2)
                        {
                            Console.Clear();
                            monster2.WritePetStats();
                            Console.ReadKey(true);
                        }
                        if (_stats == 3)
                        {
                            onStatsScreen = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Where the player can heal their monsters
        /// </summary>
        /// <param name="TeamToHeal"> The team of monsters the player would like to heal</param>
        void Hospital(Monster[] TeamToHeal)
        {
            Console.WriteLine("NURSE PENNY: Welcome to the Etherial Hospital");

            //while finishedhospital is false, the player will not exit this loop
            bool finishedhospital = false;
            while (!finishedhospital)
            {
                //Asks the player what they'd like to do
                int HospitalOptions = GetInput("NURSE PENNY: How may we be of service to you today?", "Heal Team", "Leave Hospital");
                switch (HospitalOptions)
                {
                    //Heals Etherians
                    case 1:
                        Console.WriteLine("NURSE PENNY: Sure, let me just take your etherians off your hands so I can heal them.");
                        Console.ReadKey(true);
                        float amountToHeal;
                        //Loops through the array of monsters to heal
                        for (int i = 0; i < TeamToHeal.Length; i++)
                        {
                            //calculates how much to heal by getting the difference of the monsters max health to its current health
                            amountToHeal = TeamToHeal[i].GetMaxHealth - TeamToHeal[i].GetHealth;
                            //If the difference is not 0, heal the monster
                            if (amountToHeal != 0)
                            {
                                TeamToHeal[i].IncreaseHealth(amountToHeal);
                            }
                        }
                        Console.WriteLine("NURSE PENNY: There you go, all your Etherians should be restored to full health");
                        Console.ReadKey(true);
                        break;

                    //Breaks the loop causing player to leave hospital
                    case 2:
                        finishedhospital = true;
                        break;
                }
            }

        }

        /// <summary>
        /// A method that allows the player to save, load or quit
        /// </summary>
        void SaveChurch()
        {
            bool finishedChurch =false;
            //Keeps player in this loop if finishedChurch is False
            while (!finishedChurch)
            {
                int ChurchOptions = GetInput("ETHER PRIEST: Greetings young seminary, what can I assist you with today?", "Save", "Load", "Quit", "Leave church");
                switch (ChurchOptions)
                {
                    case 1:
                        Console.WriteLine("ETHER PRIEST: Give me a moment while I save your progress..");
                        Save();
                        Console.WriteLine("ETHER PRIEST:Progress Saved.");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        //Load previous data
                        break;
                    //ends the game
                    case 3:
                        _gameOver = true;
                        finishedChurch = true;
                        ExitDistrictMenu = true;
                        break;
                    //leaves the church
                    case 4:
                        finishedChurch = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Allows two monsters to fight each other
        /// </summary>
        void MonsterBattle()
        {
            float damagetaken = 0;

            //Print stats
            Console.Clear();
            Console.WriteLine("PLAYER:" + _player.GetName.ToUpper());
            _currentPlayerMonster.WritePetStats();
            Console.WriteLine("");
            Console.WriteLine("OPPONENT:" + _currentEnemy.GetName.ToUpper());
            _currentEnemyMonster.WritePetStats();
            Console.ReadKey(true);
            Console.Clear();

            //Fight
            Console.Clear();

            //Player turn
            damagetaken = _currentPlayerMonster.DoDamage(_currentEnemyMonster);
            Console.WriteLine(_currentPlayerMonster.GetName + " Attacks " + _currentEnemyMonster.GetName + " and does " + damagetaken + " damage.");
            if (_currentPlayerMonster.GetAdvantage(_currentEnemyMonster))
            {
                //checks advantage
                Console.WriteLine(_currentPlayerMonster.GetName + " has advantage!");
            }
            _currentEnemyMonster.DecreaseHealth(damagetaken);
            Console.ReadKey(true);
            damagetaken = _currentEnemyMonster.DoDamage(_currentPlayerMonster);

            //Enemy turn
            Console.WriteLine(_currentEnemyMonster.GetName + " Attacks " + _currentPlayerMonster.GetName + " and does " + damagetaken + " damage.");
            if (_currentEnemyMonster.GetAdvantage(_currentPlayerMonster))
            {
                //Checks advantage
                Console.WriteLine(_currentEnemyMonster.GetName + " has advantage!");
            }
            _currentEnemyMonster.DecreaseHealth(damagetaken);
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Updates the monsters in the player and the current enemy's team.
        /// </summary>
        void UpdateMonsters()
        {
            int NewIndex = 0;
            if (_currentEnemyMonster.GetHealth <= 0)
            {
                NewIndex = _currentEnemy.IncreaseCurrMonsterIndex(_currentEnemy.IndexTooLarge());
                if (_currentEnemy.IndexTooLarge())
                {
                    return;
                }
                _currentEnemyMonster = _currentEnemy.GetTeam[NewIndex];
                Console.WriteLine(_currentEnemy.GetName.ToUpper() + " summoned " + _currentEnemyMonster.GetName.ToUpper());
                Console.ReadKey(true);
            }
            if (_currentPlayerMonster.GetHealth <= 0)
            {
                NewIndex = _player.IncreaseCurrMonsterIndex(_player.IndexTooLarge());
                if (_player.IndexTooLarge())
                {
                    return;
                }
                _currentPlayerMonster = _player.GetTeam[NewIndex];
                Console.WriteLine(_player.GetName.ToUpper() + " summoned " + _currentEnemyMonster.GetName.ToUpper());
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// returns a string based on whether all the party members of a priests team are dead.
        /// </summary>
        /// <param name="Priest"> The priest the team belongs to</param>
        /// <returns></returns>
        string CheckBattleState(Priest Priest)
        {
            //Counts the number of monsters dead
            int NumOfMonstersDead = 0;

            //The string the method must return
            string BattleState = "Ongoing";

            //Loops through the team array
            for (int i = 0; i < Priest.GetTeam.Length; i++)
            {
                //checks if monsters health is below or equal to 0
                if (Priest.GetTeam[i].GetHealth <= 0)
                {
                    //Increases the amount of monsters dead by 1
                    NumOfMonstersDead++;
                }
            }

            //Checks if the number of monsters found dead is equal to the party length
            if (NumOfMonstersDead == Priest.GetTeam.Length)
            {
                BattleState = "Lost";
            }

            return BattleState;
        }
    }
}
