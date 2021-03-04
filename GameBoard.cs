using System;
using System.Collections.Generic;
using System.Collections;

namespace minecreeper
{
    class GameBoard
    {
        public List<Cell> Cells {get; private set;}
        public List<Int32> MineLocations {private get; set;} = new List<Int32>();
        public int BoardWidth {get; private set;}
        public int MineCount { get; private set; } = 0;
        public int FlagCount { get; private set; } = 0;
        public GameBoard(int dimension) {
            BoardWidth = dimension;
            Cells = new List<Cell>();
            Random rnd = new Random();
            // decide which cells have a mine
            for (int i=0; i<dimension*dimension; i++) {
                bool hasAMine = rnd.Next(16) > 9;
                Cell cellToAdd = new Cell(hasAMine);
                Cells.Add(cellToAdd);
                if (hasAMine) {
                    MineLocations.Add(i);
                }
            }
            // count the number of mines
            for (int row = 0; row < BoardWidth; row++)
            {
                for (int column = 0; column < BoardWidth; column++)
                {
                    // 1. Get position of cell
                    int position = GetPositionFromRowAndCol(row, column);
                    // 2. Get Cell
                    Cell currentCell = Cells[position];
                    if (currentCell.HasMine == true)
                    {
                        MineCount++;
                    }
                    Console.WriteLine("");
                    for(int I = MineCount; I > 0; I--) { Console.Write("#"); }
                    Console.WriteLine("");
                }
            }
            FlagCount = MineCount;
            // DONE: Add number of neighbouring mines
            for (int row = 0; row<BoardWidth; row++) {
                for (int column = 0; column<BoardWidth; column++) {
                    ModifyCellNeighbours(row, column, IncrementNeighbour);
                }
            }

        }

        public Cell Flag(int row, int column) {
            // each which group of BoardWidth size and then how far along.
            int position = GetPositionFromRowAndCol(row,column);
            // flip it
            Cell currentCell = Cells[position];
            if (currentCell.IsFlagged == false && FlagCount > 0)
            {
                currentCell.Flag();
                FlagCount--;
            } else if(currentCell.IsFlagged == true)
            {
                currentCell.Flag();
                FlagCount++;
            } else {
                Console.WriteLine("You don't have enough flags to complete this action.");
                Console.WriteLine("");
            }
            return currentCell;
        }

        private void ModifyCellNeighbours(int row, int column, Action<Cell, int> handler)
        {
            // 1. Get position of cell
            int position = GetPositionFromRowAndCol(row, column);
            // 1.5. Get higher and lower positions of the cell
            int abovePosition = GetPositionFromRowAndCol(row - 1, column);
            int belowPosition = GetPositionFromRowAndCol(row + 1, column);
            // 2. Get Cell
            Cell currentCell = Cells[position];
            // 3. Get Left Cell
            if (column != 0)
            {
                handler(currentCell, position - 1);
            }
            // 4. Get Right Cell
            if (column != BoardWidth - 1)
            {
                handler(currentCell, position + 1);
            }

            // 5. Get Top Cell
            if (row != 0)
            {
                handler(currentCell, abovePosition);
            }

            // 6. Get Bottom Cell
            if (row != BoardWidth - 1)
            {
                handler(currentCell, belowPosition);
            }
            // 7. Get TopLeft Cell
            if (row != 0 && column != 0)
            {
                handler(currentCell, abovePosition - 1);
            }
            // 7.5. Get TopRight Cell
            if (row != 0 && column != BoardWidth - 1)
            {
                handler(currentCell, abovePosition + 1);
            }
            // 8. Get BottomLeft Cell
            if (row != BoardWidth - 1 && column != 0)
            {
                handler(currentCell, belowPosition - 1);
            }
            // 8.5. Get BottomRight Cell
            if (row != BoardWidth - 1 && column != BoardWidth - 1)
            {
                handler(currentCell, belowPosition + 1);
            }
        }
        public Cell Step(int row, int column) {
            int position = GetPositionFromRowAndCol(row,column); 
            Cell currentCell = Cells[position];
            ModifyCellNeighbours(row, column, StepOnNeighbour);
            currentCell.Step();
            return currentCell;
        }

        private int GetPositionFromRowAndCol(int row, int column){
            return row * BoardWidth + column;
        }

        private void IncrementNeighbour(Cell currentCell, int position) {
            Cell neighbourCell = Cells[position];                            
            if(currentCell.HasMine){
                neighbourCell.IncrementNeighbouringMines();
            }

        }

        private void StepOnNeighbour(Cell currentCell, int position)
        {
            Cell neighbourCell = Cells[position];
            if (currentCell.HasMine == false &&
                neighbourCell.HasMine == false &&
                neighbourCell.NeighbouringMines == 0 &&
                neighbourCell.HasBeenSteppedOn == false &&
                neighbourCell.IsFlagged == false)
            {
                neighbourCell.Step();
            }
        }

    }
}