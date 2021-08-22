using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private bool canCirculate = true;
    private bool isFollowingPlayer = false;
    private Animator animation;
    public GameObject BattleScene;

    private GameController gameController;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void startWalk()
    {
        animation.Play("Knight_walk");
    }

    void Move()
    {
        if(canCirculate)
        {
            body.velocity = new Vector2(2 * (sprite.flipX ? -1 : 1), 0);
            startWalk();
        }
    }

    void FollowPlayer()
    {
        if(isFollowingPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player)
            {
                float x = 0;
                if(player.transform.position.x != transform.position.x)
                {
                    if(player.transform.position.x > transform.position.x)
                    {
                        x = 1;
                    }
                    else
                    {
                        x = -1;
                    }
                }
                float y = 0;
                if(player.transform.position.y != transform.position.y)
                {
                    if(player.transform.position.y > transform.position.y)
                    {
                        y = 1;
                    }
                    else
                    {
                        y = -1;
                    }
                }
                sprite.flipX = x < 0;
                
                body.velocity = new Vector2(x, y) * 2;
                startWalk();
            }
        }
    }

    void Update()
    {
        if (BattleScene.activeSelf) {
            body.velocity = new Vector2(0, 0);
            return;
        };
        Move();
        FollowPlayer();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "dungeon" && !isFollowingPlayer) 
        {
            sprite.flipX = !sprite.flipX;
            StartCoroutine("WaitToCirculate");
        }
        else if(other.gameObject.tag == "Player")
        {
            gameController.StartBattle();
            Destroy(gameObject);
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isFollowingPlayer = true;
            canCirculate = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine("WaitToCirculate");
        }
    }
    
    IEnumerator WaitToCirculate()
    {
        isFollowingPlayer = false;
        canCirculate = false;
        animation.Play("Knight_idle");
        yield return new WaitForSeconds(2f);
        if(!isFollowingPlayer){
            canCirculate = true;
        }
    }
}
