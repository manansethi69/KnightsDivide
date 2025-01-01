using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{   
    [SerializeField] private InputActionAsset playerControls;
    private readonly String actionMapName = "Player";
    private readonly String move = "Move";
    private readonly String look = "Look";
    private readonly String dash = "Dash";
    private readonly String jump = "Jump";
    private readonly String shootUp = "ShootUp";
    private readonly String shootDown = "ShootDown";

    private InputAction moveInput;
    private InputAction lookInput;
    private InputAction jumpInput;
    private InputAction dashInput;
    private InputAction slideInput;
    private InputAction shootUpInput;
    private InputAction shootDownInput;


    public static InputManager Instance { get; private set;}

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InputActionMap actionMap = playerControls.FindActionMap(actionMapName);
        moveInput = actionMap.FindAction(move);
        lookInput = actionMap.FindAction(look);
        dashInput = actionMap.FindAction(dash);
        jumpInput = actionMap.FindAction(jump);
        shootUpInput = actionMap.FindAction(shootUp);
        shootDownInput = actionMap.FindAction(shootDown);
        RegisterActions();
    }

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool DashTriggered { get; private set; }
    public bool ShotUp { get; private set; }
    public bool ShotDown { get; private set;}

    private void RegisterActions() {
        moveInput.performed += _ => MoveInput = _.ReadValue<Vector2>();
        moveInput.canceled += _ => MoveInput = Vector2.zero;

        lookInput.performed += _ => LookInput = _.ReadValue<Vector2>();
        lookInput.canceled += _ => LookInput = Vector2.zero;

        dashInput.performed += _ => DashTriggered = true;
        dashInput.canceled += _ => DashTriggered = false;
        
        //this jump is for continuous input;
        // jumpInput.performed += _ => JumpTriggered = true;
        // jumpInput.canceled += _ => JumpTriggered = false;

        shootUpInput.performed += _ => ShotUp = true;
        shootUpInput.canceled += _ => ShotUp = false;

        shootDownInput.performed += _ => ShotDown = true;
        shootDownInput.canceled += _ => ShotDown = false;
    }

    void Update() {
        //jump for single input --> works better with wall climbing
        if(jumpInput.WasPerformedThisFrame()) {
            JumpTriggered = true;
        }else {
            JumpTriggered = false;
        }
    }

    private void OnEnable()
    {
        moveInput.Enable();
        lookInput.Enable();
        dashInput.Enable();
        jumpInput.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        lookInput.Disable();
        dashInput.Disable();
        jumpInput.Disable();
    }

    public bool pressingD(){
        if(moveInput.ReadValue<Vector2>().x > 0) {
            return true;
        } else {
            return false;
        }
    }
}
