using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using UI;

namespace Main
{

    static class EntryPoint
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static UI.UI userInterface;
        static UI.PlaylistForm playlistForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            userInterface = new UI.UI();
            playlistForm = new UI.PlaylistForm();
            playlistForm.Owner = userInterface;
            userInterface.setPlaylistForm(playlistForm);
            Application.Run(userInterface);
        }
    }
}
