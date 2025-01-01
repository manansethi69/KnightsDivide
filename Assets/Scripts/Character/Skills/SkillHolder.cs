using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class SkillHolder : MonoBehaviour
{
    public Skill skill1, skill2, skill3;
    public Image skill1_icon, skill2_icon, skill3_icon;
    public GameObject skill1_Obj, skill2_Obj, skill3_Obj;

    // public float cooldownTime;
    // public float activeTime;
    public float cooldownReduction;
    private ButtonControl skill1_Key;
    private ButtonControl skill2_Key;
    private ButtonControl skill3_Key;
    public bool skilling;

    // enum SkillState {
    //     ready,
    //     active,
    //     cooldown
    // }

    // SkillState skill1_state = SkillState.ready;

    void Awake(){
        skill1_Key = Keyboard.current.digit1Key;
        skill2_Key = Keyboard.current.digit2Key;
        skill3_Key = Keyboard.current.digit3Key;
    }
    
    void Start()
    {
        skill1.skillObj = skill1_Obj;
        skill2.skillObj = skill2_Obj;
        skill3.skillObj = skill3_Obj;
    }

    void Update()
    {

        HandleSkill(skill1, skill1_Key, skill1_icon);
        HandleSkill(skill2, skill2_Key, skill2_icon);
        HandleSkill(skill3, skill3_Key, skill3_icon);
        // Debug.Log(skill1_state.ToString());

    }

    private void HandleSkill(Skill skill, ButtonControl btn, Image icon) {
        switch (skill.skillState) {
            case Skill.SkillState.ready:
                if(btn.wasPressedThisFrame) {
                    skill.Activate(gameObject, skill.skillObj);
                    skilling = true;
                    skill.skillState = Skill.SkillState.active;
                }
            break;
            case Skill.SkillState.active:
                if(skill.activeTimer > 0) {
                    skill.activeTimer -= Time.deltaTime;
                } else {
                    skill.BeginCooldown(gameObject, skill.skillObj, cooldownReduction);
                    skill.skillState = Skill.SkillState.cooldown;
                    skilling = false;
                    icon.fillAmount = 1;
                }
            break;
            case Skill.SkillState.cooldown:
                if(skill.cooldownTimer > 0) {
                    skill.cooldownTimer -= Time.deltaTime;
                    icon.fillAmount -= 1/skill.cooldownTimer * Time.deltaTime;
                } else {
                    icon.fillAmount = 0;
                    skill.skillState = Skill.SkillState.ready;
                }
            break;
        }        
    }

    // private void HandleSkillStates(SkillState skillState) {

    // }



    public void AdjustCooldownReduction(float cdRed) {
        cooldownReduction = cdRed;
    }

    
}
