using UnityEngine;
using System.Collections;
using Prime31;

public class AIController : MonoBehaviour {

	//private CharacterController2D _controller;

	public float speed = 1f;
	public float startingPos;
	public float endingPos;
	private float direction = -1f;
	//private float originalPos;
    private float distance;
	private Vector2 walking;
//	public BoxCollider2D player;
//	public BoxCollider2D enemy;
//	public BoxCollider2D shield;

	void Start () 
	{
        distance = startingPos - endingPos;
       // _controller = gameObject.GetComponent<CharacterController2D>();
		//originalPos = this.transform.position.x;
		endingPos = transform.position.x - distance;
		startingPos = transform.position.x;
//		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<BoxCollider2D> ();
//		enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<BoxCollider2D> ();
//		shield = GameObject.FindGameObjectWithTag ("Shield").GetComponent<BoxCollider2D> ();
	}

	// Update is called once per frame
	void Update () 
	{
//		if (enemy.IsTouching (player) || enemy.IsTouching (shield))
//		{
//			Debug.Log ("here");
//			if (direction == -1f)
//				direction = 1f;
//			else
//				direction = -1f;
//		}
		walking.x = direction * speed * Time.deltaTime;
		if (direction > 0f && transform.position.x >= startingPos) {
			direction = -1f;
		}
		else if(direction < 0f && transform.position.x <= endingPos) {
			direction = 1f;
		}
		transform.Translate (walking);
	}

	void OnCollisionEnter (Collision col)
	{
		Debug.Log ("here");
		string tag = col.gameObject.tag;
		if (tag == "Player" || tag == "Shield")
			direction *= -1;
	}
}
