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
            
        }

        public int GetInput(string desc, params string[] options)
        {
            int InputRecieved = -1;
            string PlayerInput;
            Console.WriteLine(desc);
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine("[" + (i + 1) + "] " + options[i]);
            }

            while (InputRecieved == -1)
            {
                Console.Write(">");
                PlayerInput = Console.ReadLine();

                bool NumInput = int.TryParse(PlayerInput, out InputRecieved);
                if (NumInput)
                {
                    if (InputRecieved < 0 || InputRecieved > options.Length)
                    {
                        InputRecieved = -1;
                    }
                }
                else
                {
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

                    if (InputRecieved == -1)
                    {
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
            }

            return InputRecieved;
        }

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

        void DisplayMainMenu()
        {
            int PlayGame = GetInput("MONSTER FIGHTER", "New Game", "Load Game", "Quit");
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
