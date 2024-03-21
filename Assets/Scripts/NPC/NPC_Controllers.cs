using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controllers : MonoBehaviour
{
    private float dir;
    public float speed;
    Rigidbody2D rb;

    private Animator anim;

    public Transform movedirection;

    [Header(" Colliders Parameters")]
    [SerializeField] private float distanceCollider;
    [SerializeField] private float range;
    [SerializeField] private Collider2D Collider;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private bool moveUp;
    [SerializeField] private bool moveDown;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        dir = GetComponent<Transform>().localScale.x;
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();

    }


    private void follow(Transform direction)
    {
        if(Vector2.Distance(transform.position, 
            new Vector3(direction.position.x, transform.position.y, transform.position.z)) > 2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                new Vector3(direction.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);

            anim.SetBool("walk", true);

            if (direction.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
            }
            else if (direction.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    private void Update()
    {
        follow(movedirection);

        RaycastHit2D hit = Physics2D.BoxCast(Collider.bounds.center + transform.right * range *
           transform.localScale.x * distanceCollider, new Vector3(Collider.bounds.size.x * range,
           Collider.bounds.size.y, Collider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        RaycastHit2D hit2 = Physics2D.BoxCast(Collider.bounds.center - transform.right * range *
           transform.localScale.x * distanceCollider, new Vector3(Collider.bounds.size.x * range,
           Collider.bounds.size.y, Collider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Collider.isTrigger = true;
                rb.gravityScale = 0;
            }
            else
            {
                Collider.isTrigger = false;
            }
        }   
        else
        if(hit2.collider != null)
        {
            if (hit2.collider.gameObject.CompareTag("Player"))
            {
                Collider.isTrigger = true;
                rb.gravityScale = 0;
            }
            else
            {
                Collider.isTrigger = false;
            }
        }
        else
        {
            Collider.isTrigger = false;
            rb.gravityScale = 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Collider.bounds.center + transform.right *
            range * transform.localScale.x * distanceCollider, new Vector3(Collider.bounds.size.x *
            range, Collider.bounds.size.y, Collider.bounds.size.z));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Collider.bounds.center - transform.right *
            range * transform.localScale.x * distanceCollider, new Vector3(Collider.bounds.size.x *
            range, Collider.bounds.size.y, Collider.bounds.size.z));
    }

    public void setUpandDownDefault()
    {
        moveUp = false;
        moveDown = false;
        setRBConstrains(RigidbodyConstraints2D.FreezeRotation);
    }

    public void setRBConstrains(RigidbodyConstraints2D constraints2D)
    {
        rb.constraints = constraints2D;
    }

}