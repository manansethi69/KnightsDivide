using UnityEngine;
using UnityEngine.InputSystem;

public class UIInput : MonoBehaviour
{
    [SerializeField] private GameObject statsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame) {
            if(!statsMenu.activeSelf) {
                statsMenu.SetActive(true);
            } else {
                statsMenu.SetActive(false);
            }
        }
    }

    public void SetStatsMenu(GameObject menu) {
        statsMenu = menu;
    }
}
