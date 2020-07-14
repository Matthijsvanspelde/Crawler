using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    public GameObject menu;
    private bool isShowing = false;

    private void Start()
    {
        menu.SetActive(isShowing);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}
