using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

[RequireComponent(typeof(Movable))]
public class PlayerControllers : MonoBehaviour
{
    public InputManager inputManager;
    
    private float dir;
    private Movable movable;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float direction_addedx;
    private float Speed;
    
    [SerializeField]private Animator anim;
    private Jumpable jumpable;
   
    [SerializeField]private bool CanInteract = false;
    public UnityAction Interaction;
    
    public GameObject interactable;

    public AudioSource JumpAudio;
    public AudioSource RunAudio;

    public bool setPlayerNotMove;

    private Vector3 LastMove;

    [SerializeField] GameObject[] DialogPlayerRandomObjectiveUnfinish;
    [SerializeField] GameObject[] DialogPlayerRandomObjectivefinish;
    [SerializeField] GameObject[] DialogPlayerRandomHitBlockade;
    private int RandomDialogIndex;

    private int Random_reaction;

    public bool InteractPressed = false;

    bool Crouch = false;
    bool flashlightOn = false;

    public GameObject FlashLight;

    private GameObject FlashLightActive;

    [SerializeField]private bool moveUp;
    [SerializeField]private bool moveDown;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movable = GetComponent<Movable>();
        jumpable = GetComponent<Jumpable>();
        rb = GetComponent<Rigidbody2D>();
        dir = transform.localScale.x;
    }
      
    void Update()
    {
        moveUsingMovable();
    }

    private void OnSetDirection(Vector2 direction_added)
    {
        if (setPlayerNotMove)
        {
            direction_added.x = 0;
            direction_added.y = 0;
        }
        direction = direction_added;

        if(anim != null)
            anim.SetFloat("horizontalInput", direction_added.x);
    }
    private void OnJump()
    {
        if(jumpable != null)
        {
            if (jumpable.isGrounded)
            {
                if(JumpAudio != null)
                    JumpAudio.Play();
                if(anim != null)
                    anim.SetBool("isJump", true);
                if(jumpable != null)
                    jumpable.jump(GetComponent<Rigidbody2D>());
            }
        }   
    }

    private void OnInteract()
    {
        InteractPressed = !InteractPressed;

        if (CanInteract)
        {
            if (interactable != null)
            {
                if (interactable.tag == "Box" && InteractPressed)
                {
                    Speed -= 2f;
                    movable.speed -= 2f;
                }
                else if(interactable.tag == "Box" && !InteractPressed)
                {
                    Speed += 2f;
                    movable.speed += 2f;
                }

                if (interactable.GetComponent<DialogTrigger>() != null)
                {
                    direction.x = 0;

                    interactable.GetComponent<DialogTrigger>().TriggerDialog();

                    InteractPressed = false;
                }  
            }
        }
        else
        {
            direction.x = 0;

            if(FindAnyObjectByType<ObjekCount>() != null)
            {
                ObjekCount objective_count = FindAnyObjectByType<ObjekCount>();

                if (objective_count.CheckObjectCount())
                {
                    RandomDialogIndex = Random.Range(0, DialogPlayerRandomObjectivefinish.Length);
                    DialogPlayerRandomObjectivefinish[RandomDialogIndex].GetComponent<DialogTrigger>().TriggerDialog();
                }
                else
                {
                    RandomDialogIndex = Random.Range(0, DialogPlayerRandomObjectiveUnfinish.Length);
                    DialogPlayerRandomObjectiveUnfinish[RandomDialogIndex].GetComponent<DialogTrigger>().TriggerDialog();
                }
            }
            InteractPressed = false;
        }
    }

    private void OnCrouch()
    {
        Crouch = !Crouch;
        if(anim != null)
        {
            anim.SetBool("isCrouch", Crouch);
        }

        if (Crouch)
        {
            Speed -= 2f;
            movable.speed -= 2f;
        }
        else if(!Crouch)
        {
            Speed += 2f;
            movable.speed += 2f;
        }
    }

    private void OnFlashLight()
    {
        flashlightOn = !flashlightOn;
        if(FlashLight != null)
        {
            FlashLight.SetActive(flashlightOn);
        }
    }
    
    private void OnUpandDown(Vector2 direction_added)
    {
        if(direction_added.y != 0)
        {
            if(direction_added.y > 0)
            {
                moveUp = true;
                moveDown = false;
                Debug.Log("MoveUp : " + moveUp);
                Debug.Log("MoveDown : " + moveDown);
            }
            else if(direction_added.y < 0)
            {
                moveDown = true;
                moveUp = false;
                Debug.Log("MoveUp : " + moveUp);
                Debug.Log("MoveDown : " + moveDown);
            }
        }
    }

    public void OnEnable()
    {
        inputManager.OnMoveAction += OnSetDirection;
        inputManager.OnJumpAction += OnJump;
        inputManager.OnInteractAction += OnInteract;
        inputManager.OnCrouchAction += OnCrouch;
        inputManager.OnFlashLightAction += OnFlashLight;
        inputManager.OnUpAndDownAction += OnUpandDown;
    }
    public void OnDisable()
    {
        inputManager.OnMoveAction -= OnSetDirection;
        inputManager.OnJumpAction -= OnJump;
        inputManager.OnInteractAction -= OnInteract;
        inputManager.OnCrouchAction -= OnCrouch;
        inputManager.OnFlashLightAction -= OnFlashLight;
        inputManager.OnUpAndDownAction -= OnUpandDown;
    }

    public void setInteract(bool x)
    {
        CanInteract = x;
        Debug.Log("Interact State: " + CanInteract);
    }

    public void PlayerSoundDisabled()
    {
        JumpAudio.Stop();
        RunAudio.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blockade"))
        {
            RandomDialogIndex = Random.Range(0, DialogPlayerRandomHitBlockade.Length);
            DialogPlayerRandomHitBlockade[RandomDialogIndex].GetComponent<DialogTrigger>().TriggerDialog();

            PlayernotMove();
        }
    }

    public void PlayernotMove()
    {
        direction.x = 0;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void moveUsingMovable()
    {
        movable.direction = Vector3.zero;

        direction_addedx = direction.x;

        if (setPlayerNotMove)
        {
            PlayerSoundDisabled();

            direction.x = 0;
            direction.y = 0;

            direction_addedx = 0;

            anim.Play("Idle");
        }
        

        //if (jumpable.isGrounded)
        //{
            //anim.SetBool("isJump", false);
            //anim.SetBool("isInAir", false);

            movable.direction.x = direction_addedx;

            //if (interactable != null && InteractPressed)
            //{
            //    if (interactable.gameObject.CompareTag("Box") && transform.localScale.x > 0)
            //    {
            //        if (movable.direction.x != 0)
            //        {
            //            if (movable.direction.x > 0)
            //                transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
            //            else if (movable.direction.x < 0)
            //                transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
            //        }
            //    }
            //    else if (interactable.gameObject.CompareTag("Box") && transform.localScale.x < 0)
            //    {
            //        if (movable.direction.x != 0)
            //        {
            //            if (movable.direction.x > 0)
            //                transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
            //            else if (movable.direction.x < 0)
            //                transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
            //        }
            //    }
            //    else
            //    {
            //        if (movable.direction.x != 0)
            //        {
            //            if (movable.direction.x > 0)
            //                transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
            //            else if (movable.direction.x < 0)
            //                transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
            //        }
            //    }
            //}
            //else
            //{

                if (movable.direction.x != 0)
                {
                    if (movable.direction.x > 0)
                        transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
                    else if (movable.direction.x < 0)
                        transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
                }

            //}
        //}
        //else
        //{
        //    //anim.SetBool("isInAir", true);
        //    //RunAudio.Stop();
        //    //movable.direction.x = LastMove.x;
        //    //if (movable.direction.x != direction_addedx && direction_addedx != 0)
        //    //{
        //    //    movable.direction.x = 0;
        //    //}
        //}

        LastMove = movable.direction;

        anim.SetBool("isMoving", movable.direction.x != 0);

        if (movable.direction.x == 0)
        {
            RunAudio.Stop();
        }
        else if (movable.direction.x != 0)
        {
            if (!RunAudio.isPlaying)
            {
                RunAudio.Play();
            }
        }

        if (interactable != null )
        {
            //if (interactable.gameObject.CompareTag("Box") && InteractPressed)
            //{
            //    if (jumpable.isGrounded)
            //    {
            //        anim.SetBool("isPushing", true);
            //    }
            //}
            //else
            //{
            //    anim.SetBool("isPushing", false);
            //}

            if (interactable.gameObject.CompareTag("Pintu"))
            {
                interactable.GetComponent<Pintu>().setPintu(InteractPressed);
            }
        }
    }

    public bool ismoveUp()
    {
        return moveUp;
    }

    public bool ismoveDown()
    {
        return moveDown;
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