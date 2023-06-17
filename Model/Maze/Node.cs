using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
	public int x;
	public int y;
	public Node parent;

	public Node(int x, int y, Node parent)
	{
		this.x = x;
		this.y = y;
		this.parent = parent;
	}

	public Node(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}
