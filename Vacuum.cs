using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chourbot_vacuum
{
    class Vacuum
    {
        // Modèle BDI
        // Croyance - Situation dans laquelle se trouve l'agent avec sa position
        public State belief = new State();


        // Desire - Trouver un élément de la liste d'objets
        public List<Object> objects_searched = new List<Object>();

        // Intentions - Actions pour que l'aspirateur atteigne un objet
        public List<String> intentions = new List<string>();


        // Problème
        Problem problem = new Problem();

        // Case sur laquelle se situe l'aspirateur
        public Case vacuum_case;

        // Statistique sur les performances de l'aspirateur
        int electricity_unit;
        int jewelry_picked_up = 0;
        int dust_cleaned = 0;

        // Nombre d'itération de chaque algorithme avant de renvoyer une liste d'actions
        int number_iteration_BFS = 0;
        int number_iteration_astar = 0;

        public Vacuum(Case new_vacuum_case)
        {
            vacuum_case = new_vacuum_case;
            vacuum_case.set_is_vacuum(true);

            // Initialisation de la croyance
            belief = new State(new_vacuum_case);
        }

        public Case get_vacuum_case()
        {
            return vacuum_case;
        }

        public int get_number_iteration_BFS()
        {
            return number_iteration_BFS;
        }

        public int get_number_iteration_astar()
        {
            return number_iteration_astar;
        }

        public int get_electricity_number()
        {
            return electricity_unit;
        }

        public int get_jewel_pick_up()
        {
            return jewelry_picked_up;
        }

        public int get_dust_cleaned()
        {
            return dust_cleaned;
        }

        public void set_electricity_number(int new_number)
        {
            electricity_unit = new_number;
        }

        public void set_intention(List<String> new_intentions)
        {
            intentions = new List<string>(new_intentions);
        }

        // Déplace l'aspirateur en fonction de la liste de ses intentions.
        public void move(Case[,] cases)
        {
            int x = belief.get_position().Item1;
            int y = belief.get_position().Item2;

            for(int i = 0; i<intentions.Count; i++)
            {
                vacuum_case.set_is_vacuum(false);
                if (intentions[i] == "up")
                {
                    y -= 1;
                }
                else if (intentions[i] == "down")
                {
                    y += 1;
                }
                else if (intentions[i] == "right")
                {
                    x += 1;
                }

                else if (intentions[i] == "left")
                {
                    x -= 1;
                }
                vacuum_case = cases[x, y];
                vacuum_case.set_is_vacuum(true);
                belief.set_agent_position(x, y);

                // Une unité supplémentaire lorsqu'un mouvement est produit
                electricity_unit++;

                // Refresh du visuel
                foreach(Case a_case in cases){
                    a_case.case_text();
                }
                // Temporisation de 0.5 seconde entre chaque déplacement pour les rendre visibles
                Thread.Sleep(500);
            }
        }

        // Aspire les objets de la case
        public void clean_case()
        {
            vacuum_case.clean_jewelry();
            vacuum_case.clean_dust();

            // On supprime de la liste objects_searched l'objet qui a été supprimé (clean)
            (int, int) no_more_object_here = belief.agent_position;
            for (int i = 0; i < objects_searched.Count; i++)
            {
                if (objects_searched[i].position == no_more_object_here)
                {
                    objects_searched.Remove(objects_searched[i]);
                }
            }
        }

        // Choisi d'effectuer ou non une action en fonction du type de l'objet
        public void choose_action()
        {
             if((vacuum_case.get_dust_status() == true) || (vacuum_case.get_jewelry_status() == true))
            {

                if ((vacuum_case.get_dust_status() == true) && (vacuum_case.get_jewelry_status() == true))
                {
                    jewelry_picked_up++;
                    dust_cleaned++;
                }
                else if ((vacuum_case.get_jewelry_status() == true))
                {
                    jewelry_picked_up++;
                }
                else if ((vacuum_case.get_dust_status() == true))
                {
                    dust_cleaned++;
                }
                clean_case();
                electricity_unit++;
            }
        }

        // Création de noeuds à partir du noeud passé en paramètre et de la liste d'actions (haut, bas, gauche, droite) spécifiée dans le problème
        public List<Node> expand(Node curent_node)
        {
            List<Node> nodes = new List<Node>();

            foreach (String action in problem.actions)
            {
                Node child_node = new Node(curent_node);

                int agent_x = curent_node.State.agent_position.Item1;
                int agent_y = curent_node.State.agent_position.Item2;

                if (action == "up")
                {
                    if (curent_node.State.agent_position.Item2 > 0)
                        agent_y -= 1;
                    else
                        continue;
                }
                else if (action == "down")
                {
                    if (curent_node.State.agent_position.Item2 < 4)
                        agent_y += 1;
                    else
                        continue;
                } 
                else if (action == "left")
                {
                    if (curent_node.State.agent_position.Item1 > 0)
                        agent_x -= 1;
                    else
                        continue;
                }
                else if (action == "right")
                {
                    if (curent_node.State.agent_position.Item1 < 4)
                        agent_x += 1;
                    else
                        continue;
                }
                child_node.Action.Add(action);
                child_node.State.set_agent_position(agent_x, agent_y);
                nodes.Add(child_node);
            }
            return nodes;
        }

        // Renvoie vrai si la case testée a déjà été visitée
        public bool test_marked_state(List<(int, int)> marked_states, (int, int) agent_position)
        {
            foreach ((int, int) marked_state in marked_states)
            {
                if (agent_position == marked_state)
                {
                    return true;
                }
            }
            return false;
        }

        // Calcul l'objet le plus proche de l'agent, la fonction permet de clarifier l'objectif de l'A*
        public Object closest_object()
        {
            int vacuum_x = belief.agent_position.Item1;
            int vacuum_y = belief.agent_position.Item2;

            Object object_searched = new Object();

            double distance_object_searched = 100;
            double distance = 0;

            for (int i = 0 ; i < objects_searched.Count; i++)
            {
                try { 
                    distance = Math.Sqrt(Math.Pow(objects_searched[i].position.Item1 - vacuum_x, 2) + Math.Pow(objects_searched[i].position.Item2 - vacuum_y, 2));
                }
                catch(NullReferenceException e)
                {
                    Console.WriteLine("error with the distance calcul", e);
                }
                if (distance_object_searched > distance)
                {
                    distance_object_searched = distance;
                    object_searched = objects_searched[i];
                }
            }
            return object_searched;
        }

        // Heuristique - Affectation d'une valeur à un noeud (distance entre la situation de l'agent et l'objet le plus proche)
        public double h(Object object_searched, int node_case_x, int node_case_y)
        {

            double distance = Math.Sqrt(Math.Pow(object_searched.position.Item1 - node_case_x, 2) + Math.Pow(object_searched.position.Item2 - node_case_y, 2));
            return distance;
        }


        // ------------- Algorithmes d'exploration -------------
        // Breadth First Search
        public List<String> explorationBFS()
        {

            // Listes d'actions à retourner
            List<String> sequence_actions = new List<string>();

            // Si il n'y a pas d'objet alors on n'explore pas le graphe
            if (objects_searched.Count == 0)
            {
                return sequence_actions;
            }

            // Liste de coordonnées déjà visitées
            List<(int, int)> marked_positions = new List<(int, int)>();

            // Noeud courant
            Node curent_node = new Node(belief);

            // Liste des états à explorer
            Queue<Node> nodes_to_be_explored = new Queue<Node>();

            nodes_to_be_explored.Enqueue(curent_node);

            int iteration_number = 0;
            bool leave = false;

            while ((nodes_to_be_explored.Count != 0) && (leave == false))
            {
                // On récupère le premier noeud
                curent_node = nodes_to_be_explored.Dequeue();
                
                // Marquage du noeud qui vient d'être exploité
                marked_positions.Add(curent_node.State.agent_position);

                // Atteindre l'objectif
                for(int i = 0; i< objects_searched.Count; i++)
                {
                    if (objects_searched[i].position == curent_node.State.agent_position)
                    {
                        leave = true;
                        break;
                    }
                }

                // Liste de nouveaux noeuds créer à partir du noeud courant
                List<Node> new_nodes = new List<Node>(expand(curent_node));

                // On ajoute les noeuds non marqués à la queue
                foreach (Node child_node in new_nodes)
                {
                    if(!(test_marked_state(marked_positions, (child_node.State.agent_position.Item1, child_node.State.agent_position.Item2))))
                    {
                        nodes_to_be_explored.Enqueue(child_node);
                    }
                }
                iteration_number++;
            }

            // On extrait les données du noeud courant 
            number_iteration_BFS = iteration_number;
            sequence_actions = curent_node.Action;

            // Retourne une séquence d'actions à effectuer
            return sequence_actions;
        }

        // A*
        public List<String> explorationASTAR(int max_depth)
        {
            // Listes d'actions à retourner
            List<String> sequence_actions = new List<string>();

            // Objet au plus proche de l'agent
            Object searched_object = closest_object();

            // Si il n'y a pas d'objet alors on n'explore pas le graphe
            if(searched_object.type == "")
            {
                return sequence_actions;
            }

            // Liste de coordonnées marquées
            List<(int, int)> marked_positions = new List<(int, int)>();

            // Noeud courant
            Node curent_node = new Node(belief);

            // Liste des états à explorer
            Queue<Node> nodes_to_be_explored = new Queue<Node>();

            nodes_to_be_explored.Enqueue(curent_node);

            marked_positions.Add(curent_node.State.agent_position);

            int iteration_number = 0;

            while (nodes_to_be_explored.Count != 0)
            {
                // Ranger dans l'ordre les éléments de la liste en fonction du coût
                nodes_to_be_explored = new Queue<Node>(nodes_to_be_explored.OrderBy(node => node.Path_Cost));

                // On étudie le première élément de la liste : le moins couteux
                curent_node = nodes_to_be_explored.Dequeue();

                marked_positions.Add(curent_node.State.agent_position);

                // Est ce que l'agent se situe au même endroit que l'objet recherché ?
                if ((searched_object.position == curent_node.State.agent_position) || (max_depth <= curent_node.Depth))
                {
                    break;
                }

                // Liste de nouveaux noeuds créer à partir du noeud courant
                List<Node> new_nodes = new List<Node>(expand(curent_node));

                // On ajoute les noeuds valides à la queue
                foreach (Node child_node in new_nodes)
                {
                    int agent_x = child_node.State.agent_position.Item1;
                    int agent_y = child_node.State.agent_position.Item2;
                    if (!(test_marked_state(marked_positions, (agent_x, agent_y))))
                    {
                        // f(n) = g(n) + h(n)
                        child_node.Path_Cost = 1 + h(searched_object, agent_x, agent_y);
                        nodes_to_be_explored.Enqueue(child_node);
                    }
                }
                iteration_number++;
            }
            // On extrait les données du noeud courant 
            number_iteration_astar = iteration_number;
            sequence_actions = curent_node.Action;

            return sequence_actions;
        }
    }
}
