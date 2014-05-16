using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace ScreencastClipFlip
{
    public partial class Form1 : Form
    {
        Timer timer;
        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Tick += new EventHandler(CountDown);
            timer.Interval = 5000;
            timer.Start();
        }

        private void CountDown(object sender, EventArgs e)
        {
            string textData = Clipboard.GetText();
            if (textData.StartsWith("http://screencast.com/t/"))
            {
                using (WebClient client = new WebClient ()) // WebClient class inherits IDisposable
                {
                    string htmlCode = client.DownloadString(textData);
                    Regex regex = new Regex("(http://content.screencast.com/users/[^\"]*\\.png)");
                    if (regex.IsMatch(htmlCode))
                    {
                        string text = regex.Match(htmlCode).Value;
                        Clipboard.SetData(DataFormats.Text, (Object)text);
                    }
                }
            }
        
        }
    }
}
