using System;

namespace minecreeper
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard(5);
            GameRenderer renderer = new GameRenderer(board);
            renderer.DebugView=false;

            Game game = new Game(board, renderer);

            game.Play();
        }
    }
}
