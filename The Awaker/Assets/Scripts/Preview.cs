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
    public GameObject materialBrush, propBrush;
    public bool isPaintingMaterials = true;
    private BoxCollider2D collider;

    void Start() {
        // Instantiate(prefab, Vector3.zero, Quaternion.identity).transform.SetParent(this.gameObject.transform);
        // this.GetComponent<BoxCollider2D>().size = (Vector2) prefab.GetComponent<Renderer>().bounds.size;
        // this.GetComponent<BoxCollider2D>().offset += (Vector2) prefab.GetComponent<Renderer>().bounds.center;
        collider = this.GetComponent<BoxCollider2D>();

        materialBrush = gameObject.transform.Find("MaterialBrush").gameObject;
        materialBrush.SetActive(true);

        propBrush = gameObject.transform.Find("PropBrush").gameObject;
        propBrush.SetActive(false);

        SetPropBrushPrefab(FindObjectOfType<PropScript>().GetHotbarSelectedPrefab());

        // propBrush.GetComponent<SpriteRenderer>().sprite = FindObjectOfType<PropScript>().GetHotbarSelectedSprite();
    }

    void Update() {
        
        // this.transform.position = Utils.GetSpacedPosition(Utils.GetWorldMousePosition(Input.mousePosition, mainCamera));
        pos = tileMap.WorldToCell(Utils.GetWorldMousePosition(Input.mousePosition, mainCamera));
        posv3 = tileMap.CellToWorld(pos);
        posv3.x += 0.16f/2f;
        posv3.y += 0.16f/2f;
        this.transform.position = posv3;
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("PlacedProp")) {
            FindObjectOfType<PropScript>().SetCanPlace(false);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("PlacedProp")) {
            FindObjectOfType<PropScript>().SetCanPlace(true);
        }
    }


    // void OnCollisionStay2D(Collision2D collision) {
    //     if(collision.gameObject.tag == "PlacedProps" && !isPaintingMaterials) {
    //         FindObjectOfType<PropScript>().SetCanPlace(false);
    //     }
    // }

    // void OnCollisionExit2D(Collision2D collision) {
    //     if(collision.gameObject.tag == "PlacedProps" && !isPaintingMaterials) {
    //         FindObjectOfType<PropScript>().SetCanPlace(true);
    //     }
    // }

    public void SetPropBrush() {
        materialBrush.SetActive(false);
        propBrush.SetActive(true);
        // SetPrefab(newPrefab);
        // Instantiate(prefab, this.transform.position, Quaternion.identity).transform.SetParent(this.gameObject.transform);
              

        // collider.bounds = Vector3
        // collider.offset = (Vector2) propBrush.GetComponent<Renderer>().bounds.extents;        
        
        // BoxCollider2D collider = this.gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        // collider.size = (Vector2) propBrush.GetComponent<Renderer>().bounds.size;
        // collider.offset = (Vector2) propBrush.GetComponent<Renderer>().bounds.center;
        // Debug.Log(propBrush.GetComponent<Renderer>().bounds);
        // Debug.Log(propBrush.GetComponent<SpriteRenderer>().sprite);
        // Debug.Log(propBrush.GetComponent<SpriteRenderer>().sprite.rect);
        // Debug.Log(propBrush.GetComponent<SpriteRenderer>().sprite.pivot);
        // Debug.Log("---");
        // Debug.Log(collider.bounds);
        // Debug.Log(propBrush.GetComponent<SpriteRenderer>().sprite.pivot.normalized);
        // Debug.Log(propBrush.GetComponent<SpriteRenderer>().bounds);
        // Debug.Log(propBrush.GetComponent<SpriteRenderer>().localBounds);
        // Debug.Log(((RectTransform) propBrush.transform).rect);
        // SetPaintingProps();
    }

    public void SetMaterialsBrush() {
        materialBrush.SetActive(true);
        propBrush.SetActive(false);
        // Destroy(prefab);
        // Destroy(borderPrefab);
        // borderPrefab = Instantiate(borderPrefab, Vector3.zero, Quaternion.identity);
        // borderPrefab.transform.SetParent(this.gameObject.transform);
        // borderPrefab.GetComponent<SpriteRenderer>().sprite = FindObjectOfType<TileScript>().GetSelectedHotbar();
    }

    public void SetMaterialsBrushSprite(Sprite sprite) {
        materialBrush.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void SetPropBrushPrefab(GameObject prefab) {
        propBrush.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

        BoxCollider2D prefabCollider = prefab.GetComponent<BoxCollider2D>();

        // collider = prefab.GetComponent<BoxCollider2D>();

        // collider.size = (Vector2) propBrush.GetComponent<Renderer>().bounds.size;
        // collider.offset = (Vector2) propBrush.GetComponent<SpriteRenderer>().localBounds.center;  
        collider.size = (Vector2) prefabCollider.size;
        collider.offset = (Vector2) prefabCollider.offset;  
    }
}
