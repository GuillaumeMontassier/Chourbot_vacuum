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
        // List<Case> belief = new List<Case>();

        State belief = new State();

        // Desire, Trouver une case non vide is_there_an_object = true
        // Case goal = new Case();
        bool is_there_an_object = true;

        // Intention
        // public List<String> actions = new List<String>(new string[] { "haut", "bas", "gauche", "droite"});

        public List<String> possible_actions = new List<String>(new string[] { "aspirer","ramasser" });






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
        }

        public Case get_vacuum_case()
        {
            return vacuum_case;
        }


        /*        public void move(Case new_case)
                {
                    vacuum_case.set_is_vacuum(false);
                    vacuum_case = new_case;
                    new_case.set_is_vacuum(true);
                }*/

        public State move(List<String> actions, Case[,] cases, State actual_state)
        {
            int x = actual_state.get_position().Item1;
            int y = actual_state.get_position().Item2;
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
                actual_state.set_agent_position(x, y);
            }
            return actual_state;
        }



        /*      public void move(Case new_case)
              {
                  vacuum_case.set_is_vacuum(false);
                  vacuum_case = new_case;
                  new_case.set_is_vacuum(true);
              }*/

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

        // Deuxième fonction move qui renvoie la nouvelle position de l'agent
        public (int,int) move_agent((int,int) vacuum_position ,String new_action)
        {
            int new_x = 0;
            int new_y = 0;

            if (new_action == "haut")
            {
                // new_y = get_vacuum_case().y - 1;
                new_y = vacuum_position.Item2 - 1;
            }
            else if (new_action == "bas")
            {
                // new_y = get_vacuum_case().y + 1;
                new_y = vacuum_position.Item1 + 1;
            }
            else if (new_action == "gauche")
            {
                // new_x = get_vacuum_case().x - 1;
                new_x = vacuum_position.Item1 - 1;
            }
            else if (new_action == "droite")
            {
                // new_x = get_vacuum_case().x + 1;
                new_x = vacuum_position.Item1 + 1;
            }
            return (new_x, new_y);
        }

        // AlgorithmeS d'exploration

        // Renvoie une liste d'état associé avec une action
        private List<(String, State)> successor(State actual_state)
        {
            List<(String, State)> set_actions_states = new List<(String, State)>();
            // Listes d'états (new_states) après exécution de l'actions du même index
            List<String> new_actions = new List<String>();

            foreach (String action in problem.actions)
            {
                if ((actual_state.agent_position.Item2 <= 0) && (action == "haut"))
                {
                    continue;
                }
                else if ((actual_state.agent_position.Item2 >= 5) && (action == "bas"))
                {
                    continue;
                }
                else if ((actual_state.agent_position.Item1 <= 0) && (action == "gauche"))
                {
                    continue;
                }
                else if ((actual_state.agent_position.Item1 >= 5) && (action == "droite"))
                {
                    continue;
                }
                State new_state = new State();
                // new_state.grid_state = actual_state.grid_state;
                new_state.dust_position = new List<(int, int)>(actual_state.dust_position);
                new_state.jewelry_position = new List<(int, int)>(actual_state.jewelry_position);

                // move agent (vacuum)
                new_state.agent_position = move_agent(actual_state.agent_position,action);

                set_actions_states.Add((action, new_state));
            }
            return set_actions_states;
        }


        // Fonction d'expansion de l'arbre
/*        public List<Node> Expand(Node node, Problem probem)
        {
            List<Node> successors = new List<Node>();
            foreach ((String, State) action_result in successor(node.State))
            {
                Node s = new Node();
                s.Parent_Node = node;
                s.Action = action_result.Item1;
                s.State = action_result.Item2;
                s.Path_Cost = node.Path_Cost + 1;
                successors.Add(s);
            }
            return successors;
        }*/

        // Breadth First Search (BFS)
        /*public void explorationBFS(List<(int,int)> cases)*/
/*        public List<String> explorationBFS(Case[,] cases, Problem problem)
        {
            // Listes d'actions à retourner
            List<String> sequence_actions = new List<string>();

            // Liste des états à explorer
            Queue < State > states_to_be_explored = new Queue<State>();

            states_to_be_explored.Enqueue(problem.initial_State);
            *//*states_to_be_explored.Enqueue(cases[0,0]);*//*

            int agent_x;
            int agent_y;

            *//*foreach ( State state in states_to_be_explored)*//*
            while(states_to_be_explored.Count != 0 )
            {
                belief = states_to_be_explored.Dequeue();

                // Action à effectuer sur la case
                foreach ((int,int) dust in belief.dust_position){
                    if(dust == belief.agent_position)
                    {
                        clean_case();
                    }
                }
                // Ajout des états possibles à partir de l'état courant
                if (belief.agent_position.Item1 > 0)
                {
                    agent_x = belief.agent_position.Item1 - 1;
                    State new_state = new State(belief);
                    new_state.set_agent_position(agent_x, belief.agent_position.Item2);
                    states_to_be_explored.Enqueue(new_state);
                }
                if (belief.agent_position.Item1 < 5)
                {
                    agent_x = belief.agent_position.Item1 + 1;
                    State new_state = new State(belief);
                    new_state.set_agent_position(agent_x, belief.agent_position.Item2);
                    states_to_be_explored.Enqueue(new_state);
                }
                if (belief.agent_position.Item2 > 0)
                {
                    agent_y = belief.agent_position.Item2 - 1;
                    State new_state = new State(belief);
                    new_state.set_agent_position(belief.agent_position.Item1, agent_y);
                    states_to_be_explored.Enqueue(new_state);
                }
                if (belief.agent_position.Item2 < 5)
                {
                    agent_y = belief.agent_position.Item1 + 1;
                    State new_state = new State(belief);
                    new_state.set_agent_position(belief.agent_position.Item1, agent_y);
                    states_to_be_explored.Enqueue(new_state);
                }
            }
            return sequence_actions;
        }*/



        public List<String> explorationBFS2(Case[,] cases, Problem problem, State actualState)
        {
            // Listes d'actions à retourner
            List<String> sequence_actions = new List<string>();

            // Liste de coordonner marqué
            List<(int, int)> marked_positions = new List<(int, int)>();

            // Noeud courant
            /*Node curent_node = new Node(new State(cases[0,0]));*/
            Node curent_node = new Node(actualState);

            // Liste des états à explorer
            Queue<Node> nodes_to_be_explored = new Queue<Node>();

            nodes_to_be_explored.Enqueue(curent_node);
            /*states_to_be_explored.Enqueue(cases[0,0]);*/

            int agent_x = 0;
            int agent_y = 0;
            int i = 0;

            /*foreach ( State state in states_to_be_explored)*/
            while (nodes_to_be_explored.Count != 0)
            {
                curent_node = nodes_to_be_explored.Dequeue();
                /*curent_node.State.agent_case.marked = true;*/
                marked_positions.Add(curent_node.State.agent_position);

                i++;
                /*Console.WriteLine("agent_x : " + agent_x +","+ agent_y);*/
/*                if (i >= 5)
                {
                    Console.WriteLine("Position agent : " + curent_node.State.agent_position);
                    Console.WriteLine("number of node : " + nodes_to_be_explored.Count);
                    foreach (Node node in nodes_to_be_explored)
                    {
                        Console.WriteLine("node : " + node.State.agent_position);
                    }
                    return curent_node.Action;
                }*/
                // Action à effectuer sur la case
                foreach ((int, int) dust in actualState.dust_position)
                {
                    if (dust == curent_node.State.agent_position)
                    {
                        /*clean_case();*/

                        Console.WriteLine("Poussière trouvée !" + curent_node.State.agent_position);
                        return curent_node.Action;
                        //return curent_node.Action;
                    }
                }
                // Définition du nouveau noeud
                /*Node child_node = new Node(curent_node);*/

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
                    /*if (!(cases[agent_x, agent_y].marked)) {*/
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

            return curent_node.Action;
        }


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



        /*        public void test_exploration(Case[,] cases, Problem problem)
                {
                    Queue<State> states_to_be_explored = new Queue<State>();

                    states_to_be_explored.Enqueue(new State(cases[1,1]));
                    states_to_be_explored.Enqueue(problem.initial_State);

                    while (states_to_be_explored.Count != 0)
                    {
                        belief = states_to_be_explored.Dequeue();
                        move(belief.agent_case);
                        Thread.Sleep(2000);
                    }
                }*/
    }

}
