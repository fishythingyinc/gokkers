using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gokkers
{
    public partial class Form1 : Form
    {
        Fish[] contestants = new Fish[9];
        PictureBox[] fishsticks = new PictureBox[9];
        PictureBox[] entitys = new PictureBox[9];
        RadioButton[] playerBoxes = new RadioButton[3];
        TextBox[] messageBoxes = new TextBox[3];
        Guy[] players = new Guy[3];
        int[] xpositions = new int[9];
        string[] messageHolder = new string[3];
        Bet bet = new Bet();
        public void CreateEntity()
        {
            for (int i = 0; i < contestants.Length; i++)
            {
                contestants[i] = new Fish(850, entitys[i], entitys[i].Name);
                entitys[i].BackColor = Color.Transparent;
            }
        }

        public void CreatePlayers()
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Guy(playerBoxes[i].Text, playerBoxes[i]);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            string fileName = @"wav\BennyHill.wav";
            string path = Path.Combine(Environment.CurrentDirectory, @"assets\", fileName);

            //new SoundPlayer(
                //(System.IO.MemoryStream)Gokkers.Properties.Resources.ResourceManager.GetObject("BennyHill")
            //);
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = path;
            player.PlayLooping();

            entitys[0] = fish1;
            entitys[1] = fish2;
            entitys[2] = fish3;
            entitys[3] = fish4;
            entitys[4] = fish5;
            entitys[5] = fish6;
            entitys[6] = fish7;
            entitys[7] = fish8;
            entitys[8] = fish9;
            fishsticks[0] = fishStick1;
            fishsticks[1] = fishStick2;
            fishsticks[2] = fishStick3;
            fishsticks[3] = fishStick4;
            fishsticks[4] = fishStick5;
            fishsticks[5] = fishStick6;
            fishsticks[6] = fishStick7;
            fishsticks[7] = fishStick8;
            fishsticks[8] = fishStick9;
            playerBoxes[0] = player1;
            playerBoxes[1] = player2;
            playerBoxes[2] = player3;
            messageBoxes[0] = bottomMessage;
            messageBoxes[1] = centerMessage;
            messageBoxes[2] = topMessage;
            CreateEntity();
            CreatePlayers();
            foreach (PictureBox entity in entitys)
            {
                Background.Controls.Add(entity);
                entity.Location = new Point(entity.Location.X, entity.Location.Y);
                entity.BackColor = Color.Transparent;
            }
            foreach (PictureBox fishstick in fishsticks)
            {
                Background.Controls.Add(fishstick);
                fishstick.Location = new Point(fishstick.Location.X, fishstick.Location.Y);
                fishstick.BackColor = Color.Transparent;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            foreach (Fish contestant in contestants)
            {
                if (contestant == null)
                {

                }
                else {
                    bool result = contestant.Run(winningLabel);
                    if (result)
                    {
                        bet.CheckWinner(players, contestants, betLabel, messageBoxes);
                        player1.Enabled = true;
                        player2.Enabled = true;
                        player3.Enabled = true;
                        gameTimer.Stop();
                        for (int i = 0; i < contestants.Length; i++)
                        {
                            if (contestants[i] == null)
                            {

                            }
                            else {
                                xpositions[i] = contestants[i].GetPosX();
                            }
                        }
                        int loser = xpositions.Min();
                        for (int j = 0; j < xpositions.Length; j++)
                        {
                            if (xpositions[j] == loser)
                            {
                                if (contestants[j] == null)
                                {

                                }
                                else
                                {
                                    contestants[j].MyPictureBox.Location = new Point(1000, contestants[j].MyPictureBox.Location.Y);
                                    contestants[j].SetHasLost(true);
                                    fishsticks[j].Visible = true;
                                }

                            }
                        }
                        break;
                    }
                    else
                    {
                        contestant.Run(winningLabel);
                    }
                }
            }
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            winningLabel.Visible = false;
            betLabel.Visible = false;
            gameTimer.Start();
            foreach (Fish contestant in contestants)
            {
                if (contestant == null)
                {

                }
                else
                {
                    contestant.TakeStartingPosition();
                }
                
            }
            player1.Enabled = false;
            player1.Checked = false;
            player2.Enabled = false;
            player2.Checked = false;
            player3.Enabled = false;
            player3.Checked = false;

        }

        private void betBtn_Click(object sender, EventArgs e)
        {
            decimal betValue;
            betValue = betAmount.Value;
            if (player1.Checked)
            {
                if (players[0].Bet(betValue))
                {
                    players[0].ActivateBet(betValue, targetNumbers.Value);
                    players[0].UpdateMessage(messageBoxes);
                }
                player1.Enabled = false;
                player1.Checked = false;
            }
            else if (player2.Checked)
            {
                if (players[1].Bet(betValue))
                {
                    players[1].ActivateBet(betValue, targetNumbers.Value);
                    players[1].UpdateMessage(messageBoxes);
                }
                player2.Enabled = false;
                player2.Checked = false;
            }
            else if (player3.Checked)
            {
                if (players[2].Bet(betValue))
                {
                    players[2].ActivateBet(betValue, targetNumbers.Value);
                    players[2].UpdateMessage(messageBoxes);
                }
                player3.Enabled = false;
                player3.Checked = false;
            }
        }

        private void cashUpdate_Tick(object sender, EventArgs e)
        {
            foreach (Guy player in players)
            {
                player.SetBoxText();
            }
            for (int i = 0; i < playerBoxes.Length; i++)
            {
                if (playerBoxes[i].Checked)
                {
                    if (players[i].GetCash() <= 0)
                    {
                        playerBoxes[i].Enabled = false;
                        playerBoxes[i].Checked = false;
                    }
                    else
                    {
                        betAmount.Maximum = players[i].ChangeMaxBet();
                    }
                    betterLabel.Text = players[i].GetName();
                }
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            foreach (PictureBox fishstick in fishsticks)
            {
                fishstick.Visible = false;
            }
            foreach (Fish contestant in contestants)
            {
                contestant.SetHasLost(false);
                contestant.TakeStartingPosition();
                
            }
            foreach (Guy player in players)
            {
                player.SetCash(100);
            }

        }
    }
}
