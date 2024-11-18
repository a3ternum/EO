using UnityEngine;
using UnityEngine.UI; // If using standard UI Text
using TMPro; // If using TextMesh Pro
using UnityEngine.EventSystems;

public class ButtonColorChangeEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator animator;
    private TMP_Text textComponent;  // Use Text instead if not using TMP

    private Color defaultColor;
    public Color hoverColor = Color.yellow; // Color when hovered
    public Color clickColor = Color.red;  // Color when clicked

    void Start()
    {
        animator = GetComponent<Animator>();
        textComponent = GetComponentInChildren<TMP_Text>();  // Get the TMP_Text component, or Text for normal UI Text

        if (textComponent != null)
            defaultColor = textComponent.color; // Save the default color
    }

    // On hover, change color to the hover color
    public void OnPointerEnter(PointerEventData eventData)
    {
        //textComponent.color = hoverColor; // Change text color when hovered
        //animator.SetTrigger("Hover");  // Trigger the hover animation
    }

    // On exit, revert to the default color
    public void OnPointerExit(PointerEventData eventData)
    {
        //textComponent.color = defaultColor; // Revert text color back
    }

    // On click, change the text color to the clicked color
    public void OnPointerClick(PointerEventData eventData)
    {
        textComponent.color = clickColor; // Change text color when clicked
        animator.SetTrigger("Click");  // Trigger the click animation (if needed, or just visual feedback)
        Invoke("ResetTextColor", 1f); // Optionally, reset after a short delay
    }

    // Optionally reset color after a short delay (like a flash)
    private void ResetTextColor()
    {
        textComponent.color = defaultColor; // Reset color to default after click
    }
}