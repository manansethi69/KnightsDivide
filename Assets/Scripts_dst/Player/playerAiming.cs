using System;
using System.Collections;
using SupanthaPaul;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerAiming : MonoBehaviour
{

    [SerializeField] private PlayerControl controller;
    private Animator animator;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projSpawnPos;
    [SerializeField] private Transform projSpawnPosMoving;


    private GameObject projInstance;
    private Vector2 worldPos;
    private Vector2 aimDirection; 
    private float weaponAngle;
    [SerializeField] private float attackSpeed = 0.3f;
    private float atkSpeedBoost = 0f;
    private int atkDamageBoost = 0;
    [SerializeField] private float arrowSpeed = 15f;
    private InputManager inputManager;
    private raevynAnimator raevynAnimator;

    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controller = GetComponent<PlayerControl>();
        animator = GetComponentInChildren<Animator>();
        inputManager = InputManager.Instance;
        raevynAnimator = GetComponentInChildren<raevynAnimator>();
        
    }

    void Start() {
        // animator.SetFloat("attackSpeed", attackSpeed + atkSpeedBoost);
    }

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
        RotatePlayer();
        Shoot();
    }

    private void RotateWeapon() {
        //rotate gun towards mouse pos
        worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        aimDirection = (worldPos - (Vector2)projSpawnPos.transform.position).normalized;
        // projSpawnPos.transform.right = weaponDirection;
        
        //flip weapon
        weaponAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        
        // Vector3 localScale = new Vector3(0.6896043f, 0.6896043f, 0.6896043f);

        // if(weaponAngle >= 90 || weaponAngle <= -90) {
        //     localScale.x = localScale.x * -1;

        // } else {
        //     localScale.x = Mathf.Abs(localScale.x);
        // }

        // Debug.Log(weaponAngle);

        // weapon.transform.localScale = localScale;

    }

    private void RotatePlayer() {
        // if(transform.position.x > worldPos.x) {
        //     // localScale.x = localScale.x * -1;
        //     if(controller.m_facingRight) {
        //         controller.Flip();
        //     }
        // } else {
        //     // localScale.x = Mathf.Abs(localScale.x);
        //     if(!controller.m_facingRight) {
        //         controller.Flip();
        //     }
        // }
        // Flipping
		if (!controller.m_facingRight && controller.moveInput > 0f)
			controller.Flip();
		else if (controller.m_facingRight && controller.moveInput < 0f)
			controller.Flip();

    }

    private void Shoot() {
        if(Keyboard.current.zKey.wasPressedThisFrame) {
            //spawn projectile
            animator.SetTrigger("Ranged");
            // StartCoroutine(WaitForAnimationFinished());
            
        }

        // if(Keyboard.current.zKey.wasPressedThisFrame) {
        //     //shooting up
        //     animator.SetTrigger("RangedUp");
        // }

        // if(Keyboard.current.xKey.wasPressedThisFrame) {
        //     //shooting down
        //     animator.SetTrigger("RangedDown");

        // }
    }

    public void InstantiateArrow() {
        // if(weaponAngle <= 45 && weaponAngle >= -45) {
        //     projInstance = Instantiate(projectile, projSpawnPos.position, projSpawnPos.transform.rotation);
        // } else if(weaponAngle > 45) {
        //     projInstance = Instantiate(projectile, projSpawnPos.position, projSpawnPos.transform.rotation);
        // }else if(weaponAngle < -45) {
        //     projInstance = Instantiate(projectile, projSpawnPos.position, projSpawnPos.transform.rotation);
        // }
        float attackDir = controller.m_facingRight ? 1f : -1f;
        Vector2 launchVelocity = new Vector2(arrowSpeed * attackDir, 0);
        projectile.GetComponent<arrowDamage>().damage = damage;
        if(controller.moveInput == 0) {
            projInstance = Instantiate(projectile, projSpawnPos.transform.position, transform.rotation);
        } else {
            projInstance = Instantiate(projectile, projSpawnPosMoving.transform.position, transform.rotation);
        }
        
        if(projInstance != null) {
            projInstance.GetComponent<Rigidbody2D>().linearVelocity = launchVelocity;
            projInstance.GetComponent<arrowDamage>().dmgBoost = atkDamageBoost;
        }
        
        
        
    }

    public void InstantiateArrow(GameObject proj, float projVel) {
        // if(weaponAngle <= 45 && weaponAngle >= -45) {
        //     projInstance = Instantiate(projectile, projSpawnPos.position, projSpawnPos.transform.rotation);
        // } else if(weaponAngle > 45) {
        //     projInstance = Instantiate(projectile, projSpawnPos.position, projSpawnPos.transform.rotation);
        // }else if(weaponAngle < -45) {
        //     projInstance = Instantiate(projectile, projSpawnPos.position, projSpawnPos.transform.rotation);
        // }
        float attackDir = controller.m_facingRight ? 1f : -1f;
        Vector2 launchVelocity = new Vector2(projVel * attackDir, 0);
        projectile.GetComponent<arrowDamage>().damage = damage;

        Vector3 tempPos = projSpawnPos.transform.position;

        if(proj.name.Equals("FireTornado")) {
            tempPos.y = tempPos.y - 0.6f;
        }

        
        if(controller.moveInput == 0) {
            projInstance = Instantiate(proj, tempPos, transform.rotation);
        } else {
            projInstance = Instantiate(proj, projSpawnPosMoving.transform.position, transform.rotation);
        }
        
        if(projInstance != null) {
            projInstance.GetComponent<Rigidbody2D>().linearVelocity = launchVelocity;
        }
        
        
        
    }

    // public IEnumerator WaitForAnimationFinished()
    // {   
    //     yield return new WaitUntil( delegate { return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_E_clip"); });
    //     Debug.Log("faf");
    //     projInstance = Instantiate(projectile, projSpawnPos.position, weapon.transform.rotation);
    // }

    public void AdjustAttackSpeed(float boost) {
        atkSpeedBoost = boost;
        animator.SetFloat("attackSpeed", (attackSpeed + boost));
    }


    public float GetAttackSpeed() {
        return attackSpeed;
    }

    public void AdjustArrowDmgBoost(int boost) {
        atkDamageBoost = boost;
    }
}
