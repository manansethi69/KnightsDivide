using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, OnAttackPeformed, OnBlocked, OnRangedAttack, OnDash, OnHitProtection;

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPeformed?.Invoke();
    }

    public void TriggerBlock(){
        OnBlocked?.Invoke();
    }

    public void TriggerRangedAttack(){
        OnRangedAttack?.Invoke();
    }

    public void triggerdashAttack(){
        OnDash?.Invoke();
    }

    public void resetHitProtection(){
        OnHitProtection?.Invoke();
    }
}