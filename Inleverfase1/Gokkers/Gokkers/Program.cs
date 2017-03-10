using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gokkers
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MessageBox.Show("Do not turn your volume too high when using this program!", "Fishy Thingy MOTD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Run(new Form1());
        }
    }
}
