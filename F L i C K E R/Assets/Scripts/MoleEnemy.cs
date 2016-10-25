using UnityEngine;
using System.Collections;

public class MoleEnemy : MonoBehaviour {

    public float speed = 1f;
    public float startingPos;
    public float endingPos;
    private float direction = -1f;
    private int health = 3;
    private float distance;
    private Vector2 walking;
    public float hidingX;
    public float hidingY;
    public float showingX;
    public float showingY;
    public float popVal;
    private Vector2 pop;
    private Vector2 hidingPos;
    private Vector2 showingPos;
    public GameObject platform;
    private BoxCollider2D platColl;
    private bool hiding = true;
    private BoxCollider2D player;
    private BoxCollider2D shield;
    private BoxCollider2D enemy;
    private GameObject me;
    private BoxCollider2D sword;

    void Start()
    {
        distance = startingPos - endingPos;
        endingPos = transform.position.x - distance;
        startingPos = transform.position.x;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<BoxCollider2D>();
        platColl = platform.GetComponent<BoxCollider2D>();
        me = gameObject;
        enemy = me.GetComponent<BoxCollider2D>();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider2D>();
        hidingPos = new Vector2(hidingX, hidingY);
        showingPos = new Vector2(showingX, showingY);
        me.transform.position = hidingPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (hiding)
        {
            me.transform.position = hidingPos;
            if(player.IsTouching(platColl))
            {
                hiding = false;
                me.transform.position = showingPos;
            }
        }
        else
        {
            if (health == 0)
            {
                me.SetActive(false);
            }
            if (enemy.IsTouching(sword))
            {
                health--;
            }
            if (enemy.IsTouching(player) || enemy.IsTouching(shield))
            {
                //if (direction == -1f)
                //    direction = 1f;
                //else
                //    direction = -1f;
            }
            walking.x = direction * speed * Time.deltaTime;
            if (direction > 0f && transform.position.x >= startingPos)
            {
                pop.x = transform.position.x;
                pop.y = popVal * speed * Time.deltaTime;
                transform.Translate(pop);
                //direction = -1f;
            }
            else if (direction < 0f && transform.position.x <= endingPos)
            {
                pop.x = transform.position.x;
                pop.y = popVal * speed * Time.deltaTime;
                transform.Translate(pop);
                //direction = 1f;
            }
            transform.Translate(walking);
        }
    }
}
