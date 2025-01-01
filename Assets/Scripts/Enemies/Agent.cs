using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    private AgentAnimations agentAnimations;
    private AgentMover agentMover;
    public GameObject attackArea;
    private attack attack;
    private Block block;
    private Vector2 pointerInput, movementInput;
    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }  

    private void Update()
    {
        // pointerInput = GetPointerInput();
        // movementInput = movement.action.ReadValue<Vector2>().normalized;

        agentMover.MovementInput = movementInput;
        
        AnimateCharacter();
    }

    public void PerformAttack()
    {
        attack.Attack();
    }
    
    private void Awake()
    {
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        attack = GetComponentInChildren<attack>();
        agentMover = GetComponent<AgentMover>();
        block = GetComponentInChildren<Block>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation();
    
    }
}