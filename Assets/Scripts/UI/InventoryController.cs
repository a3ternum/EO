using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryPage inventoryPage;

    public int inventorySize = 54;
    private void Start()
    {
        inventoryPage.InitializeInventory(inventorySize);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPage.isActiveAndEnabled == false)
            {
                inventoryPage.Show();
            }
            else
            {
                inventoryPage.Hide();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inventoryPage.isActiveAndEnabled == true)
            {
                inventoryPage.Hide();
            }
        }
    }
}
