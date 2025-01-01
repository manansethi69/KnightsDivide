using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class MagicArrow : Skill
{
    private GameObject currentSkill;
    private GameObject skillParent;

    public override void Activate(GameObject parent, GameObject skill)
    {
        base.Activate(parent, skill);
        currentSkill = skill;
        parent.GetComponentInChildren<Animator>().SetTrigger("magicArrow");
    }

    public override void BeginCooldown(GameObject parent, GameObject skill, float cdReduction) {
        base.BeginCooldown(parent, skill, cdReduction);
    }
}
