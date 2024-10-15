// Application Name: Tic Tac Toe Game
// Description: This is a simple WPF application for playing a two-player Tic Tac Toe game.
// Creation Date: 2024-10-12
// Last Modified: 2024-10-15
// Created by: Ning Han

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;



namespace Assingment2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Variables to track scores for player X and player O
        private int xScore = 0;
        private int oScore = 0;

        // Array to represent the Tic Tac Toe game board
        private string[,] boardGame = new string[3, 3];

        // Variable to track the current player, starting with X
        private string currentPlayer = "X";

        public MainWindow()
        {
            InitializeComponent();
            // Set the initial display for the current player
            currentPlayerTxtBox.Text = currentPlayer;

            // Update the score display for both players
            UpdateScoreDisplay();
        }

        /// <summary>
        /// Handles the click event for the Tic Tac Toe board buttons
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button clicked");
            Button clickedButton = sender as Button;
            if (clickedButton == null)
            {
                return;
            }

            // Extract the row and column from the button's name (e.g., btn00 for row 0, column 0)
            string buttonName = clickedButton.Name; // e.g., btn00
            int row = int.Parse(buttonName[3].ToString());
            int column = int.Parse(buttonName[4].ToString());

            // Check if the button is already clicked
            if (clickedButton.Content != null && clickedButton.Content.ToString() != "")
            {
                MessageBox.Show("This cell has already been selected.", "Invalid Move", MessageBoxButton.OK);
                return;
            }

            // Update the board and UI with the current player's mark (X or O)
            clickedButton.Content = currentPlayer;
            boardGame[row, column] = currentPlayer;

            // Check if the current player has won
            if (checkWinner(boardGame))
            {
                MessageBox.Show($"There is a winner: {currentPlayerTxtBox.Text}");

                // Update the score for the current player
                if (currentPlayer == "X")
                {
                    xScore++;
                }
                else
                {
                    oScore++;
                }

                // Update the score display and reset the board
                UpdateScoreDisplay();
                ResetBoard();
                return;
            }

            // Switch to the other player (X to O, or O to X)
            currentPlayer = (currentPlayer == "X") ? "O" : "X";

            // Update the display to show the current player
            currentPlayerTxtBox.Text = currentPlayer;
        }

        /// <summary>
        /// This function checks if there is a winner in the Tic Tac Toe game
        /// </summary>
        /// <param name="boardGame">The current state of the Tic Tac Toe board</param>
        /// <returns>True if a player has won, otherwise False</returns>
        private bool checkWinner(string[,] boardGame)
        {
            // Size of the Tic Tac Toe board (3x3)
            int size = 3;

            // Check each row for a win
            for (int row = 0; row < size; row++)
            {
                if (boardGame[row, 0] == boardGame[row, 1] &&
                    boardGame[row, 1] == boardGame[row, 2] &&
                    !string.IsNullOrEmpty(boardGame[row, 0]))
                {
                    // Highlight the winning row
                    HighlightButtons($"btn{row}0", $"btn{row}1", $"btn{row}2");
                    return true;
                }
            }

            // Check each column for a win
            for (int col = 0; col < size; col++)
            {
                if (boardGame[0, col] == boardGame[1, col] &&
                    boardGame[1, col] == boardGame[2, col] &&
                    !string.IsNullOrEmpty(boardGame[0, col]))
                {
                    // Highlight the winning column
                    HighlightButtons($"btn0{col}", $"btn1{col}", $"btn2{col}");
                    return true;
                }
            }

            // Check the diagonal from top-left to bottom-right
            if (boardGame[0, 0] == boardGame[1, 1] &&
                boardGame[1, 1] == boardGame[2, 2] &&
                !string.IsNullOrEmpty(boardGame[0, 0]))
            {
                // Highlight the diagonal
                HighlightButtons("btn00", "btn11", "btn22");
                return true;
            }

            // Check the diagonal from top-right to bottom-left
            if (boardGame[0, 2] == boardGame[1, 1] &&
                boardGame[1, 1] == boardGame[2, 0] &&
                !string.IsNullOrEmpty(boardGame[0, 2]))
            {
                // Highlight the diagonal
                HighlightButtons("btn02", "btn11", "btn20");
                return true;
            }

            // No winner found
            return false;
        }

        /// <summary>
        /// Highlights the buttons that are part of the winning line
        /// </summary>
        /// <param name="buttonNames">The names of the buttons to highlight</param>
        private void HighlightButtons(params string[] buttonNames)
        {
            // Loop through each button and change its background color to yellow
            for (int i = 0; i < buttonNames.Length; i++)
            {
                Button btn = (Button)this.FindName(buttonNames[i]);
                if (btn != null)
                {
                    btn.Background = Brushes.Yellow;
                }
            }
        }

        /// <summary>
        /// Updates the display of the score for both players
        /// </summary>
        private void UpdateScoreDisplay()
        {
            xScoreTxtBox.Text = $"{xScore}";
            oScoreTxtBox.Text = $"{oScore}";
        }

        /// <summary>
        /// Resets the game, including both the board and the scores
        /// </summary>
        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            xScore = 0;
            oScore = 0;
            UpdateScoreDisplay();
            ResetBoard();
        }

        /// <summary>
        /// Exits the game application
        /// </summary>
        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Resets the Tic Tac Toe board to the initial state
        /// </summary>
        private void ResetBoard()
        {
            // Reset the game board array to empty
            boardGame = new string[3, 3];

            // Clear the content of each button and reset its background color
            btn00.Content = ""; btn00.Background = Brushes.LightGray;
            btn01.Content = ""; btn01.Background = Brushes.LightGray;
            btn02.Content = ""; btn02.Background = Brushes.LightGray;
            btn10.Content = ""; btn10.Background = Brushes.LightGray;
            btn11.Content = ""; btn11.Background = Brushes.LightGray;
            btn12.Content = ""; btn12.Background = Brushes.LightGray;
            btn20.Content = ""; btn20.Background = Brushes.LightGray;
            btn21.Content = ""; btn21.Background = Brushes.LightGray;
            btn22.Content = ""; btn22.Background = Brushes.LightGray;

            // Reset the current player to "X"
            currentPlayer = "X";
            currentPlayerTxtBox.Text = currentPlayer;

            Debug.WriteLine("Board has been reset.");
        }
    }
}