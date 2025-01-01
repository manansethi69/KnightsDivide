using UnityEngine;

public class ControlsPanelController : MonoBehaviour
{
    public GameObject controlsPanel; // Reference to the Controls Panel

    public void ToggleControlsPanel()
    {
        if (controlsPanel != null)
        {
            // Toggle the panel's active state
            controlsPanel.SetActive(!controlsPanel.activeSelf);
        }
    }
    public void CloseControlsPanel()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }
    }

}

