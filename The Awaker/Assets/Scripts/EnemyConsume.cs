using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConsume : MonoBehaviour
{
    [ReadOnly, SerializeField] private bool inConsumeMode = true;
    public Vector3 gotoPosition;
    public Animator animator; 
    private bool firstTimeEnter = true;

    // Start is called before the first frame update
    void Start() {

        // Debug.Log("pos: " + this.transform.position);
        gotoPosition = this.transform.position;
        this.transform.SetParent(GameObject.Find("EnemyHandler").transform); 
        // Debug.Log(">pos: " + gotoPosition);
        // Debug.Log("Ready to consume");
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.Lerp(transform.position, gotoPosition, Time.deltaTime);
        // Debug.Log(">>> " + transform.position);


        // Debug.Log(Vector2.Distance((Vector2) transform.position, gotoPosition));

        if(Vector2.Distance(transform.position, gotoPosition) > 0.01) {
            inConsumeMode = false;
            animator.SetBool("isWalking", true);
            firstTimeEnter = true;
        } else {
            inConsumeMode = true;
            animator.SetBool("isWalking", false);
            if (firstTimeEnter)  {
                Invoke("Goto", 5);
                firstTimeEnter = false;
            }
        } 
    }

    private void Goto() {
        gotoPosition = Utils.SetLayer(FindObjectOfType<TileScript>().GetRandomCellGlobalPosition(), Utils.L_ENEMY);
    }

    void OnCollisionStay2D(Collision2D collision) {
        // if(collision.gameObject.tag == "PlacedObject" && inConsumeMode) Destroy(collision.gameObject);
        if(collision.gameObject.tag == "PlacedObject" && inConsumeMode) {
            foreach (ContactPoint2D contact in collision.contacts) {
                FindObjectOfType<TileScript>().DestroyTileAt(new Vector3(contact.point.x, contact.point.y));
                
            }
        }

        // if(collision.gameObject.tag == "KillCollider") {
        //     Debug.Log("kill collider");
        // }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "KillCollider") {
            this.transform.Find("KillKey").gameObject.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "KillCollider") {
            this.transform.Find("KillKey").gameObject.SetActive(false);
        }
    }

}
