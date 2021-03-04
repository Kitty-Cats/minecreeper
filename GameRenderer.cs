namespace minecreeper {
    class GameRenderer {
        public bool DebugView {get; set;}
        private GameBoard board;
        public GameRenderer(GameBoard gameBoard) {
            board = gameBoard;
        }

        public void Draw() {
            int numberOfCells = board.BoardWidth * board.BoardWidth;
            for (int cell = 0 ; cell < numberOfCells; cell++) {
                Cell currentCell = board.Cells[cell];
                string cellContent = "[";
                /*if (DebugView) {
                    cellContent += currentCell.HasMine ? "*";
                }
                */
                if (currentCell.IsFlagged) {
                    cellContent += "F";
                } else if (currentCell.HasBeenSteppedOn || DebugView) {
                    if (currentCell.HasExploded) {
                        cellContent += "*";
                    } else {
                        cellContent += currentCell.NeighbouringMines + "";
                    }
                } else {
                    cellContent += " ";
                }

                cellContent += "]";

                System.Console.Write(cellContent);
                if ((cell != 0) && ((cell+1) % board.BoardWidth == 0)) {
                    System.Console.WriteLine("");
                }
            
            }
            System.Console.WriteLine("");
            System.Console.WriteLine(" END OF LINE ");
            System.Console.WriteLine("");
            string Flags = "You have ";
            Flags += board.FlagCount + " Flags left.";
            System.Console.WriteLine(Flags);
        }
    }
}