using UnityEngine;

public class destroyObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Destroy the GameObject immediately
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // You can also destroy it in Update if necessary
        // Destroy(gameObject);
    }
}