using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True, if it's player 1's turn (X) and false, if player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True, if the game is ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of free cells
            mResults = new MarkType[9];

            for (int i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure player 1 starts the game
            mPlayer1Turn = true;

            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.Azure;
                button.Foreground = Brushes.MidnightBlue;
            });

            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button, that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after its finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            if (!(sender is Button button))
                return;

            // Find the button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //     C0    C1    C2
            // R0  0     1     2
            // R1  3     4     5
            // R2  6     7     8

            // column0; row2 - 6 ( 6 = column0 + row2 * 3 = 0 + 6 = 6 )
            // column2; row2 - 8 ( 8 = column2 + row2 * 3 = 2 + 6 = 8 )
            var index = column + row * 3;

            // Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value base on which player turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";
            
            //Change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.OrangeRed;

            // Toggle the players turns
            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal wins

            // Check for horizontal wins
            //
            // Row 0
            //
            var same = (mResults[0] & mResults[1] & mResults[2]) == mResults[0];

            if (mResults[0] != MarkType.Free && same)
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.GreenYellow;
            }
            //
            // Row 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.GreenYellow;
            }
            //
            // Row 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.GreenYellow;
            }

            #endregion

            #region Vertical wins

            // Check for vertical wins
            //
            // Column 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.GreenYellow;
            }
            //
            // Column 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.GreenYellow;
            }
            //
            // Column 2
            //
            if (mResults[2] != MarkType.Free && (mResults[5] & mResults[2] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.GreenYellow;
            }

            #endregion

            #region Diagonal wins

            // Check for vertical wins
            //
            // Top Left Bottom Right
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.GreenYellow;
            }
            // Check for vertical wins
            //
            // Top Right Bottom Left
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.GreenYellow;
            }

            #endregion

            #region No winners

            // Check for no winner and full board
            if (mResults.All(result => result != MarkType.Free))
            {
                // Game ended
                mGameEnded = true;

                // Turn all cells orange
                // Iterate every button on the grid
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.DarkOrange;
                });
            }

            #endregion
        }
    }
}
