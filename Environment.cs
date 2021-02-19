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

namespace Chourbot_vacuum
{
    public partial class form1 : Form
    {
        // Thread contenant le cycle de vie de l'aspirateur
        thread.Thread thread_vacuum_live;

        bool is_vacuum_alive = false;

        // Timer pour la génération aléatoire d'objets
        Timer t1 = new Timer
        {
            Interval = 2000
        };

        // Timer pour rafraichir les données sur l'affichage
        Timer t2 = new Timer
        {
            Interval = 500
        };

        // Tableau des cases 5 par 5 
        Case[,] cases = new Case[5, 5];

        // Aspirateur
        Vacuum vacuum;

        // Problème
        Problem problem = new Problem();


        public form1()
        {
            InitializeComponent();

            // Initialisation du bouton start
            start_vacuum.Enabled = false;

            t1.Tick += new EventHandler(timer_tick);
            t1.Start();

            t2.Tick += new EventHandler(timer_refresh_tick);
            t2.Start();
        }

        // Fonction qui génére aléatoirement un objet toutes les 0.5 à 3 secondes
        private void timer_tick(object sender, EventArgs e)
        {
            generate_Object();
            var random = new Random();
            t1.Interval = random.Next(500, 3000);
        }

        // Fonction pour rafraichir l'affichage toutes les 0.5 secondes.
        private void timer_refresh_tick(object sender, EventArgs e)
        {
            bfs_iteration_number.Text = vacuum.get_number_iteration_BFS().ToString();
            astar_iteration_number.Text = vacuum.get_number_iteration_astar().ToString();
            electricity_number.Text = vacuum.get_electricity_number().ToString();
            jewel_pick_up.Text = vacuum.get_jewel_pick_up().ToString();
            dust.Text = vacuum.get_dust_cleaned().ToString();
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

            // Ajout de l'aspirateur dans la position initial
            vacuum = new Vacuum(cases[problem.x,problem.y]);

            // Lancement du thread de l'agent
            thread_vacuum_live = new thread.Thread(new thread.ThreadStart(vacuum_live));
        }

        // Cycle de vie de l'agent
        private void vacuum_live()
        {
            while (is_vacuum_alive) {
                if(vacuum.objects_searched.Count == 0)
                {
                    vacuum.set_electricity_number(0);
                }
                List<String> actions = new List<String>();

                // Choix de l'algorithme d'exploration (en fonction de l'état des radios buttons)
                if (breadth_first_search.Checked)
                    actions = vacuum.explorationBFS();
                else if(a_star.Checked)
                    actions = vacuum.explorationASTAR((int)max_depth_selector.Value);

                // Mise à jour des intentions (actions) de l'aspirateur pour atteindre son objectif
                vacuum.set_intention(actions);

                // On déplace l'aspirateur
                vacuum.move(cases);

                // Choose action and do it
                vacuum.choose_action();
            }
        }

        // Génération d'objets
        private void generate_Object()
        {
            var random = new Random();
            
            // Sélection d'un objet aléatoire
            int index_random_object_type = random.Next(50);

            // Type du nouvel objet
            String type_object = "";

            // Sélection d'une case aléatoire
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

            // Si l'objet exite déjà on ne l'ajoute pas
            foreach(Object an_object in vacuum.objects_searched)
            {
                if((an_object.position == new_object.position) && (new_object.GetType() == an_object.GetType()))
                {
                    return;
                }
            }
            // On ajoute le nouvel élément dans la liste d'objets recherchés par l'agent
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

        private void breadth_first_search_CheckedChanged(object sender, EventArgs e)
        {
            if (!is_vacuum_alive)
                start_vacuum.Enabled = true;
        }

        private void a_star_CheckedChanged(object sender, EventArgs e)
        {
            if(!is_vacuum_alive)
                start_vacuum.Enabled = true;
        }

        private void start_vacuum_Click(object sender, EventArgs e)
        {
            is_vacuum_alive = true;
            start_vacuum.Enabled = false;
            if (!thread_vacuum_live.IsAlive)
                thread_vacuum_live.Start();

        }
    }
}
