namespace minecreeper {

    class Game {
        private GameBoard GameBoard;
        private GameRenderer GameRenderer;

        private static int ACTION_POSITION = 0;
        private static int ROW_POSITION = 1;
        private static int COLUMN_POSITION = 2;

        public Game(GameBoard gameBoard, GameRenderer gameRenderer) {
            GameBoard = gameBoard;
            GameRenderer = gameRenderer;
        }

        public void Play() {
            bool gameComplete = false;

            System.Console.WriteLine("On each move, provide your action and cell position");
            System.Console.WriteLine("Eg. 'Flag row 0, column 2' would be 'flag 0 2'");
            System.Console.WriteLine("Eg. 'Step on row 0, column 2' would be 'step 0 2'");
            System.Console.WriteLine("Or 'quit' to end the game");

            
            while (!gameComplete) {
                GameRenderer.Draw();
                string location = System.Console.ReadLine();
   
                if (location.Contains("quit")) {
                    System.Console.WriteLine("You quitter!");
                    gameComplete = true;
                    break;
                }

                string[] move = location.Split(' ');
                string action = move[ACTION_POSITION];
                int row = System.Int32.Parse(move[ROW_POSITION]);
                int col = System.Int32.Parse(move[COLUMN_POSITION]);

                if (action.Equals("flag")) {
                    GameBoard.Flag(row, col);
                } else if (action.Equals("step")) {
                    Cell currentCell = GameBoard.Step(row, col);
                    if (currentCell.HasExploded) {
                        System.Console.WriteLine("You died");
                        GameRenderer.Draw();
                        //gameComplete = true;
                    }
                }
            }



        }

    }
}