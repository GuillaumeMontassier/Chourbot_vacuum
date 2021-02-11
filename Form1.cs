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

        // état atuel du robot
        State actual_state = new State();


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

        // Génération d'objets
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
                // Mis à jour de l'état actuel
                actual_state.dust_position.Add((index_random_column, index_random_row));

            }
            else
            {
                cases[index_random_column, index_random_row].spawn_jewelry();
                // Mis à jour de l'état actuel
                actual_state.jewelry_position.Add((index_random_column, index_random_row));
            }

        }



        // ------------------------- Boutons ------------------------- 
        // move random vaccuum
        private void button1_Click_1(object sender, EventArgs e)
        {
            var random = new Random();
            var direction = random.Next(4);
            int new_x = 0;
            int new_y = 0;

            // haut
            if(direction == 0)
            {
                if(vacuum.get_vacuum_case().x > 0) 
                {
                    new_x = vacuum.get_vacuum_case().x - 1;
                }
                else
                {
                    new_x = vacuum.get_vacuum_case().x;
                }
            }
            // bas
            else if (direction == 1)
            {
                if(vacuum.get_vacuum_case().y < 5)
                {
                    new_y = vacuum.get_vacuum_case().y + 1;
                }
                else
                {
                    new_y = vacuum.get_vacuum_case().y;
                }
            }
            // gauche
            else if (direction == 2)
            {
                if (vacuum.get_vacuum_case().x > 0)
                {
                    new_x = vacuum.get_vacuum_case().x - 1;
                }
                else
                {
                    new_x = vacuum.get_vacuum_case().x;
                }
                
            }
            // droite
            else if (direction == 3)
            {
                
                if (vacuum.get_vacuum_case().x < 5)
                {
                    new_x = vacuum.get_vacuum_case().x + 1;
                }
                else
                {
                    new_x = vacuum.get_vacuum_case().x;
                }
            }



            Case new_case = cases[new_x, new_y];
            vacuum.move(new_case);

            // Mis à jour de l'état actuel
            actual_state.set_agent_position(new_x, new_y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vacuum.pick_up_jewelry();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            vacuum.clean_case();
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

        private void button1_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show("Position " + actual_state.get_position());
        }
    }
}
