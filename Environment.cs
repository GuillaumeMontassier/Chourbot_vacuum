using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using thread = System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// using Timer1 = System.Windows.Forms;
namespace Chourbot_vacuum
{
    public partial class form1 : Form
    {

        thread.Thread thread1;

        bool is_vacuum_alive = false;

        // Timer pour la génération aléatoire d'objets
        Timer t1 = new Timer
        {
            Interval = 2000
        };

        Timer t2 = new Timer
        {
            Interval = 500
        };

        // Tableau des cases 5 par 5
        Case[,] cases = new Case[5, 5];

        // Aspirateur
        Vacuum vacuum;

        // Problem
        Problem problem = new Problem();

        // Mesure de performance
        int performance_mesure = 0;


        public form1()
        {
            InitializeComponent();

            // Initialisation les boutons
            start_vacuum.Enabled = false;
            stop_vacuum.Enabled = false;

            t1.Tick += new EventHandler(timer_tick);
            t1.Start();

            t2.Tick += new EventHandler(timer_refresh_tick);
            t2.Start();
        }

        // Timer à partir duquel on fait spawn les objets (ici toutes les 2 secondes)
        private void timer_tick(object sender, EventArgs e)
        {
            generate_Object();
            var random = new Random();
            t1.Interval = random.Next(5000, 10000);
        }

        // On rafraichit l'affichages toutes les 0.5 secondes.
        private void timer_refresh_tick(object sender, EventArgs e)
        {
            bfs_iteration_number.Text = vacuum.get_number_iteration_BFS().ToString();
            astar_iteration_number.Text = vacuum.get_number_iteration_astar().ToString();
            electricity_number.Text = vacuum.get_electricity_number().ToString();
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
                column.Width = 80;
            }
            // hauteur des lignes
            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Height = 80;
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

            // Lancement du thread de l'agent
            thread1 = new thread.Thread(new thread.ThreadStart(vacuum_live));

/*            electricity.Text = vacuum.electricity_unit.ToString();*/
        }
        private void vacuum_live()
        {
            while (is_vacuum_alive) {
                if(vacuum.objects_searched.Count == 0)
                {
                    vacuum.set_electricity_number(0);
                }
                List<String> actions = new List<String>();

                // Choix de l'algorithme d'exploration
                if (breadth_first_search.Checked)
                    actions = vacuum.explorationBFS();
                else if(a_star.Checked)
                    actions = vacuum.explorationASTAR();
                
                vacuum.move(actions, cases);

                // Choose action and do it
                vacuum.choose_action();

            }
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

        private void restart_vacuum_position_Click(object sender, EventArgs e)
        {
            vacuum.restart_vacuum_position(cases);
        }

        private void breadth_first_search_CheckedChanged(object sender, EventArgs e)
        {
            start_vacuum.Enabled = true;
        }

        private void a_star_CheckedChanged(object sender, EventArgs e)
        {
            start_vacuum.Enabled = true;
        }

        private void stop_vacuum_Click(object sender, EventArgs e)
        {
            /*if (thread1.IsAlive)
                thread1.Interrupt();*/
            is_vacuum_alive = false;
            start_vacuum.Enabled = true;
            stop_vacuum.Enabled = false;
        }

        private void start_vacuum_Click(object sender, EventArgs e)
        {
            is_vacuum_alive = true;
            if (!thread1.IsAlive)
                thread1.Start();
            start_vacuum.Enabled = false;
            stop_vacuum.Enabled = true;
        }
    }
}
