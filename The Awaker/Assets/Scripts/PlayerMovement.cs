using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator; 

    public float nowSpeed;
    private bool facingRight;

    public const float walkSpeed = 0.01f, runSpeed = 0.03f;

    void Start() {
        nowSpeed = 0f;
        facingRight = true;
    }

    private void Flip(float direction) {
        if((direction > 0 && !facingRight) || (direction < 0 && facingRight) || direction == 0f) 
            return;
        else {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1f;
            this.transform.localScale = theScale;

            facingRight = !facingRight;
        }
	}

    void FixedUpdate() {
        nowSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        animator.SetBool("isWalking", Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f ? true : false);

        Flip(Input.GetAxis("Horizontal"));

        this.transform.position = new Vector3(
            this.transform.position.x + Input.GetAxis("Horizontal") * nowSpeed, 
            this.transform.position.y + Input.GetAxis("Vertical") * nowSpeed, 
            this.transform.position.z);
    }
}
