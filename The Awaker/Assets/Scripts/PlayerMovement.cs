using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator; 

    public float nowSpeed;
    private bool facingRight;
    public Tilemap tilemap;

    public const float walkSpeed = 0.02f, runSpeed = 0.03f;
    private Rigidbody2D rb2d;

    public AudioClip footstepsSound;

    void Start() {
        nowSpeed = 0f;
        facingRight = true;
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    private void Flip(float direction) {
        if((direction > 0 && facingRight) || (direction < 0 && !facingRight) || direction == 0f) 
            return;
        else {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1f;
            this.transform.localScale = theScale;

            facingRight = !facingRight;
        }
	}

    public void PlayFootsteps() {
        SoundManager.Instance.PlaySound(footstepsSound);
    }

    void FixedUpdate() {
        nowSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) {
            animator.SetBool("isWalking", true);
            // this.GetComponent<AudioSource>().enabled = true;
            // animator.SetBool("isWalking", nowSpeed == walkSpeed ? true : false);
            // animator.SetBool("isRunning", nowSpeed == runSpeed ? true : false);
        } else {
            animator.SetBool("isWalking", false);
            // this.GetComponent<AudioSource>().enabled = false;
            // animator.SetBool("isRunning", false);
        }

        Flip(Input.GetAxis("Horizontal"));

        // rb2d.AddForce(new Vector2(Input.GetAxis("Horizontal") * 0.5f, Input.GetAxis("Vertical") * 0.5f ));

        rb2d.MovePosition(
            (Vector2) this.transform.position + new Vector2(Input.GetAxis("Horizontal") * nowSpeed, Input.GetAxis("Vertical") * nowSpeed )
        );

        // Debug.Log("ZV: " + gridBase.CellToWorld(gridBase.WorldToCell(this.transform.position)).y);

        // Vector3 cellPos = 

        // Debug.Log("ZV: " + GetComponent<Renderer>().bounds.center + "Tile: " + tilemap.WorldToCell(GetComponent<Renderer>().bounds.center));

        // this.transform.position = new Vector3(transform.position.x, transform.position.y, tilemap.CellToWorld(tilemap.WorldToCell(this.transform.position)).y);
        this.transform.position = new Vector3(transform.position.x, transform.position.y, tilemap.CellToWorld(tilemap.WorldToCell(this.transform.position)).y);
        // tilemap.transform.position = new Vector3(tilemap.transform.position.x, tilemap.transform.position.y, this.transform.position.z + 1);
        // rb2d.MovePosition(

        //     new Vector2(rb2d.position.x + Input.GetAxis("Horizontal") * 2, 
        //     rb2d.position.y + Input.GetAxis("Vertical") * 2)

        //      * Time.fixedDeltaTime);

        // this.transform.position = new Vector3(
        //     this.transform.position.x + Input.GetAxis("Horizontal") * nowSpeed, 
        //     this.transform.position.y + Input.GetAxis("Vertical") * nowSpeed, 
        //     this.transform.position.z);
    }
}
