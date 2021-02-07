using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chourbot_vacuum
{
    public partial class Form1 : Form
    {
        Timer t = new Timer
        {
            Interval = 2000
        };

        List<Case> cases = new List<Case>();

        List<String> type_object = new List<String> { "Poussière", "bijou" };

        public Form1()
        {
            InitializeComponent();
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.Enabled = false;
                Case new_case = new Case();
                new_case.button = button;
                cases.Add(new_case);
            }
            t.Tick += new EventHandler(timer_tick);
            t.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Génération aléatoire d'objet
            generate_Object();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            generate_Object();
            var random = new Random();
            t.Interval = random.Next(5000, 10000);
        }

        private void generate_Object()
        {
            var random = new Random();
            // Random Case Selection
            int index_random_case = random.Next(cases.Count);

            // Random Object Selection
            int index_random_object_type = random.Next(type_object.Count);

            if (index_random_object_type == 0)
            {
                cases[index_random_case].spawn_dust();
            }
            else
            {
                cases[index_random_case].spawn_jewelry();
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var a_case in cases)
            {
                {
                    a_case.clean_dust();
                    a_case.clean_jewelry();
                }
            }
        }
    }
}
