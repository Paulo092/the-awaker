using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;


public class EnemyConsume : MonoBehaviour
{
    [ReadOnly, SerializeField] private bool inConsumeMode = true;
    public Vector3 gotoPosition;
    public Animator animator; 
    private bool firstTimeEnter = true, killElegible = false;
    private float killDistance = 1.10F;
    private GameObject player, killKey, tileMap;
    private Tilemap tilemap;
    [SerializeField] public AudioClip dieSound;

    // Start is called before the first frame update
    void Start() {

        // Debug.Log("pos: " + this.transform.position);
        gotoPosition = this.transform.position;
        player = GameObject.Find("Player");
        this.transform.SetParent(GameObject.Find("EnemyHandler").transform); 
        killKey = this.transform.Find("KillKey").gameObject;
        tilemap = FindObjectOfType<TileScript>().GetTilemap();
        // Debug.Log(">pos: " + gotoPosition);
        // Debug.Log("Ready to consume");
        
    }

    // Update is called once per frame
    void Update() {
        // Debug.Log(this.GetComponent<Renderer>().bounds);

        // transform.position = Vector3.Lerp(transform.position, gotoPosition, Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, new Vector3(gotoPosition.x, gotoPosition.y, tilemap.CellToWorld(tilemap.WorldToCell(this.transform.position)).y), Time.deltaTime);
        // Debug.Log(">>> " + transform.position);

        killKey.SetActive(killElegible);

        if(killElegible && Input.GetKey(KeyCode.E)) {
            StartCoroutine(Utils.SmoothDestroyGameObject(this.gameObject));
            FindObjectOfType<EnemySpawn>().isEnemySpawned = false;
        }
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

        killElegible = Vector2.Distance(player.transform.position, this.transform.position) <= killDistance ? true : false;
    }

    public void Die() {
        SoundManager.Instance.PlaySound(dieSound);
        Destroy(this);
    }

    private void Goto() {
        // this.transform.position = new Vector3(transform.position.x, transform.position.y, tilemap.CellToWorld(tilemap.WorldToCell(this.transform.position)).y);
        gotoPosition = Utils.SetLayer(FindObjectOfType<TileScript>().GetRandomCellGlobalPosition(), Utils.L_ENEMY);
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("PlacedObject") && inConsumeMode) {
            FindObjectOfType<TileScript>().DestroyTileAt(this.GetComponent<Renderer>().bounds.center, 2);
        }

        if(killElegible && Input.GetKey(KeyCode.E)) {
            SoundManager.Instance.PlaySound(dieSound);
            animator.SetBool("isDead", true);
            FindObjectOfType<EnemySpawn>().isEnemySpawned = false;
        }

    }
    // void OnCollisionStay2D(Collision2D collision) {
    //     Debug.Log("Colidiu");
    //     // if(collision.gameObject.tag == "PlacedObject" && inConsumeMode) Destroy(collision.gameObject);
    //     if(collision.gameObject.tag == "PlacedObject" && inConsumeMode) {
    //         foreach (ContactPoint2D contact in collision.contacts) {
    //             if(FindObjectOfType<TileScript>().GetTilesPlaced() > 0) {
    //                 FindObjectOfType<TileScript>().DestroyTileAt(new Vector3(contact.point.x, contact.point.y));
    //             } else {
    //                 Destroy(this);
    //             }
    //         }
    //     }
    // }

    // void OnTriggerEnter2D(Collider2D other) {
    //     if(other.CompareTag("KillCollider")) {
    //         this.transform.Find("KillKey").gameObject.SetActive(true);
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other) {
    //     if(other.CompareTag("KillCollider")) {
    //         this.transform.Find("KillKey").gameObject.SetActive(false);
    //     }
    // }

}
