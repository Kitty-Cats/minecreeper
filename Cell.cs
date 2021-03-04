using System;
using System.Collections.Generic;
using System.Collections;

namespace minecreeper
{
    
    class Cell {

        public bool HasMine { get; } = false;

        public bool HasExploded { get; private set; } = false;

        public bool HasBeenSteppedOn { get; private set; } = false;

        public Int32 NeighbouringMines { get; private set; } = 0;

        

        public bool IsFlagged { get; set; } = false ;

        public Cell(bool mineEnabled) {
            HasMine = mineEnabled;
            IsFlagged = false;
        }

        public void Step() 
        {
            if (IsFlagged == false && HasBeenSteppedOn == false)
            {
                HasExploded = HasMine;
                HasBeenSteppedOn = true;
            }
            else
            {
                Console.WriteLine("ERROR: You cannot step on a cell which has been flagged/already stepped on.");
                Console.WriteLine("");
            }
        }

        public void Flag() {
            if (HasBeenSteppedOn == false)
            {
                IsFlagged = !IsFlagged;
            }
            else
            {
                Console.WriteLine("ERROR: You cannot flag a cell that has been stepped on.");
                Console.WriteLine("");
            }
        }

        public void IncrementNeighbouringMines() {
            NeighbouringMines++;
        }

    }
}