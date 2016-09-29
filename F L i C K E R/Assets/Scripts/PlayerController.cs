using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
#region Vars
	public GameObject gameCamera;
	public GameObject healthBar;
	public GameObject gameOverPanel;
	public GameObject winPanel;
	public float walkSpeed = 3;
	public float gravity = -35;
	public float jumpHeight = 2;
	public int health = 100;

	public BoxCollider2D shield;
	public BoxCollider2D enemy;

	private bool shieldin = false;
	private bool floatin = false;
	private bool playerControl = true;
	private int currHealth = 0;
	private CharacterController2D _controller;
	//private AnimationController2D _animator;
#endregion

	// Use this for initialization
	void Start ()
    {
		shield = GameObject.FindGameObjectWithTag ("Shield").GetComponent<BoxCollider2D> ();
		enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<BoxCollider2D> ();
		_controller = gameObject.GetComponent<CharacterController2D>();
		//_animator = gameObject.GetComponent<AnimationController2D>();

		gameCamera.GetComponent<CameraFollow2D> ().startCameraFollow (this.gameObject);

		currHealth = health;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (playerControl) 
		{
			Vector3 velocity = PlayerInput ();
			_controller.move (velocity * Time.deltaTime);
		}
	}
		
#region Movement
	private Vector3 PlayerInput()
	{
		Vector3 velocity = _controller.velocity;
		velocity.x = 0;

		if (_controller.isGrounded && _controller.ground != null && _controller.ground.tag.Equals("MovingPlatform")) {
			this.transform.parent = _controller.ground.transform;
		}
		else 
		{
			if (this.transform.parent != null)
				this.transform.parent = null;
		}

		#region running left/right
		// Left arrow key
		if (Input.GetAxis ("Horizontal") < 0 && !shieldin)
		{
			velocity.x = -walkSpeed;
			if (_controller.isGrounded) 
			{
				//_animator.setAnimation ("Run");
				//_animator.setFacing ("Left");
			}
		}

		// Right arrow key
		else if (Input.GetAxis ("Horizontal") > 0 && !shieldin) 
		{
			velocity.x = walkSpeed;
			if (_controller.isGrounded) 
			{
				//_animator.setAnimation ("Run");
				//_animator.setFacing ("Right");
			}
		}
		#endregion
		#region idle
		// Idle
		//else 
		//{
			//if (_controller.isGrounded && currHealth != 0) {
			//}
				//_animator.setAnimation("Idle");
		//}
		#endregion
		#region Jump/Float
		// Space bar - Jump
		if (Input.GetKeyDown (KeyCode.Space) && !shieldin && _controller.isGrounded) 
		{
			velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
		} 
		else if ((Input.GetKeyDown (KeyCode.Space) && !_controller.isGrounded) || floatin) 
		{
			velocity.y = -2;
			floatin = true;
		}
		if (_controller.isGrounded)
		{
			floatin = false;
			gravity = -35;
		}
		#endregion
		//Shield up and down
		if (Input.GetAxis("Fire1") > 0) {
			shieldin = true;
		} else
			shieldin = false;

		// Snapping camera for big jump
		/*if (GameObject.Find("Player").transform.position.x > 13)
		{
			Camera.main.fieldOfView = 100;
			gameCamera.GetComponent<CameraFollow2D>().stopCameraFollow();
			Vector3 newPos = new Vector3(21.32f, 1.33f, -8.0f);
			Camera main = Camera.main;
			main.transform.position = newPos;
		}*/

		//Check if touching enemy and shield up/down
		if (shield.IsTouching(enemy) && Input.GetKey(KeyCode.X)) {}
		else if(shield.IsTouching(enemy))
		{
			PlayerDamage (5);	
		}

		// Change velocity.
		velocity.y += gravity * Time.deltaTime;
		return velocity;
	}

//	void OnCollisionEnter(Collision col)
//	{
//		if (GetComponent<Collider>().GetComponent<Collider>().name == "Enemy")
//			PlayerDamage(5);
//	}

#endregion
#region Damage/Death/Winning
	// When the player collides with the death collider
	void OnTriggerEnter2D(Collider2D col)
	{
		//Debug.Log ("trigger");
		if (col.tag == "KillZ")
			PlayerFallDeath ();
		else if (col.tag == "Damaging")
			PlayerDamage (10);
		else if (col.tag == "YouWin")
			Winning ();
		else if (col.tag == "Enemy" && Input.GetKey (KeyCode.X)) {}
		else if(col.tag == "Enemy")
			PlayerDamage (5);
	}
		
	private void Winning()
	{
		playerControl = false;
		//_animator.setAnimation("Idle");
		winPanel.SetActive(true);
	}

	// Changes player health when damage is taken. checks for death
	private void PlayerDamage(int damage)
	{
		currHealth -= damage;
		float normHealth = (float)currHealth/(float)health;

		Debug.Log ("You lost " + damage);
		//healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(normHealth*256, 32);

		if (currHealth <= 0)
			PlayerDeath();
	}

	// Play death animation
	private void PlayerDeath()
	{
		//_animator.setAnimation("Death");
		playerControl = false;
		gameOverPanel.SetActive(true);
	}

	// Stops the camera follow and reduces health
	private void PlayerFallDeath()
	{
        Debug.Log("dead");
		currHealth = 0;
		//healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 32);
		gameCamera.GetComponent<CameraFollow2D>().stopCameraFollow();
		//gameOverPanel.SetActive(true);
	}
#endregion
}
