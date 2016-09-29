using UnityEngine;
using System.Collections;
using Prime31;

public class AIController : MonoBehaviour {

	private CharacterController2D _controller;

	public float speed = 1f;
	public float startingPos;
	public float endingPos;
	private float direction = -1f;
	private float originalPos;
    private float distance;
	private Vector2 walking;

	void Start () 
	{
        distance = startingPos - endingPos;
        _controller = gameObject.GetComponent<CharacterController2D>();
		originalPos = this.transform.position.x;
		endingPos = transform.position.x - distance;
		startingPos = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		walking.x = direction * speed * Time.deltaTime;
		if (direction > 0f && transform.position.x >= startingPos) {
			direction = -1f;
		}
		else if(direction < 0f && transform.position.x <= endingPos) {
			direction = 1f;
		}
		transform.Translate (walking);
	}
}
