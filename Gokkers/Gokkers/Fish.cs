using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gokkers
{
    class Fish
    {
        public static Random Randomizer = new Random();
        public int RaceTrackLength;
        public PictureBox MyPictureBox;
        private bool hasLost;
        private string name;

        private int distance;

        public Fish(int RaceTrackLength, PictureBox MyPictureBox, string name)
        {
            this.RaceTrackLength = RaceTrackLength;
            this.MyPictureBox = MyPictureBox;
            this.name = name;
            this.hasLost = false;
        }

        public bool Run(Label winner)
        {

            if (this.MyPictureBox.IsDisposed)
            {
                return false;
            }
            if (this.MyPictureBox.Location.X < RaceTrackLength)
            {

                distance = Randomizer.Next(1, 50);
                this.MyPictureBox.Location = new Point(this.MyPictureBox.Location.X + (distance / 10), this.MyPictureBox.Location.Y);
                return false;
            }
            else if (this.MyPictureBox.Location.X == 1000)
            {
                return false;
            }
            else if ((this.MyPictureBox.Location.X < 870) && (this.MyPictureBox.Location.X >= RaceTrackLength))
            {
                winner.Text = MyPictureBox.Name + " Wins";
                winner.Visible = true;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void TakeStartingPosition()
        {
            if (this.hasLost)
            {
                this.MyPictureBox.Location = new Point(1000, this.MyPictureBox.Location.Y);
            }
            else {
                if (this.MyPictureBox == null)
                {

                }
                else
                {
                    this.MyPictureBox.Location = new Point(23, this.MyPictureBox.Location.Y);
                }
            }
            
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetPosX()
        {
            return MyPictureBox.Location.X;
        }
        public bool GetHasLost()
        {
            return this.hasLost;
        }

        public void RemoveLast(Fish[] fish)
        {
            
        }
        public void SetHasLost(bool hasLost)
        {
            this.hasLost = hasLost;
        }
    }
}
