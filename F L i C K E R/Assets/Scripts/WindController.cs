using UnityEngine;
using System.Collections;

public class WindController : MonoBehaviour 
{
	public Vector3 endPosition = Vector3.zero;
	public float speed = 2;

	private float timer = 0;
	private Vector3 startPosition = Vector3.zero;
	private bool outgoing = true;
	private BoxCollider2D player;
	private BoxCollider2D wind1;
	private BoxCollider2D wind2;
	private BoxCollider2D wind3;
	private BoxCollider2D wind4;
	private GameObject[] winds;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<BoxCollider2D>();
		winds = GameObject.FindGameObjectsWithTag ("Wind");
		Debug.Log (winds.Length);
		wind1 = winds[0].GetComponent<BoxCollider2D>();
		wind2 = winds[1].GetComponent<BoxCollider2D>();
		wind3 = winds[2].GetComponent<BoxCollider2D>();
		wind4 = winds[3].GetComponent<BoxCollider2D>();

		startPosition = this.gameObject.transform.position;
		endPosition += startPosition;

		float distance = Vector3.Distance(startPosition, endPosition);
		if (distance != 0)
		{
			speed /= distance;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime * speed;
		bool go = false;
		bool touching = false;
		if (wind1.IsTouching (player) || wind2.IsTouching (player) || wind3.IsTouching (player) || wind4.IsTouching (player)) {
			touching = true;
		} else
			touching = false;

		if (outgoing && touching) 
		{
			Debug.Log ("go");
			go = true;
			this.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
			if (timer > 1) 
			{
				outgoing = false;
				timer = 0;
			}
		} 
		else if (!touching && go)
		{
			this.transform.position = Vector3.Lerp(endPosition, startPosition, timer);
			if (timer > 1) 
			{
				outgoing = true;
				timer = 0;
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(this.transform.position, endPosition + this.transform.position);
	}
}
