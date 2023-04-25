using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConsume : MonoBehaviour
{
    private bool consumeMode = false;
    public Vector2 gotoPosition;
    public Animator animator; 

    // Start is called before the first frame update
    void Start() {
        // Debug.Log("Ready to consume");
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, gotoPosition, Time.deltaTime);

        Debug.Log(Vector2.Distance((Vector2) transform.position, gotoPosition));

        if(Vector2.Distance((Vector2) transform.position, gotoPosition) > 0.01) {
            consumeMode = false;
            animator.SetBool("isWalking", true);
        } else {
            consumeMode = true;
            animator.SetBool("isWalking", false);
        } 
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "PlacedObject" && consumeMode) Destroy(collision.gameObject);
    }

}
