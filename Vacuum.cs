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
        // Croyance
        public State belief = new State();


        // Desire - 1 pour chaque algorithme
        // 1 - Trouver un élément de la liste d'objets (Objectif pour l'algorithme BFS)
        public List<Object> objects_searched = new List<Object>();

        // 2 - Trouver l'objet le plus proche en un minimum de coups (Objectif pour l'algorithme Astar)
        public Object priority_wanted_object = new Object();

        // Intentions
        public List<String> intentions = new List<String>(new string[] { "up", "down", "left", "right"});

        Case vacuum_case;
        int jewelry_picked_up = 0;

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

        // Reinitialise la position de l'aspirateur en (0,0)
        public void restart_vacuum_position(Case[,] cases)
        {
            vacuum_case.set_is_vacuum(false);
            vacuum_case = cases[0, 0];
            vacuum_case.set_is_vacuum(true);
            belief.set_agent_position(0, 0);
        }

        // Déplace l'aspirateur en fonction de la liste d'actions passée en paramètre
        public void move(List<String> actions, Case[,] cases)
        {
            int x = belief.get_position().Item1;
            int y = belief.get_position().Item2;
            foreach (String action in actions)
            {
                vacuum_case.set_is_vacuum(false);
                if (action == "up")
                {
                    y -= 1;
                }
                else if (action == "down")
                {
                    y += 1;
                }
                else if (action == "right")
                {
                    x += 1;
                }

                else if (action == "left")
                {
                    x -= 1;
                }
                vacuum_case = cases[x, y];
                vacuum_case.set_is_vacuum(true);
                belief.set_agent_position(x, y);
            }
        }

        // Aspire les objets de la case
        public void clean_case()
        {
            vacuum_case.clean_jewelry();
            vacuum_case.clean_dust();
        }
        // Ramasser le bijou de la case
        public void pick_up_jewelry()
        {
            vacuum_case.clean_jewelry();
            jewelry_picked_up++;
        }

        // Création de noeuds à partir du noeuds passer en paramètre et de la liste d'actions possibles
        public List<Node> expand(Node curent_node)
        {
            List<Node> nodes = new List<Node>();

            foreach (String intention in intentions)
            {
                Node child_node = new Node(curent_node);

                int agent_x = curent_node.State.agent_position.Item1;
                int agent_y = curent_node.State.agent_position.Item2;

                if (intention == "up")
                {
                    if (curent_node.State.agent_position.Item2 > 0)
                        agent_y -= 1;
                    else
                        continue;
                }
                else if (intention == "down")
                {
                    if (curent_node.State.agent_position.Item2 < 4)
                        agent_y += 1;
                    else
                        continue;
                } 
                else if (intention == "left")
                {
                    if (curent_node.State.agent_position.Item1 > 0)
                        agent_x -= 1;
                    else
                        continue;
                }
                else if (intention == "right")
                {
                    if (curent_node.State.agent_position.Item1 < 4)
                        agent_x += 1;
                    else
                        continue;
                }
                child_node.Action.Add(intention);
                child_node.State.set_agent_position(agent_x, agent_y);
                nodes.Add(child_node);
            }
            return nodes;
        }

        // Renvoie vrai si la case testé à déjà été visitée
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

        // Fonction de calcul de l'objet le plus proche de l'agent
        public Object closest_object()
        {
            int vacuum_x = belief.agent_position.Item1;
            int vacuum_y = belief.agent_position.Item2;

            Object object_searched = new Object();

            double distance_object_searched = 100;

            foreach (Object an_object in objects_searched)
            {
                double distance = Math.Sqrt(Math.Pow(an_object.position.Item1 - vacuum_x, 2) + Math.Pow(an_object.position.Item2 - vacuum_y, 2));

                if (distance_object_searched > distance)
                {
                    distance_object_searched = distance;
                    object_searched = an_object;
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


        // ------------- AlgorithmeS d'exploration -------------
        public List<String> explorationBFS(Case[,] cases)
        {
            // Listes d'actions à retourner
            List<String> sequence_actions = new List<string>();

            // Liste de coordonner marqué
            List<(int, int)> marked_positions = new List<(int, int)>();

            // Noeud courant
            Node curent_node = new Node(belief);

            // Liste des états à explorer
            Queue<Node> nodes_to_be_explored = new Queue<Node>();

            nodes_to_be_explored.Enqueue(curent_node);

            int i = 0;
            bool leave = false;

            while (nodes_to_be_explored.Count != 0)
            {
                curent_node = nodes_to_be_explored.Dequeue();
                
                // On marque le noeud qui vient d'être exploité
                marked_positions.Add(curent_node.State.agent_position);

                // Atteindre l'objectif
                foreach (Object object_search in objects_searched)
                {
                    if (object_search.position == curent_node.State.agent_position)
                    {
                        sequence_actions = curent_node.Action;
                        leave = true;
                        break;
                    }
                }
                if(leave == true)
                {
                    break;
                }

                // Liste de nouveaux noeuds créer à partir du noeud courrant
                List<Node> new_nodes = new List<Node>(expand(curent_node));

                // On ajoute les noeuds valides à la queue
                foreach (Node child_node in new_nodes)
                {
                    if(!(test_marked_state(marked_positions, (child_node.State.agent_position.Item1, child_node.State.agent_position.Item2))))
                    {
                        nodes_to_be_explored.Enqueue(child_node);
                    }
                }
                i++;
            }
            Console.WriteLine("Nombre d'itérations de l'algorithme BFS : " + i);
            // Retourne une séquence d'actions si l'agent trouve un objet
            return sequence_actions;
        }


        public List<String> explorationASTAR()
        {
            // Listes d'actions à retourner
            List<String> sequence_actions = new List<string>();

            // Objet au plus proche de l'agent
            Object searched_object = closest_object();

            // Si l'objet retournée est null (sans type) alors on n'explore pas le graphe
            if(searched_object.type == "")
            {
                return sequence_actions;
            }

            // Liste de coordonnées marqués
            List<(int, int)> marked_positions = new List<(int, int)>();

            // Noeud courant
            Node curent_node = new Node(belief);

            // Liste des états à explorer
            Queue<Node> nodes_to_be_explored = new Queue<Node>();

            // Liste pour trier les noeuds par ordre de priorité en fonction de l'heuristique
            List<Node> nodes_to_be_explored_ordered = new List<Node>();

            nodes_to_be_explored.Enqueue(curent_node);

            marked_positions.Add(curent_node.State.agent_position);
            
            int i = 0;

            while (nodes_to_be_explored.Count != 0)
            {
                // Ranger dans l'ordre les éléments de la liste
                nodes_to_be_explored_ordered = nodes_to_be_explored.ToList();
                nodes_to_be_explored = new Queue<Node>(nodes_to_be_explored.OrderBy(node => node.Path_Cost));

                // On étudie le première élément de la liste : le moins couteux
                curent_node = nodes_to_be_explored.Dequeue();

                marked_positions.Add(curent_node.State.agent_position);

                // Est ce que l'agent se situe au même endroit que l'objet recherché ?
                if (searched_object.position == curent_node.State.agent_position)
                {
                    sequence_actions = curent_node.Action;
                    break;
                }

                // Liste de nouveaux noeuds créer à partir du noeud courrant
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
                i++;
            }
            Console.WriteLine("Nombre d'itérations de l'algorithme Astar : " + i);
            return sequence_actions;
        }
    }
}
