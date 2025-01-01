using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class FireTornado : Skill
{
    public GameObject tornado;
    public override void Activate(GameObject parent, GameObject skill)
    {
        base.Activate(parent, skill);
        parent.GetComponentInChildren<Animator>().SetTrigger("fireTornado");
        // skill.SetActive(true);
        parent.GetComponent<playerAiming>().InstantiateArrow(tornado, 5f);
        parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void BeginCooldown(GameObject parent, GameObject skill, float cdReduction) {
        base.BeginCooldown(parent, skill, cdReduction);
        // skill.SetActive(false);
        parent.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }
}
