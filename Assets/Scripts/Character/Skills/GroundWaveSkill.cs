using UnityEngine;

[CreateAssetMenu]
public class GroundWaveSkill : Skill
{
    

    public override void Activate(GameObject parent, GameObject skill)
    {
        base.Activate(parent, skill);
        parent.GetComponentInChildren<Animator>().SetTrigger("groundWave");;
        skill.SetActive(true);
    }

    public override void BeginCooldown(GameObject parent, GameObject skill, float cdReduction) {
        base.BeginCooldown(parent, skill, cdReduction);
        skill.SetActive(false);
    }
}

