using UnityEngine;


	public class raevynAnimator : MonoBehaviour
	{
		private Rigidbody2D m_rb;
		private PlayerControl m_controller;
		private Animator m_anim;
		private Transform playerTransform;
		private playerAiming playerAiming;
		[SerializeField] perfectDodge perfectDodge;
		[SerializeField] private GameObject proj;

		private static readonly int Move = Animator.StringToHash("Move");
		private static readonly int JumpState = Animator.StringToHash("JumpState");
		private static readonly int IsJumping = Animator.StringToHash("IsJumping");
		private static readonly int WallGrabbing = Animator.StringToHash("WallGrabbing");
		private static readonly int IsDashing = Animator.StringToHash("IsDashing");

		private void Start()
		{
			m_anim = GetComponent<Animator>();
			m_controller = GetComponentInParent<PlayerControl>();
			m_rb = GetComponentInParent<Rigidbody2D>();
			playerAiming = GetComponentInParent<playerAiming>();
			
		}

		private void Update()
		{
			// Idle & Running animation
			m_anim.SetFloat(Move, Mathf.Abs(m_rb.linearVelocity.x));
			
			
			// if(m_rb.velocity.x > 0) {
			// 	m_anim.SetInteger("Direction", 0);
			// }

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

			m_anim.SetBool("isGrounded", m_controller.isGrounded);

			// if(perfectDodge.dodgeBuffed) {
			// 	m_anim.SetFloat("attackSpeed", 4);
			// } else {
			// 	m_anim.SetFloat("attackSpeed", 2);
			// }
		}

		public void triggerShot() {
			playerAiming.InstantiateArrow();
		}

		public void triggerRangedShot() {
			playerAiming.InstantiateArrow(proj, 12f);
		}


		public void animateHit() {
			m_anim.SetTrigger("hit");
		}

		public void animateDeath() {
			m_anim.SetTrigger("Death");
		}

		public void InitiateDodgeBuff() {
			Debug.Log("wafaf");
			m_anim.SetBool("canPerfectDodge", false);
			perfectDodge.DodgeBuff();
		}
	}
