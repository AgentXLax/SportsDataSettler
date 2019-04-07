using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportsDataSettler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            string curItem;
            Models.Matchup nhl = new Models.Matchup(date);
            Models.GameData[] games = nhl.games;
            Models.GameSettler settle;
            Models.GameData curObject = null;
            bool isGame = false;
            string aStr;
            try
            {
                curItem = listBox1.SelectedItem.ToString();

                foreach (Models.GameData game in games)
                {
                    curObject = game;
                    if (game.ToString().Equals(curItem))
                    {
                        isGame = true;
                        break;
                    }
                }//for
                settle = new Models.GameSettler(curObject);
                if (isGame)
                {
                    aStr = settle.ToString();
                }
                else
                {
                    aStr = "There is nothing to display yet.";
                }
                label1.Text = aStr;
            }//try
            catch { }
            }//listbox_selectedIndexChanged

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            DateTime date = dateTimePicker1.Value;
            Models.Matchup nhl = new Models.Matchup(date);
            Models.GameData[] games = nhl.games;
            listBox1.Items.AddRange(games);
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;

            listBox1.Items.Clear();
            try
            {
                Models.Matchup nhl = new Models.Matchup(date);
                Models.GameData[] games = nhl.games;
                listBox1.Items.AddRange(games);
            } catch { listBox1.Items.Add("There are no games to display on this date."); }
    }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            Models.DailySettler settle = new Models.DailySettler(date);
            label1.Text = settle.ToString();
        }
    }
}
