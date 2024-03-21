using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject box;

    private bool pushpull = false;

    [Header(" Colliders Parameters")]
    [SerializeField] private float distanceCollider;
    [SerializeField] private float range;
    [SerializeField] private BoxCollider2D Collider;
    [SerializeField] private LayerMask playerLayer;

    private Movable movable;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Collider = this.gameObject.GetComponent<BoxCollider2D>();
        movable = this.gameObject.GetComponent<Movable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<PlayerControllers>() != null)
        {
            gameObject.GetComponent<PlayerControllers>().setInteract(false);
        }

        pushpull = false;

        if (this.gameObject.GetComponent<PlayerControllers>().InteractPressed == true)
        {
            pushpull = true;
        }

        Debug.Log("Push Pull = " + pushpull);

        RaycastHit2D hit = Physics2D.BoxCast(Collider.bounds.center + transform.right * range *
           transform.localScale.x * distanceCollider, new Vector3(Collider.bounds.size.x * range,
           Collider.bounds.size.y, Collider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider == null)
        {
            if (this.gameObject.GetComponent<PlayerControllers>() != null)
            {
                gameObject.GetComponent<PlayerControllers>().setInteract(false);
                gameObject.GetComponent<PlayerControllers>().interactable = null;
            }
        }
        else
        {
            if (hit.collider.gameObject.CompareTag("Interact"))
            {
                if (this.gameObject.GetComponent<PlayerControllers>() != null)
                    gameObject.GetComponent<PlayerControllers>().setInteract(true);
                gameObject.GetComponent<PlayerControllers>().interactable = hit.collider.gameObject;

            }

            if (hit.collider.gameObject.CompareTag("Box"))
            {
                Debug.Log("Kena Box");

                if (this.gameObject.GetComponent<PlayerControllers>() != null)
                    gameObject.GetComponent<PlayerControllers>().setInteract(true);
                gameObject.GetComponent<PlayerControllers>().interactable = hit.collider.gameObject;

                box = hit.collider.gameObject;
            }

            if (hit.collider.gameObject.CompareTag("Pintu"))
            {
                Debug.Log("Ada Pintu");

                if (this.gameObject.GetComponent<PlayerControllers>() != null)
                    gameObject.GetComponent<PlayerControllers>().setInteract(true);
                gameObject.GetComponent<PlayerControllers>().interactable = hit.collider.gameObject;

            }

            if (pushpull == true)
            {
                if (box != null)
                    PushPull(box);
            }
        }

        if (box != null)
        {
            if (pushpull == false)
            {
                if(box != null)
                    PushPullDisable(box);
            }
        }
    }

    void PushPull(GameObject gameObject)
    {
        if(gameObject.GetComponent<FixedJoint2D>() != null)
        {
            gameObject.GetComponent<FixedJoint2D>().enabled = true;
            gameObject.GetComponent<FixedJoint2D>().connectedBody = rb;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.GetComponent<Rigidbody2D>().mass = 0.3f;
        } 
    }

    void PushPullDisable(GameObject gameObject)
    {
        if (gameObject.GetComponent<FixedJoint2D>() != null)
        {
            gameObject.GetComponent<FixedJoint2D>().enabled = false;
            gameObject.GetComponent<FixedJoint2D>().connectedBody = null;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            gameObject.GetComponent<Rigidbody2D>().mass = 1f;
            box = null;
        }   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Collider.bounds.center + transform.right *
            range * transform.localScale.x * distanceCollider, new Vector3(Collider.bounds.size.x *
            range, Collider.bounds.size.y, Collider.bounds.size.z));
    }
}