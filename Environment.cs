using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chourbot_vacuum
{
    public partial class form1 : Form
    {
        // Timer pour la génération aléatoire d'objet
        Timer t = new Timer
        {
            Interval = 2000
        };

        // Tableau des cases 5 par 5
        Case[,] cases = new Case[5, 5];

        // Aspirateur
        Vacuum vacuum;

        // Problem
        Problem problem = new Problem();



        public form1()
        {
            InitializeComponent();

            t.Tick += new EventHandler(timer_tick);
            t.Start();
        }

        // Timer à partir duquel on fait spawn les objets
        private void timer_tick(object sender, EventArgs e)
        {
            generate_Object();
            var random = new Random();
            t.Interval = random.Next(5000, 10000);
        }

        // création de la grille et du robot 
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                grid.Rows.Add();
            }
            // Largeur des colonnes
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Width = 60;
            }
            // hauteur des lignes
            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Height = 60;
            }

            // Initialisation des cellules du tableau
            for(int column=0; column < grid.Columns.Count; column++)
            {
                for (int row = 0; row < grid.Rows.Count; row++)
                {
                    Case new_case = new Case(grid[column, row]);
                    cases[column, row] = new_case;
                }
            }

            // Ajout de l'aspirateur dans une case
            vacuum = new Vacuum(cases[0,0]);
        }

        // Génération d'objets
        private void generate_Object()
        {
            var random = new Random();
            
            // Random Object Selection
            int index_random_object_type = random.Next(50);

            // Type du nouvel objet
            String type_object = "";

            // Random Case Selection
            int index_random_column = random.Next(5);
            int index_random_row = random.Next(5);

            // 80% de chance -> dust
            if (index_random_object_type <= 40)
            {
                type_object = "dust";
                cases[index_random_column, index_random_row].spawn_dust();
            }
            else
            {
                type_object = "jewelry";
                cases[index_random_column, index_random_row].spawn_jewelry();
            }
            Object new_object = new Object((index_random_column, index_random_row), type_object);
            vacuum.objects_searched.Add(new_object);

        }



        // ------------------------- Boutons ------------------------- 
        private void button2_Click(object sender, EventArgs e)
        {
            vacuum.pick_up_jewelry();
            (int, int) no_more_jewelry_here = vacuum.belief.agent_position;
            for (int i = 0; i < vacuum.objects_searched.Count; i++)
            {
                if ((vacuum.objects_searched[i].position == no_more_jewelry_here) && ((vacuum.objects_searched[i].type == "jewelry")))
                {
                    vacuum.objects_searched.Remove(vacuum.objects_searched[i]);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            vacuum.clean_case();
            (int, int) no_more_object_here = vacuum.belief.agent_position;
            for(int i=0; i< vacuum.objects_searched.Count; i++)
            {
                if (vacuum.objects_searched[i].position == no_more_object_here)
                {
                    vacuum.objects_searched.Remove(vacuum.objects_searched[i]);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var a_case in cases)
            {
                {
                    a_case.clean_dust();
                    a_case.clean_jewelry();
                    vacuum.objects_searched = new List<Object>();
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show("Position " + vacuum.belief.get_position());
        }

        // Launch exploration BFS
        private void button4_Click(object sender, EventArgs e)
        {
            List<String> actions = vacuum.explorationBFS(cases);
            vacuum.move(actions, cases);
        }

        // launch ASTAR
        private void launch_astar_Click(object sender, EventArgs e)
        {
            {
                List<String> actions = vacuum.explorationASTAR();
                vacuum.move(actions, cases);
            }
        }

        private void restart_vacuum_position_Click(object sender, EventArgs e)
        {
            vacuum.restart_vacuum_position(cases);
        }
    }
}
