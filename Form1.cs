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

        /*List<Case> cases = new List<Case>();*/
        Case[,] cases = new Case[5, 5];

        Vacuum vacuum;

        List<String> type_object = new List<String> { "Poussière", "bijou" };

        public Form1()
        {
            InitializeComponent();

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

 /*       private void generate_Object()
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
        }*/

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
        // création de la grille et du robot
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                grid.Rows.Add();
            }
            // Set width of column
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Width = 60;
            }
            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Height = 60;
            }

            // Initialisation des cellules du tableau
            for(int column=0; column < grid.Columns.Count; column++)
            {
                for (int row = 0; row < grid.Rows.Count; row++)
                {
                    Case new_case = new Case(grid[column, row], column, row);
                    cases[column, row] = new_case;
                }
            }
            // Ajout de l'aspirateur dans une case
            vacuum = new Vacuum(cases[0,0]);

        }

        private void generate_Object()
        {
            var random = new Random();
            
            // Random Object Selection
            int index_random_object_type = random.Next(50);

            // Random Case Selection
            int index_random_column = random.Next(5);
            int index_random_row = random.Next(5);

            // 80% de chance -> dust
            if (index_random_object_type <= 40)
            {
                cases[index_random_column, index_random_row].spawn_dust();
            }
            else
            {
                cases[index_random_column, index_random_row].spawn_jewelry();
            }
        }
    }
}
