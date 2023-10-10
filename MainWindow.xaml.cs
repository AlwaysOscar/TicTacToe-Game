using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    // Default Constructor
    public partial class MainWindow : Window
    {
        #region Private Members

        // Store the content inside of cells (empty, X, or O)
        private char[,] gameGrid;

        // If True its player1 turn (X), false player2 turn (O)
        private bool isPlayerOneTurn;

        // Game condition
        private bool isGameOver;

        // count the total # of moves
        private int numOfMoves;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            numOfMoves = 0;
            NewGame();
        }
        #endregion

        #region NewGame Function
        // Starts a new game and clears all values back to the start
        private void NewGame()
        {
            int rows = 3;
            int columns = 3;

            // Create a new blank array of free cells
            gameGrid = new char[rows, columns];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    gameGrid[i, j] = ' ';

            // Player 1 starts the game
            isPlayerOneTurn = true;

            // UI display settings
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground, and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.LightGoldenrodYellow;
                button.Foreground = Brushes.Blue;
            });

            // Set Game Condition to not over
            isGameOver = false;

        }
        #endregion

        #region button_Click Function
        /// Handles a button click event
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event of the click</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Cast the sender to a button
            var button = (Button)sender;

            // Find the buttons positions in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            // Ignore move click if it was not a empty space
            if (gameGrid[row,column] != ' ')
                return;
            
            // Start a new game after game over condition is met
            if (isGameOver)
            {
                NewGame();
                return;
            }
            // Update Symbol changes in gameGrid to 'X' or 'O' based off isPlayerOneTurn condition
            gameGrid[row, column] = isPlayerOneTurn ? 'X' : 'O';

            // Update Symbol changes in UI to 'X' or 'O' based off isPlayerOneTurn condition
            button.Content = isPlayerOneTurn ? "X" : "O";

            // If Player's 2 turn display symbols 'O' to be Red, since it starts as blue (Brushes condition)
            if (!isPlayerOneTurn)
                button.Foreground = Brushes.Red;

            // If isPlayerOneTurn orginally true, it will become false. If orginally false, it will become true. (XOR)
            isPlayerOneTurn ^= true;

            // Increment the move count
            numOfMoves++;

            // Check for a winner
            CheckForWinner();

            
        }
        #endregion

        #region CheckForWinner Function
        private void CheckForWinner()
        {
            #region Horizontal wins
            // Check for horizontal wins

            // Top Row
            if (gameGrid[0, 0] != ' ' && gameGrid[0, 0] == gameGrid[0, 1] && gameGrid[0, 1] == gameGrid[0, 2])
            {
                // Game Ends
                isGameOver = true;

                // Highlight winning background of UI cells in LightGreen
                bTopLeft.Background = bTopMiddle.Background = bTopRight.Background = Brushes.LightGreen;
            }
            // Middle Row
            if (gameGrid[1, 0] != ' ' && gameGrid[1, 0] == gameGrid[1, 1] && gameGrid[1, 1] == gameGrid[1, 2])
            {
                // Game Ends
                isGameOver = true;

                // Highlight winning background of UI cells in LightGreen
                bMiddleLeft.Background = bMiddleMiddle.Background = bMiddleRight.Background = Brushes.LightGreen;
            }

            // Bottom Row
            if (gameGrid[2, 0] != ' ' && gameGrid[2, 0] == gameGrid[2, 1] && gameGrid[2, 1] == gameGrid[2, 2])
            {
                // Game Ends
                isGameOver = true;

                // Highlight winning background of UI cells in LightGreen
                bBottomLeft.Background = bBottomMiddle.Background = bBottomRight.Background = Brushes.LightGreen;
            }
            #endregion

            #region Vertical Wins
            // Check for vertical wins

            //  Left Column
            if (gameGrid[0, 0] != ' '  && gameGrid[0, 0] == gameGrid[1, 0] && gameGrid[1, 0] == gameGrid[2, 0])
            {
                // Game Ends
                isGameOver = true;

                // Highlight winning background of UI cells in LightGreen
                bTopLeft.Background = bMiddleLeft.Background = bBottomLeft.Background = Brushes.LightGreen;
            }
            
            // Middle Column
            if (gameGrid[0, 1] != ' '  && gameGrid[0, 1] == gameGrid[1, 1] && gameGrid[1, 1] == gameGrid[2, 1])
            {
                // Game Ends
                isGameOver = true;

                // Highlight winning cells in LightGreen
                bTopMiddle.Background = bMiddleMiddle.Background = bBottomMiddle.Background = Brushes.LightGreen;
            }

            // Right Column
            if (gameGrid[0, 2] != ' ' && gameGrid[0, 2] == gameGrid[1, 2] && gameGrid[1, 2] == gameGrid[2, 2])
            {
                // Game Ends
                isGameOver = true;

                // Highlight winning  background of UI cells in green
                bTopRight.Background = bMiddleRight.Background = bBottomRight.Background = Brushes.Green;
            }
            #endregion

            #region Digonal Wins
            // Check for diagonal wins
       
            // Top left to Bottom right
            if (gameGrid[0, 0] != ' ' && gameGrid[0, 0] == gameGrid[1, 1] && gameGrid[1, 1] == gameGrid[2, 2])
            {
                // Game Ends
                isGameOver = true;

                // Highlight winning  background of UI cells in LightGreen
                bTopLeft.Background = bMiddleMiddle.Background = bBottomRight.Background = Brushes.LightGreen;
            }

            // Top right to Bottom left
            if (gameGrid[0, 2] != ' ' && gameGrid[0, 2] == gameGrid[1, 1] && gameGrid[1, 1] == gameGrid[2, 0])
            {
                // Game Ends
                isGameOver = true;

                // Highlight winning background of UI cells in green
                bTopRight.Background = bMiddleMiddle.Background = bBottomLeft.Background = Brushes.LightGreen;
            }
            #endregion

            #region No Winners
            // If both players filled up all cells with no win condition met, then tie
            if (numOfMoves == 9 && isGameOver != true)
            {
                // Turn all background of UI cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.PaleVioletRed;
                });

                NewGame();
                return;
            }
            #endregion
        }
        #endregion
    }
}
