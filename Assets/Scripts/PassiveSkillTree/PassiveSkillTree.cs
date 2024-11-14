using UnityEngine;
using System.Collections.Generic;

public class PassiveSkillTree : MonoBehaviour
{
    private Player player;
    public List<Node> nodes = new List<Node>();
    public Node startNode;
    void Start()
    {
        FindPlayerWithDelay();
        
    }

    private void FindPlayerWithDelay()
    {
        Invoke("FindPlayerAndInitializeTree", 0.1f);
        
    }
    private void FindPlayerAndInitializeTree()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Player object not found in the scene.");
        }

        InitializeSkillTree();
    }

    private void InitializeSkillTree()
    {
        Debug.Log("Initializing skill tree...");
        // Automatically find and add all nodes that are children of the PassiveSkillTree GameObject
        nodes.AddRange(GetComponentsInChildren<Node>());

        // Link UI buttons to nodes
        LinkUIButtons();
        // Select start node (assuming the start node is named "Start")
        if (startNode != null)
        {
            SelectNode(startNode);
        }

 
    }

    private void LinkUIButtons()
    {
        foreach (var node in nodes)
        {

            var button = node.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(() => OnNodeSelected(node));
        }
    }


    private void OnNodeSelected(Node node)
    {
        if (CanSelectNode(node))
        {
            SelectNode(node);
        }
    }

    private bool CanSelectNode(Node node)
    {
        // Check if the node is connected to any selected node
        foreach (var n in nodes)
        {
            if (n.ConnectedNodes.Contains(node) && !n.GetComponent<UnityEngine.UI.Button>().interactable)
            {
                return true;
            }
        }
        return false;
    }

    private void SelectNode(Node node)
    { 
        node.ApplyEffect(player);
        // Mark node as selected (e.g., change button color)
        var button = node.GetComponent<UnityEngine.UI.Button>();
        button.interactable = false;
    }

}