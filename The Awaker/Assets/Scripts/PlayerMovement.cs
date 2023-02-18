using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float nowSpeed, walkSpeed = 0.001f, runSpeed = 0.002f;
    public Animator animator; 

    private bool facingRight;

    void Start() {
        nowSpeed = 0f;
    }

    private void Flip(float direction)
	{
        if((direction > 0 && !facingRight) || (direction < 0 && facingRight) || direction == 0f) return;
        else {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1f;
            transform.localScale = theScale;

            facingRight = !facingRight;
        }
	}

    void Update() {

        nowSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);

        Flip(Input.GetAxis("Horizontal"));

        this.transform.position = new Vector3(
            this.transform.position.x + Input.GetAxis("Horizontal") * nowSpeed, 
            this.transform.position.y + Input.GetAxis("Vertical") * nowSpeed, 
            this.transform.position.z);
    }
}
