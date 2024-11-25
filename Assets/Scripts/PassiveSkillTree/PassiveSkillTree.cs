using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class PassiveSkillTree : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public static PassiveSkillTree Instance { get; private set; }

    private Player player;
    public List<Node> nodes = new List<Node>();
    public Node startNode;
    private CanvasGroup canvasGroup;
    private bool isSkillTreeVisible = false;
    private Vector3 lastMousePosition;
    private float dragSensitivity = 0.05f; // Higher values make the skill tree move faster

    [SerializeField]
    private PassiveSkillButton passiveSkillButton;

    public RectTransform canvasBounds; // Define the bounds for the canvas movement

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
        if (node == startNode)
        {
            node.ApplyEffect(player);
            // Mark node as selected (e.g., change button color)
            var startButton = node.GetComponent<UnityEngine.UI.Button>();
            startButton.interactable = false;
            return;
        }
        // remove skill point
        --player.playerStats.availableSkillPoints;
        // add total skill point
        ++player.playerStats.totalSkillPoints;

        // remove skill point from button
        passiveSkillButton.RemoveSkillPoint();

        // disable button if no skill points are available
        if (player.playerStats.availableSkillPoints == 0)
        {
            passiveSkillButton.HideButton();
        }
        if (player.playerStats.availableSkillPoints < 0)
        {
            Debug.LogError("Available skill points cannot be negative. we now have: " + player.playerStats.availableSkillPoints + " skill points");
        }

        // Apply node effect
        node.ApplyEffect(player);
        // Mark node as selected (e.g., change button color)
        var button = node.GetComponent<UnityEngine.UI.Button>();
        button.interactable = false;
    }

    public void ToggleSkillTree()
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


    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 delta = (Input.mousePosition - lastMousePosition) * dragSensitivity;
        Vector3 newPosition = transform.position + delta;

        // Clamp the new position within the bounds
        newPosition.x = Mathf.Clamp(newPosition.x, canvasBounds.rect.xMin, canvasBounds.rect.xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, canvasBounds.rect.yMin, canvasBounds.rect.yMax);

        transform.position = newPosition;
        lastMousePosition = Input.mousePosition;
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