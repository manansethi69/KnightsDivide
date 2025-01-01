using UnityEngine;

public class exp : MonoBehaviour
{
    public int numOfOrbs;
    private Health health;
    public GameObject orb;
    private bool done = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.isDead && !done){
            dropOrb();
        }
    }

    private void dropOrb(){
        done = true;
        for(int i = 0; i < numOfOrbs; i++){
            Instantiate(orb, transform.position, transform.rotation);
        }
    }
}
