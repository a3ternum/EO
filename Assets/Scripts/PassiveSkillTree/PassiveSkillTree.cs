using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class PassiveSkillTree : MonoBehaviour
{
    public static PassiveSkillTree Instance { get; private set; }

    private Player player;
    public List<Node> nodes = new List<Node>();
    public Node startNode;
    private CanvasGroup canvasGroup;
    private bool isSkillTreeVisible = false;
    private Vector3 lastMousePosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

    }

    private void Start()
    {
        FindPlayerWithDelay();
        SetSkillTreeVisibility(false);
    }

    private void FindPlayerWithDelay()
    {
        Invoke("FindPlayerAndInitializeTree", 0.1f);
        
    }
    private void FindPlayerAndInitializeTree()
    {
        player = FindFirstObjectByType<Player>();
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
            if (n.ConnectedNodes.Contains(node) && !n.GetComponent<UnityEngine.UI.Button>().interactable 
                && player.playerStats.availableSkillPoints > 0)
            {
                return true;
            }
        }
        return false;
    }

    private void SelectNode(Node node)
    {
        // remove skill point
        player.playerStats.availableSkillPoints--;
        player.playerStats.totalSkillPoints++;
        // Apply node effect
        node.ApplyEffect(player);
        // Mark node as selected (e.g., change button color)
        var button = node.GetComponent<UnityEngine.UI.Button>();
        button.interactable = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        transform.position += delta;
        lastMousePosition = Input.mousePosition;

    }

    private void ToggleSkillTree()
    {
        isSkillTreeVisible = !isSkillTreeVisible;
        SetSkillTreeVisibility(isSkillTreeVisible);
        if (player != null)
        {
            player.EnablePlayerActions(!isSkillTreeVisible);
        }
        if (isSkillTreeVisible)
        {
            transform.SetAsLastSibling(); // Bring the skill tree to the front
        }
        else
        {
            transform.SetAsFirstSibling(); // Send the skill tree to the back
        }

    }
   
    private void SetSkillTreeVisibility(bool isVisible)
    {
        canvasGroup.alpha = isVisible ? 1 : 0;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleSkillTree();
        }

        if (Input.GetMouseButtonDown(0) || isSkillTreeVisible)
        {
            lastMousePosition = Input.mousePosition;
        }
    }
}