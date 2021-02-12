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

        // Desire, Trouver une case non vide is_there_an_object = true
        public List<Object> objects_searched = new List<Object>();

        // Intention
        // public List<String> actions = new List<String>(new string[] { "haut", "bas", "gauche", "droite"});

        // public List<String> possible_actions = new List<String>(new string[] { "aspirer","ramasser" });

        // Liste des capteurs
        List<Sensor> sensors = new List<Sensor>();

        Case vacuum_case;
        int jewelry_picked_up = 0;

        // Problem
        Problem problem;

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


        public void move(List<String> actions, Case[,] cases)
        {
            int x = belief.get_position().Item1;
            int y = belief.get_position().Item2;
            foreach (String action in actions)
            {
                Console.Write("x : " + x + "y : " + y);
                vacuum_case.set_is_vacuum(false);
                if (action == "up")
                {
                    y -= 1;
                    Console.WriteLine("Comparaison up : " + y);
                }
                else if (action == "down")
                {
                    y += 1;
                    Console.WriteLine("Comparaison down y : " + y);
                }
                else if (action == "right")
                {
                    x += 1;
                    Console.WriteLine("Comparaison right : " + x);
                }

                else if (action == "left")
                {
                    x -= 1;
                    Console.WriteLine("Comparaison left : " + x);
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


        // AlgorithmeS d'exploration

        public List<String> explorationBFS(Case[,] cases, Problem problem)
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

            int agent_x = 0;
            int agent_y = 0;

            while (nodes_to_be_explored.Count != 0)
            {
                curent_node = nodes_to_be_explored.Dequeue();
    
                marked_positions.Add(curent_node.State.agent_position);

                // Atteindre l'objectif
                /*                foreach ((int, int) dust in belief.dust_position)
                                {
                                    if (dust == curent_node.State.agent_position)
                                    {
                                        Console.WriteLine("Poussière trouvée !" + curent_node.State.agent_position);
                                        sequence_actions = curent_node.Action;
                                        return sequence_actions;
                                    }
                                }*/
                foreach (Object object_search in objects_searched)
                {
                    if (object_search.position == curent_node.State.agent_position)
                    {
                        Console.WriteLine("Objet trouvé !" + curent_node.State.agent_position);
                        sequence_actions = curent_node.Action;
                        return sequence_actions;
                    }
                }

                // Ajout des états possibles à partir de l'état courant
                if (curent_node.State.agent_position.Item1 > 0)
                {
                    Node child_node = new Node(curent_node);
                    agent_x = curent_node.State.agent_position.Item1 - 1;
                    agent_y = curent_node.State.agent_position.Item2;
                    child_node.Action.Add("left");
                    if (!(test_marked_state(marked_positions, (agent_x,agent_y)))) {
                        child_node.State.set_agent_position(agent_x, agent_y);
                        nodes_to_be_explored.Enqueue(child_node);
                    }
                }
                if (curent_node.State.agent_position.Item1 < 4)
                {
                    Node child_node = new Node(curent_node);
                    agent_x = curent_node.State.agent_position.Item1 + 1;
                    agent_y = curent_node.State.agent_position.Item2;
                    child_node.Action.Add("right");
                    if (!(test_marked_state(marked_positions, (agent_x, agent_y))))
                    {
                        child_node.State.set_agent_position(agent_x, agent_y);
                        nodes_to_be_explored.Enqueue(child_node);
                    }
                }
                if (curent_node.State.agent_position.Item2 > 0)
                {
                    Node child_node = new Node(curent_node);
                    agent_x = curent_node.State.agent_position.Item1;
                    agent_y = curent_node.State.agent_position.Item2 - 1;
                    child_node.Action.Add("up");
                    if(!(test_marked_state(marked_positions, (agent_x, agent_y)))) { 
                        child_node.State.set_agent_position(agent_x, agent_y);
                        nodes_to_be_explored.Enqueue(child_node);
                    }
                }
                if (curent_node.State.agent_position.Item2 < 4)
                {
                    Node child_node = new Node(curent_node);
                    agent_x = curent_node.State.agent_position.Item1;
                    agent_y = curent_node.State.agent_position.Item2 + 1;
                    child_node.Action.Add("down");
                    if (!(test_marked_state(marked_positions, (agent_x, agent_y))))
                    {
                        child_node.State.set_agent_position(agent_x, agent_y);
                        nodes_to_be_explored.Enqueue(child_node);
                    }
                }

            }
            // Retourne une séquence d'actions vide si l'agent n'a pas trouvé de poussière ou un bijou
            return sequence_actions;
        }

        // Vérifie si une case à déjà été explorée
        public bool test_marked_state(List<(int,int)> marked_states, (int,int) agent_position)
        {
            foreach((int,int) marked_state in marked_states)
            {
                if(agent_position == marked_state)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
