using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterFighter
{
    public enum Scene
    {
        MAINMENU,
        CHOOSEPET
    }


    class Game
    {
        private Scene _currentScene;
        private bool _gameOver;
        private Player _player;
        private DayMonster _brightling;

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
            _brightling = new DayMonster("Brightling", 300, 40, 20);
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
            ChooseStarterPet();
            Console.WriteLine("PRIEST: Ah! a " + _player.GetTeam[0].GetName + ", wonderful choice!");
            Console.ReadKey(true);
        }

        void ChooseStarterPet()
        {
            bool starterchosen = false;
            bool onStatsScreen = false;
            while (!starterchosen)
            {
                int choosepet = GetInput("PRIEST: Now that you have a decent understanding as to why you're here " +
                "Tell me, which Etherian would you like?", "Moonhush", "BrightLing", "Check stats");
                if (choosepet == 1)
                {

                }
                if (choosepet ==  2)
                {
                    _player.AddToTeam(_brightling);
                    starterchosen = true;
                }
                if (choosepet == 3)
                {
                    onStatsScreen = true;
                    while (onStatsScreen)
                    {
                        int _stats = GetInput("Who's stats would you like to view?", "MoonHush", "Brightling", "Return to selection");
                        if (_stats == 1)
                        {

                        }
                        if (_stats == 2)
                        {
                            _brightling.WritePetStats();
                        }
                        if (_stats == 3)
                        {
                            onStatsScreen = false;
                        }
                    }
                }
            }
        }
    }
}
