using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using TicTacToeRendererLib.Enums;
using TicTacToeRendererLib.Renderer;

namespace TicTacToeSubmissionConole
{
    public class TicTacToe
    {
        private TicTacToeConsoleRenderer _boardRenderer;

        private List<(int, int)> player_1_moves = new List<(int, int)>();
        private List<(int, int)> player_2_moves = new List<(int, int)>();

        private int[][] win_conditions =
        {
            new[] { 0, 1, 2 },
            new[] { 3, 4, 5 },
            new[] { 6, 7, 8 },

            new[] { 0, 3, 6 },
            new[] { 1, 4, 7 },
            new[] { 2, 5, 8 },

            new[] { 0, 4, 8 },
            new[] { 2, 4, 6 }
        };

        private bool is_win = false;

        public TicTacToe()
        {
            _boardRenderer = new TicTacToeConsoleRenderer(10,6);
            _boardRenderer.Render();
        }

        private void play_move(PlayerEnum player, List<(int, int)> player_moves)
        {
            int row;
            int column;


            while (true)
            {
                Console.SetCursorPosition(2, 19);

                Console.Write($"Player {player}'s turn");

                Console.SetCursorPosition(2, 20);

                Console.Write("Please Enter Row: ");
                row = int.Parse(Console.ReadLine());

                Console.SetCursorPosition(2, 22);

                Console.Write("Please Enter Column: ");
                column = int.Parse(Console.ReadLine());

                if (player_moves.Contains((row, column)) || player_1_moves.Contains((row, column)) || player_2_moves.Contains((row, column)))
                {
                    Console.SetCursorPosition(20, 25);
                    Console.Write($"The cell in: Row {row}, Column {column} is already occupied");
                    continue;
                }
                break;
            }
            _boardRenderer.AddMove(row, column, player, true);
            player_moves.Add((row,column));
        }

        private bool win_check(List<(int, int)> player_moves)
        {
            var moves = new bool[9];

            foreach (var (row, column) in player_moves)
            {
                moves[row * 3 + column] = true;
            }

            foreach (var condition in win_conditions)
            {
                if (moves[condition[0]] && moves[condition[1]] && moves[condition[2]])
                {
                    return true;
                }
            }
            return false;
        }

        public void Run()
        {
            // FOR ILLUSTRATION CHANGE TO YOUR OWN LOGIC TO DO TIC TAC TOE
            while (!is_win)
            {
                play_move(PlayerEnum.X, player_1_moves);
                if (win_check(player_1_moves))
                {
                    Console.SetCursorPosition(20, 26);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Player 1 WINS!!!");
                    is_win = true;
                    break;
                }

                play_move(PlayerEnum.O, player_2_moves);
                if (win_check(player_2_moves))
                {
                    Console.SetCursorPosition(20, 26);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Player 2 WINS!!!");
                    is_win = true;
                    break;
                }
            }
        }

    }
}
