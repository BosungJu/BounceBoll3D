using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public JoyStick joyStick;
    public Rigidbody rb;
    public Item item;

    private const float bounceValue = 400f;
    private const float speed = 3f;

    private const float maxSpeed = 5f;

    private Vector3 angle;
    private Vector3 direction;

    private bool _playJump;
    public bool playJump
    {
        get 
        { 
            return _playJump; 
        }

        private set 
        { 
            _playJump = value;

            if (jumpEvent != null)
            {
                jumpEvent(value);
            }
        }
    }

    public Action<bool> jumpEvent = null;
    
    private void SetJumpEvent()
    {
        jumpEvent += Jump;
    }

    public void Jump(bool jumpFlag)
    {
        if (jumpFlag) 
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * bounceValue); 
        }
    }

    private void Move(bool isDraging)
    {
        if (!isDraging)
        {
            rb.velocity = new Vector3(rb.velocity.x / 3, rb.velocity.y, rb.velocity.z / 3);
        }
        else 
        {
            //float angleDiff = transform.eulerAngles.y - Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
            //if (angleDiff >= 90)
            //{
            //    rb.velocity = new Vector3(rb.velocity.x / 10, rb.velocity.y, rb.velocity.z / 10);
            //    //Debug.Log("enter");
            //}
            // don't use

            direction = Vector3.zero;

            rb.AddRelativeForce(joyStick.direction * speed);

            if (Mathf.Abs(rb.velocity.x) > maxSpeed || Mathf.Abs(rb.velocity.z) > maxSpeed)
            {
                rb.velocity = new Vector3(
                    rb.velocity.x > maxSpeed ? 
                    maxSpeed : rb.velocity.x < -maxSpeed ? 
                    -maxSpeed : rb.velocity.x,
                    rb.velocity.y,
                    rb.velocity.z > maxSpeed ? 
                    maxSpeed : rb.velocity.z < -maxSpeed ? 
                    -maxSpeed : rb.velocity.z);
            }
            
        } 
    }

    private void Awake()
    {
        SetJumpEvent();
        joyStick.dragEvent += Move;
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.velocity);
        // TODO y?? ?? ???? ???????? ?? ????
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            RaycastHit[] raycastHit = new RaycastHit[5];
            if ((Physics.Raycast(transform.position, Vector3.down, out raycastHit[0], 1) ||
                Physics.Raycast(transform.position + Vector3.forward / 2, Vector3.down, out raycastHit[1], 0.6f) ||
                Physics.Raycast(transform.position + Vector3.back / 2, Vector3.down, out raycastHit[2], 0.6f) ||
                Physics.Raycast(transform.position + Vector3.left / 2, Vector3.down, out raycastHit[3], 0.6f) ||
                Physics.Raycast(transform.position + Vector3.right / 2, Vector3.down, out raycastHit[4], 0.6f)) &&
                Mathf.Abs(rb.velocity.y) < 0.6f)
            {
                playJump = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        playJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            item = other.GetComponent<Item>();
        }
    }

}
