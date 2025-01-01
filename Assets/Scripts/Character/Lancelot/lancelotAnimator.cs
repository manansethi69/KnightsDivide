using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SupanthaPaul
{
	public class lancelototAnimator : MonoBehaviour
	{
		private Rigidbody2D m_rb;
		private lancelotController m_controller;
		private Animator m_anim;
		private static readonly int Move = Animator.StringToHash("Move");
		private static readonly int JumpState = Animator.StringToHash("JumpState");
		private static readonly int IsJumping = Animator.StringToHash("IsJumping");
		private static readonly int WallGrabbing = Animator.StringToHash("WallGrabbing");
		private static readonly int IsDashing = Animator.StringToHash("IsDashing");
		private SkillHolder skillHolder;
		private void Start()
		{
			m_anim = GetComponentInChildren<Animator>();
			m_controller = GetComponent<lancelotController>();
			m_rb = GetComponent<Rigidbody2D>();
			skillHolder = GetComponent<SkillHolder>();
		}
		private static readonly int isRunning = Animator.StringToHash("isRunning");
		public int noOfClicks = 0;
		float lastClickedTime = 0;
    	float maxComboDelay = 0.5f;
		public float cooldownTime = 2f;
    	private float nextFireTime = 0f;
		public Block block; 
		private void Update()
		{
			// Idle & Running animation
			if(Input.GetButton("Horizontal")){
				m_anim.SetBool("isWalking", true);
				
			}
			else{
				m_anim.SetBool("isWalking", false);
			}

			// Jump state (handles transitions to falling/jumping)
			float verticalVelocity = m_rb.linearVelocity.y;
			m_anim.SetFloat(JumpState, verticalVelocity);

			// Jump animation
			if (!m_controller.isGrounded && !m_controller.actuallyWallGrabbing)
			{
				m_anim.SetBool(IsJumping, true);
			}
			else
			{
				m_anim.SetBool(IsJumping, false);
			}

			if(!m_controller.isGrounded && m_controller.actuallyWallGrabbing)
			{
				m_anim.SetBool(WallGrabbing, true);
			} else
			{
				m_anim.SetBool(WallGrabbing, false);
			}

			// dash animation
			m_anim.SetBool(IsDashing, m_controller.isDashing);

			if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("attack_1"))
        	{
        	    m_anim.SetBool("attack_1", false);
        	}
			else{
			}

			if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("attack_2") || m_anim.GetCurrentAnimatorStateInfo(0).IsName("attack_1") || m_anim.GetCurrentAnimatorStateInfo(0).IsName("block(block)") || skillHolder.skilling)
        	{
				m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        	}
			else{
				m_rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
			}
			
        	


			if (Time.time - lastClickedTime > maxComboDelay)
        	{
        	    noOfClicks = 0;

        	}
			if(noOfClicks == 0){
				m_anim.SetBool("attack_2", false);
			}

			//cooldown time
        	if (Time.time > nextFireTime)
        	{
        	    // Check for mouse input
        	    if (Input.GetKeyDown(KeyCode.Z))
        	    {
        	        OnClick();
        	    }
        	}
			if (Input.GetKey(KeyCode.LeftShift)){
				Sprint();
			}
			else{
				m_anim.SetBool("run", false);
			}
			// if(!m_controller.isRunning){
			// 	m_anim.SetBool("isRunning(loop)", false);
			// 	m_anim.SetBool("isRunning(End)", true);
			// }
			// if(m_anim.GetCurrentAnimatorStateInfo(0).IsName("run(end)")){
			// 	m_anim.SetBool("isRunning(End)", false);
			// }
			
			if(Input.GetKey(KeyCode.C)){
				Block();
			}
			else{
				m_anim.SetBool("isBlocking", false);
				m_anim.SetTrigger("block(end)");
			}
		}
		void Block(){
			if(!m_anim.GetCurrentAnimatorStateInfo(0).IsName("block(start)") && !m_anim.GetCurrentAnimatorStateInfo(0).IsName("block(block)") && !m_anim.GetCurrentAnimatorStateInfo(0).IsName("special") && !m_anim.GetCurrentAnimatorStateInfo(0).IsName("hit")){
				m_anim.SetTrigger("block(start)");
			}
			m_anim.SetBool("isBlocking", true);
		}
		void Sprint(){
			// if(!m_anim.GetCurrentAnimatorStateInfo(0).IsName("run(start)") && !m_anim.GetCurrentAnimatorStateInfo(0).IsName("run(loop)")){
			// 	m_anim.SetBool("isRunningStart", true);
			// }
			// if(m_anim.GetCurrentAnimatorStateInfo(0).IsName("run(start)")){
			// 	m_anim.SetBool("isRunningStart", false);
			// 	m_anim.SetBool("isRunning(loop)", true);
			// }
			// if(m_controller.isRunning && !m_anim.GetCurrentAnimatorStateInfo(0).IsName("run(start)")){
			// 	m_anim.SetBool("isRunning(loop)", true);
			// }
			m_anim.SetBool("run", true);
		}
		void OnClick()
    	{
        // 	//so it looks at how many clicks have been made and if one animation has finished playing starts another one.
       		lastClickedTime = Time.time;
			noOfClicks++;
       		if (noOfClicks >= 1)
       		{
				if(!m_anim.GetCurrentAnimatorStateInfo(0).IsName("attack_2")){
		
					m_anim.SetBool("attack_1", true);
				}
				if(m_anim.GetCurrentAnimatorStateInfo(0).IsName("attack_2")){
				
					m_anim.SetBool("attack_1", true);
					m_anim.SetBool("attack_2", true);
				}
       		}
       		noOfClicks = Mathf.Clamp(noOfClicks, 0, 2);
 
       		if (noOfClicks >= 2 && m_anim.GetCurrentAnimatorStateInfo(0).IsName("attack_1"))
       		{
       		    m_anim.SetBool("attack_1", false);
       		    m_anim.SetBool("attack_2", true);
       		}
    	}
		public void hit(){
			m_anim.SetTrigger("hit");
		}

		public void death(){
			m_anim.SetTrigger("death");
		}
	}
}

