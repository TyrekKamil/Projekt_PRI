using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public Animator animator;
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[SerializeField] private int extraJumpsValue = 1;							// Amount of extra jumps
	[Range(0, 1f)] [SerializeField] private float extraJumpDifficulty = 0.9f;   //difficulty of the extra jumps	
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsWater;
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_Swimming;            // Whether or not the player is in water.
										//const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.	
	public int extraJumps;
	private bool onMovingPlatform = false;
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]
	MovingPlatform currentPlatform;
	public UnityEvent OnLandEvent;

	//[System.Serializable]
	//public class BoolEvent : UnityEvent<bool> { }
	//^Probably to use when making wall jump

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		extraJumps = extraJumpsValue;
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		//TODO: OnWallEvent = new BoolEvent();
	}
	int i = 0;
	private void FixedUpdate()
	{
		if (CanUseTripleJump()) {
			extraJumpsValue = 2;
		}
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject.name.Contains("Moving Platform")) {
				onMovingPlatform = true;
				currentPlatform = colliders[i].gameObject.GetComponent<MovingPlatform>();
			}
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
			if (!wasGrounded)
			{
				animator.SetBool("IsJumping", false);
			}

		}

		bool wasInWater = m_Swimming;
		wasInWater = false;

		Collider2D[] waterColliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsWater);
		for (int i = 0; i < waterColliders.Length; i++)
		{
			if (waterColliders[i].gameObject != gameObject)
			{
				animator.SetBool("IsSwimming", true);
				m_Swimming = true;
			}
			else
			{
				animator.SetBool("IsSwimming", false);
			}

		}
	}

	public void Move(float move, bool jump)
	{
		if (!m_Grounded && Statics.isOnRope) {
			animator.SetBool("IsHanging", true);
		}
		if (!Statics.isOnRope) {
			animator.SetBool("IsHanging", false);
		}
		if (!m_Grounded && !Statics.isOnRope) {
			animator.SetBool("IsSwimming", false);
			animator.SetBool("IsJumping", true);
		}
		if (m_Grounded) {
			animator.SetBool("IsSwimming", false);
			extraJumps = extraJumpsValue;
			animator.SetBool("IsJumping", false);
			animator.SetBool("doubleJump", false);
		}
		if (m_Grounded || m_AirControl)
		{
			//include force of moving objects
			if (onMovingPlatform && !m_Grounded)
			{
				move += currentPlatform.velocityX;
				onMovingPlatform = false;
			}
			//include sprint ability
			if (CanUseSprint() && Input.GetKeyDown(KeyCode.B)) {
				move *= 5f;
			}
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight && !Statics.isOnRope)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight && !Statics.isOnRope)
			{ 
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump(jump button is pressed)
		if (m_Grounded && jump){
			m_Grounded = false;

			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			
			// Add a vertical force to the player.

			jump = false;
			onMovingPlatform = false;
		}
		if (!m_Grounded && !jump)
		{
			animator.SetBool("doubleJump", false);
		}
		if (!m_Grounded && jump && extraJumps > 0 && !Statics.isOnRope)
		{
			//Set vertical velocity to 0 in order to double jump
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
			//Executing double jump
			animator.SetBool("doubleJump", true);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * extraJumpDifficulty));
			onMovingPlatform = false;
			jump = false;
			extraJumps--;
		}
	}
	public bool CanUseTripleJump()
	{
		return PlayerMovement.GetPlayerSkillsStatic().IsSkillTypeUnlocked(PlayerSkills.SkillType.TripleJump);
	}
	public bool CanUseSprint()
	{
		return PlayerMovement.GetPlayerSkillsStatic().IsSkillTypeUnlocked(PlayerSkills.SkillType.Sprint);
	}
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}