using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
public class Node : MonoBehaviour
{
    public NodeEffect nodeEffect;
    public List<Node> ConnectedNodes = new List<Node>();
 

    public Node(string name, NodeEffect effect)
    {
        nodeEffect = effect;
    }
    public void ApplyEffect(Player player)
    {
        if (nodeEffect != null)
        {
            nodeEffect.ApplyEffect(player);
        }
    }

}
