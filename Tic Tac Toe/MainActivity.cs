using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Threading;

namespace Tic_Tac_Toe
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        enum GameMode
        {
            Null = 0,
            TwoPlayersMode = 1,
            PlayWithComputer = 2
        }

        #region Controls and varibles
        LinearLayout mainMenuLinearLayout;
        LinearLayout gameLinearLayout;
        Button buttonPlayTwoPlayersMode;
        Button buttonPlayWithComputer;

        Button[] gameButton = new Button[9];
        Button startButton;
        Button resetButton;
        TextView endGameTextView;
        TextView showPlayerTextView;
        char player = ' ';
        char[] playerList = new char[2] { 'O', 'X' };
        int freeFields = 9;
        GameMode gameMode = GameMode.Null;
        bool IsGameGoOn = false;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Download Components of .xml:
            mainMenuLinearLayout = FindViewById<LinearLayout>(Resource.Id.linearLayoutMainMenu);
            gameLinearLayout = FindViewById<LinearLayout>(Resource.Id.linearLayoutGame);
            buttonPlayTwoPlayersMode = FindViewById<Button>(Resource.Id.ButtonPlayTwoPlayersMode);
            buttonPlayWithComputer = FindViewById<Button>(Resource.Id.buttonPlayWithComputer);

            gameButton[0] = FindViewById<Button>(Resource.Id.button1);
            gameButton[1] = FindViewById<Button>(Resource.Id.button2);
            gameButton[2] = FindViewById<Button>(Resource.Id.button3);
            gameButton[3] = FindViewById<Button>(Resource.Id.button4);
            gameButton[4] = FindViewById<Button>(Resource.Id.button5);
            gameButton[5] = FindViewById<Button>(Resource.Id.button6);
            gameButton[6] = FindViewById<Button>(Resource.Id.button7);
            gameButton[7] = FindViewById<Button>(Resource.Id.button8);
            gameButton[8] = FindViewById<Button>(Resource.Id.button9);
            startButton = FindViewById<Button>(Resource.Id.buttonStart);
            resetButton = FindViewById<Button>(Resource.Id.buttonReset);
            endGameTextView = FindViewById<TextView>(Resource.Id.EndGameTextView);
            showPlayerTextView = FindViewById<TextView>(Resource.Id.ShowPlayerTextView);

            // Get events:
            buttonPlayTwoPlayersMode.Click += ButtonPlayTwoPlayersMode_Click;
            buttonPlayWithComputer.Click += ButtonPlayWithComputer_Click;

            gameButton[0].Click += GameButton0_click;
            gameButton[1].Click += GameButton1_click;
            gameButton[2].Click += GameButton2_click;
            gameButton[3].Click += GameButton3_click;
            gameButton[4].Click += GameButton4_click;
            gameButton[5].Click += GameButton5_click;
            gameButton[6].Click += GameButton6_click;
            gameButton[7].Click += GameButton7_click;
            gameButton[8].Click += GameButton8_click;

            startButton.Click += StartButton_Click;
            resetButton.Click += ResetButton_Click;

            // After initialize components:
            IsGameGoOn = false;
            mainMenuLinearLayout.Visibility = Android.Views.ViewStates.Visible;
            gameLinearLayout.Visibility = Android.Views.ViewStates.Gone;
            FieldReset();
        }

        private void ButtonPlayWithComputer_Click(object sender, EventArgs e)
        {
            mainMenuLinearLayout.Visibility = Android.Views.ViewStates.Gone;
            gameLinearLayout.Visibility = Android.Views.ViewStates.Visible;
            gameMode = GameMode.PlayWithComputer;
        }

        private void ButtonPlayTwoPlayersMode_Click(object sender, EventArgs e)
        {
            mainMenuLinearLayout.Visibility = Android.Views.ViewStates.Gone;
            gameLinearLayout.Visibility = Android.Views.ViewStates.Visible;
            gameMode = GameMode.TwoPlayersMode;
        }

        private void GameButton0_click(object sender, EventArgs e)
        {
            CheckField(gameButton[0]);
        }

        private void GameButton1_click(object sender, EventArgs e)
        {
            CheckField(gameButton[1]);
        }

        private void GameButton2_click(object sender, EventArgs e)
        {
            CheckField(gameButton[2]);
        }

        private void GameButton3_click(object sender, EventArgs e)
        {
            CheckField(gameButton[3]);
        }

        private void GameButton4_click(object sender, EventArgs e)
        {
            CheckField(gameButton[4]);
        }

        private void GameButton5_click(object sender, EventArgs e)
        {
            CheckField(gameButton[5]);
        }

        private void GameButton6_click(object sender, EventArgs e)
        {
            CheckField(gameButton[6]);
        }

        private void GameButton7_click(object sender, EventArgs e)
        {
            CheckField(gameButton[7]);
        }

        private void GameButton8_click(object sender, EventArgs e)
        {
            CheckField(gameButton[8]);
        }

        private void CheckField(Button argButton)
        {
            if (gameMode == GameMode.TwoPlayersMode)
            {
                argButton.Enabled = false;
                argButton.Text = player.ToString();
                if (freeFields > 0)
                {
                    freeFields--;
                }
                CheckIsEnd();
                LoadNextPlayer();
            }
            else if (gameMode == GameMode.PlayWithComputer && player == playerList[0])
            {
                argButton.Enabled = false;
                argButton.Text = player.ToString();
                if (freeFields > 0)
                {
                    freeFields--;
                }
                CheckIsEnd();
                LoadNextPlayer();
                if (IsGameGoOn)
                {
                    //TODO ruch komputera
                    LockButtons();
                    ComputerGo();
                    CheckIsEnd();
                    UnlockButtons();
                    LoadNextPlayer();
                }
            }
        }

        private void UnlockButtons()
        {
            foreach (var button in gameButton)
            {
                if (button.Text != playerList[0].ToString() && button.Text != playerList[1].ToString())
                {
                    button.Enabled = true;
                }
            }
        }

        private void ComputerGo()
        {
            Thread.Sleep(10);

            if (IsGameGoOn)
            {
                int[] f = new int[9];
                int i = 0;
                foreach (var button in gameButton)
                {
                    if (button.Text == playerList[0].ToString()) // 1 - gracz
                    {
                        f[i] = 1;
                    }
                    else if (button.Text == playerList[1].ToString()) // 2 - komputer
                    {
                        f[i] = 2;
                    }
                    else // 0 - puste
                    {
                        f[i] = 0;
                    }
                    i++;
                }

                ComputerEqualsAlghoritm(ref f[0], ref f[4], ref f[8]);
                ComputerEqualsAlghoritm(ref f[6], ref f[4], ref f[2]);

                ComputerEqualsAlghoritm(ref f[0], ref f[1], ref f[2]);
                ComputerEqualsAlghoritm(ref f[3], ref f[4], ref f[5]);
                ComputerEqualsAlghoritm(ref f[6], ref f[7], ref f[8]);

                ComputerEqualsAlghoritm(ref f[0], ref f[3], ref f[6]);
                ComputerEqualsAlghoritm(ref f[1], ref f[4], ref f[7]);
                ComputerEqualsAlghoritm(ref f[3], ref f[5], ref f[8]);

                i = 0;
                foreach (var item in f)
                {
                    if (item == 10)
                    {
                        gameButton[i].Text = playerList[1].ToString();
                        if (freeFields > 0)
                        {
                            freeFields--;
                        }
                        goto END;
                    }
                    i++;
                }
                i = 0;
                foreach (var item in f)
                {
                    if (item == 20)
                    {
                        gameButton[i].Text = playerList[1].ToString();
                        if (freeFields > 0)
                        {
                            freeFields--;
                        }
                        goto END;
                    }
                    i++;
                }
                i = 0;
                foreach (var item in f)
                {
                    if (item == 30)
                    {
                        gameButton[i].Text = playerList[1].ToString();
                        if (freeFields > 0)
                        {
                            freeFields--;
                        }
                        goto END;
                    }
                    i++;
                }
            }
        END:;
        }

        private void ComputerEqualsAlghoritm(ref int v1, ref int v2, ref int v3)
        {
            if (((v1 == 0 || v1 == 2) &&
                (v2 == 0 || v2 == 2) &&
                (v3 == 0 || v3 == 2)) &&
                   ((v1 == 2 && v2 == 2) ||
                   (v2 == 2 && v3 == 2) ||
                   (v1 == 2 && v3 == 2)))
            {
                if (v1 == 0)
                {
                    v1 = 10;
                }
                else if (v2 == 0)
                {
                    v2 = 10;
                }
                else if (v3 == 0)
                {
                    v3 = 10;
                }
            }
            else if (v1 == 0 &&
                     v2 == 1 &&
                     v3 == 1)
            {
                v1 = 20;
            }
            else if (v1 == 1 &&
                     v2 == 0 &&
                     v3 == 1)
            {
                v2 = 20;
            }
            else if (v1 == 1 &&
                     v2 == 1 &&
                     v3 == 0)
            {
                v3 = 20;
            }

            else if (v1 == 1 &&
                     v2 == 0 &&
                     v3 == 0)
            {
                v2 = 30;
            }
            else if (v1 == 0 &&
                     v2 == 1 &&
                     v3 == 0)
            {
                v1 = 30;
            }
            else if (v1 == 0 &&
                     v2 == 0 &&
                     v3 == 1)
            {
                v2 = 30;
            }

        }

        private void StartButton_Click(object sender, System.EventArgs e)
        {
            resetButton.Visibility = Android.Views.ViewStates.Visible;
            startButton.Visibility = Android.Views.ViewStates.Gone;
            FieldReset();

            foreach (var button in gameButton)
            {
                button.Enabled = true;
            }
            IsGameGoOn = true;
            player = playerList[0];
            showPlayerTextView.Text = player.ToString();
        }


        private void ResetButton_Click(object sender, EventArgs e)
        {
            resetButton.Visibility = Android.Views.ViewStates.Gone;
            startButton.Visibility = Android.Views.ViewStates.Visible;
            IsGameGoOn = false;
            FieldReset();
            player = playerList[0];
            showPlayerTextView.Text = player.ToString();
        }

        private void LoadNextPlayer()
        {
            if (player == playerList[0])
            {
                player = playerList[1];

            }
            else if (player == playerList[1])
            {
                player = playerList[0];
            }
            else
            {
                player = playerList[0];
            }
            showPlayerTextView.Text = player.ToString();
        }

        private void FieldReset()
        {
            ResetButtonsText();
            LockButtons();
            endGameTextView.Visibility = Android.Views.ViewStates.Gone;
            freeFields = 9;
        }

        private void ResetButtonsText()
        {
            foreach (var button in gameButton)
            {
                button.Text = " ";
            }
        }

        private void LockButtons()
        {
            foreach (var button in gameButton)
            {
                button.Enabled = false;
            }
        }

        private void CheckIsEnd()
        {
            foreach (var p in playerList)
            {
                if (
                Equals(gameButton[0].Text, gameButton[1].Text, gameButton[2].Text, p.ToString()) ||
                Equals(gameButton[3].Text, gameButton[4].Text, gameButton[5].Text, p.ToString()) ||
                Equals(gameButton[6].Text, gameButton[7].Text, gameButton[8].Text, p.ToString()) ||

                Equals(gameButton[0].Text, gameButton[3].Text, gameButton[6].Text, p.ToString()) ||
                Equals(gameButton[1].Text, gameButton[4].Text, gameButton[7].Text, p.ToString()) ||
                Equals(gameButton[2].Text, gameButton[5].Text, gameButton[8].Text, p.ToString()) ||

                Equals(gameButton[0].Text, gameButton[4].Text, gameButton[8].Text, p.ToString()) ||
                Equals(gameButton[2].Text, gameButton[4].Text, gameButton[6].Text, p.ToString())
                )
                {
                    LockButtons();
                    endGameTextView.Visibility = Android.Views.ViewStates.Visible;
                    endGameTextView.Text = "Gracz \"" + p + "\" wygrał!";
                    resetButton.Visibility = Android.Views.ViewStates.Gone;
                    startButton.Visibility = Android.Views.ViewStates.Visible;
                    IsGameGoOn = false;
                    break;
                }
                else if (freeFields <= 0)
                {
                    LockButtons();
                    endGameTextView.Visibility = Android.Views.ViewStates.Visible;
                    endGameTextView.Text = "Koniec gry, nikt nie wygrał.";
                    resetButton.Visibility = Android.Views.ViewStates.Gone;
                    startButton.Visibility = Android.Views.ViewStates.Visible;
                    IsGameGoOn = false;
                }
            }
        }

        private bool Equals(string a, string b, string c)
        {
            return a == b && b == c;
        }

        private bool Equals(string a, string b, string c, string seed)
        {
            return Equals(a, b, c) && c == seed;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}