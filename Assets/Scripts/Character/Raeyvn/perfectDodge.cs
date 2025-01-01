using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class perfectDodge : MonoBehaviour
{
    private float collisionTimer;
    private float buffTimer = 0f;
    private bool canPerfectDodge = false;
    public bool dodgeBuffed = false;
    [SerializeField] private float dodgeWindow = 3f;
    [SerializeField] private float buffWindow = 5f;
    InputManager inputManager;

    [SerializeField] private Animator raevynAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(canPerfectDodge) {
            raevynAnimator.SetBool("canPerfectDodge", true);
            collisionTimer += Time.deltaTime;
            if(collisionTimer > dodgeWindow) {
                canPerfectDodge = false;
                collisionTimer = 0;
            }
        } else {
            raevynAnimator.SetBool("canPerfectDodge", false);
        }

        if(Keyboard.current.gKey.wasPressedThisFrame) {
            canPerfectDodge = true;
        }

        if(dodgeBuffed) {
            Debug.Log(buffTimer);
            buffTimer += Time.deltaTime;
            if(buffTimer > buffWindow) {
                dodgeBuffed = false;
                buffTimer = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.collider.tag == "attack or projectile") {
        //     canPerfectDodge = true;
        // }

        canPerfectDodge = true;
        Debug.Log("wat");
    }

    public bool PerfectDodge() {
        return canPerfectDodge;
    }

    public void DodgeBuff () {
        dodgeBuffed = true;
    }
}
