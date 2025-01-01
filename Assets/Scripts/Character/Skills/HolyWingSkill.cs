using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class HolyWingSkill : Skill
{
    public int healthToRecover = 20;
    public override void Activate(GameObject parent, GameObject skill)
    {
        base.Activate(parent, skill);
        if(parent.name.Equals("Raevyn")) {
            parent.GetComponentInChildren<Animator>().SetTrigger("fireTornado");
        } else {
            parent.GetComponentInChildren<Animator>().SetTrigger("summonHoly");
        }
        
        parent.GetComponent<Health>().AdjustCurrentHealth(healthToRecover);
        skill.SetActive(true);
    }

    public override void BeginCooldown(GameObject parent, GameObject skill, float cdReduction) {
        base.BeginCooldown(parent, skill, cdReduction);
        skill.SetActive(false);
    }
}
