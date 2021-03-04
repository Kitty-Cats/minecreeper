using System;

namespace minecreeper
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard(10);
            GameRenderer renderer = new GameRenderer(board);
            renderer.DebugView=false;

            Game game = new Game(board, renderer);

            game.Play();
        }
    }
}
