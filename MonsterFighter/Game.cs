using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    public enum Scene
    {
        MAINMENU,
        CHOOSEPET,
        DAWNDISTRICT
    }


    class Game
    {
        private Scene _currentScene;
        private bool _gameOver;
        private Player _player;
        bool ExitDistrictMenu;

        //Monsters for player
        private DayMonster _brightling;
        private NightMonster _moonHush;

        //EnemyMonsters

        //Dawns Team
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
        }

        void Update()
        {
            DisplayCurrentScene();
        }

        void End()
        {
            Console.WriteLine("The application has ended, please close the console");
        }

        void InitializePets()
        {
            //Starters
            _brightling = new DayMonster("Brightling", 300, 30, 40);
            _moonHush = new NightMonster("Moonhush", 200, 50, 30);

            //Enemy monsters
            //Dawn Team
            _whispurn = new DuskMonster("Whispurn", 300, 30, 20);
            _Snoozem = new DuskMonster("Snoozem", 200, 30, 40);
            _EarlyBird = new DawnMonster("EarlyBird", 400, 20, 30);
            
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
                case Scene.CHOOSEPET:
                    ChoosePetScene();
                    break;
                case Scene.DAWNDISTRICT:
                    District1Scene();
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
                    _currentScene = Scene.CHOOSEPET;
                    break;
                case 2:
                    //loadgame
                    break;
                case 3:
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
            ChooseStarterPet("PRIEST: Now that you have a decent understanding as to why you're here " +
                "Tell me, which Etherian would you like?", _moonHush, _brightling);
            Console.WriteLine("PRIEST: Ah! a " + _player.GetTeam[0].GetName + ", wonderful choice!");
            Console.ReadKey(true);
            Console.WriteLine("PRIEST: Before you head off to the first district there is one more thing I'd like you to know");
            Console.WriteLine("PRIEST: There should be 2 churches in each district, one being the church of whomever you're battling and The Church of Ether");
            Console.WriteLine("PRIEST: You can SAVE, LOAD, and QUIT in the Churches of Ether");
            Console.WriteLine("PRIEST: That is all the information I have for you, " + _player.GetName + ", go forth and do well. May the Etherians bless you.");
            _currentScene = Scene.DAWNDISTRICT;
        }

        void District1Scene()
        {
            Console.Clear();
            Console.WriteLine("You venture along until you reach the first district, a city known for it's plentiful variety of dawn and dusk Etherians");
            Console.WriteLine("It's in this district a Priestess by the name of Dawn resides, her name fitting for the types that she wields.");
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
                        ExitDistrictMenu = true;
                        break;
                    case 3:
                        SaveChurch();
                        break;
                }
            }
            Console.WriteLine("You make your way towards the church of sunrise, it's glistening beauty standing before you.");
            Console.WriteLine("It looks calm, peaceful even, much like a quiet morning.");
            Console.WriteLine("You walk into the chapel to be greeted by a priestess.");
            Console.WriteLine("LADY DAWN: Hello there, " + _player.GetName + ", its a pleasure to meet you. I'm Lady Dawn, the priestess of this district");
            Console.WriteLine("LADY DAWN: You must be here to prove yourself to me so you can further yourself as a priest.");
            Console.WriteLine("LADY DAWN: Very well then, let me see what you're capable of, if you win against me I'll share " +
                "my knowledge of Dawn and Dusk types with you.");


        }


        //NOTE TO SELF: MAKE THIS FUNCTION MORE APPLICABLE TO ALL CASES CHOOSING MONSTERS
        void ChooseStarterPet(string desc, Monster monster1, Monster monster2)
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
                            monster1.WritePetStats();
                        }
                        if (_stats == 2)
                        {
                            monster2.WritePetStats();
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
            //Keeps player in this loop so they don't automatically leave the church if finishedChurch is False
            while (!finishedChurch)
            {
                int ChurchOptions = GetInput("ETHER PRIEST: Greetings young seminary, what can I assist you with today?", "Save", "Load", "Quit", "Leave church");
                switch (ChurchOptions)
                {
                    case 1:
                        //save progress
                        break;
                    case 2:
                        //Load previous data
                        break;
                    //ends the game
                    case 3:
                        _gameOver = true;
                        break;
                    //leaves the church
                    case 4:
                        finishedChurch = true;
                        break;
                }
            }
        }
    }
}
