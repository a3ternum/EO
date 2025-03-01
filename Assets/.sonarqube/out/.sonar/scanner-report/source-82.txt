using UnityEngine;
using UnityEngine.UI;

public class MapDeviceUI : MonoBehaviour
{
    [SerializeField]
    private GameObject mapDeviceUIPanel;
    [SerializeField]
    private Image image;


    private void Start()
    {
        mapDeviceUIPanel.SetActive(false);
    }

    public void OpenUI()
    {
        mapDeviceUIPanel.SetActive(true);
    }

    public void CloseUI()
    {
        mapDeviceUIPanel.SetActive(false);
    }

    public void SetMapImage(Sprite mapImage)
    {
        image.enabled = true;
        image.sprite = mapImage;
    }

    public void RemoveMapImage()
    {
        image = null;
        image.enabled = false;
    }


}
