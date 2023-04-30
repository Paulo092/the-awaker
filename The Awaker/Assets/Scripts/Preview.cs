using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public Camera mainCamera;
    public Tilemap tileMap;
    private Vector3Int pos;
    private Vector3 posv3;
    public GameObject prefab;

    void Start() {
        Instantiate(prefab, Vector3.zero, Quaternion.identity).transform.SetParent(this.gameObject.transform);
        this.GetComponent<BoxCollider2D>().size = (Vector2) prefab.GetComponent<Renderer>().bounds.size;
        this.GetComponent<BoxCollider2D>().offset += (Vector2) prefab.GetComponent<Renderer>().bounds.center;
        // this.GetComponent<BoxCollider2D>().offset = (Vector2) prefab.GetComponent<Renderer>().bounds.extents;
        // Debug.Log(prefab.GetComponent<Renderer>().bounds.center);
        // Debug.Log(prefab.GetComponent<SpriteRenderer>().sprite.pivot);
    }

    void Update() {
        
        // this.transform.position = Utils.GetSpacedPosition(Utils.GetWorldMousePosition(Input.mousePosition, mainCamera));
        pos = tileMap.WorldToCell(Utils.GetWorldMousePosition(Input.mousePosition, mainCamera));
        posv3 = tileMap.CellToWorld(pos);
        posv3.x += 0.16f/2f;
        posv3.y += 0.16f/2f;
        this.transform.position = posv3;
    }

    void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag == "PlacedObject") {
            FindObjectOfType<PropScript>().SetCanPlace(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "PlacedObject") {
            FindObjectOfType<PropScript>().SetCanPlace(true);
        }
    }
}
