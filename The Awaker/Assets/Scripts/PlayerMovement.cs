using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.005f;
    public Animator animator; 

    private bool facingRight;

    void Start() {
        speed = 0.005f;
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

        if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) animator.SetFloat("Speed", 1);
        else animator.SetFloat("Speed", 0);

        Flip(Input.GetAxis("Horizontal"));

        this.transform.position = new Vector3(
            this.transform.position.x + Input.GetAxis("Horizontal") * speed, 
            this.transform.position.y + Input.GetAxis("Vertical") * speed, 
            this.transform.position.z);
    }
}
