using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpable : MonoBehaviour
{
    public float jumpPower;
    public bool isGrounded; 

    public void jump(Rigidbody2D rb)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Box")
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Ground Special")
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Blockade")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Box")
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Ground Special")
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Blockade")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        if (other.gameObject.tag == "Box")
        {
            isGrounded = false;
        }
        if (other.gameObject.tag == "Ground Special")
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Blockade")
        {
            isGrounded = false;
        }
    }
}