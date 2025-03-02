using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDescriptionUI : MonoBehaviour
{
    public static ItemDescriptionUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemStats;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void ShowDescription(Item item)
    {
        if (item == null)
        {
            HideDescription(); 
            return;
        }


        itemName.text = item.itemName;
        itemStats.text = item.itemStats;
    }

    public void HideDescription()
    {
        itemName.text = string.Empty;
        itemStats.text = string.Empty;


    }
}
