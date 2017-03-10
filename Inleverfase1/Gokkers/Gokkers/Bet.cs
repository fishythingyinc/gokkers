using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gokkers
{
    class Bet
    {
        public void CheckWinner(Guy[] players, Fish[] entitys, Label betting, TextBox[] messageBoxes)
        {
            foreach(Guy player in players)
            {
                foreach (Fish entity in entitys)
                {
                    if (entity == null)
                    {

                    }
                    else {
                        if (("fish" + player.GetTarget().ToString()) == entity.GetName() && (entity.GetPosX() >= entity.RaceTrackLength && !entity.GetHasLost()))
                        {
                            player.Collect(betting);
                            player.ShowWinnerMessage(messageBoxes);
                        }
                        else
                        {
                            //losing
                        }
                    }
                }
                player.SetBetValue(0);
            }
        }
    }
}
