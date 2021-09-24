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
        }

        void Update()
        {
            DisplayCurrentScene();
        }

        void End()
        {
            Console.WriteLine("The application has ended, please close the console");
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

            //Writes out prompt and options
            Console.WriteLine(desc);
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine("[" + (i + 1) + "] " + options[i]);
            }

            //Checks to see if the input the player selected is a valid input
            while (InputRecieved == -1)
            {
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


    }
}
