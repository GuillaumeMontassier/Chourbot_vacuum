using System;


public class Agent
{
	public Expand(node, problem)
	{
		
		var successor;

		foreach (action, result in Successor-Fn(problem, State[node]))
		{
			var s = new Node;
			s.Parent-Node = node;
			s.Action = action; 
			s.State = result;
			s.Path_Cost = node.Path_Cost + node.Step_Cost(node, action, s);
			s.Depth = node.Depth + 1;

			successor += s;
		}

		return successor;
}
