using UnityEngine;

public class Skill : ScriptableObject
{
    public new string name;
    public float baseCooldown;
    public float currentCooldown;
    public float cooldownTimer;
    public float activeTimer;
    public float activeTime;
    public GameObject skillObj;

    public enum SkillState {
        ready,
        active,
        cooldown
    }

    public SkillState skillState = SkillState.ready;

    public virtual void Activate(GameObject parent, GameObject skill) {
        activeTimer = activeTime;
    }

    public virtual void BeginCooldown(GameObject parent, GameObject skill, float cdReduction) {
        currentCooldown =  baseCooldown + cdReduction;
        cooldownTimer = currentCooldown;
    }
    

}
