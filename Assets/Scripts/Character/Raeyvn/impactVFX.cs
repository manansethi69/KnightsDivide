using Unity.VisualScripting;
using UnityEngine;

public class impactVFX : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.5f);
    }
}
