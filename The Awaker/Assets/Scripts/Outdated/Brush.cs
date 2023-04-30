using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public Camera mainCamera;

    public List<Sprite> hotbar = new List<Sprite>();
    public GameObject brushObject, brushBorderObject, materialParent, materialPrefab;

    [ReadOnly, SerializeField] private int hotbarSelectedItem;

    private List<GameObject> placedObjects = new List<GameObject>();
    private SpriteRenderer brushSpriteRenderer;
    private int deleteTilePosition;
    private sbyte materialLayer = 0, 
                //   propLayer = -1, 
                  brushLayer = -2;
                //   brushBorderLayer = -3;

    [ReadOnly, SerializeField] private bool isTouchingPlacedObject = false;

    // private static float offset = 0.16f;

    void Start() {
        brushSpriteRenderer = brushObject.GetComponent<SpriteRenderer>();

        hotbarSelectedItem = 0;
        brushSpriteRenderer.sprite = hotbar[hotbarSelectedItem];
    }

    void Update() {
        this.gameObject.transform.position = Utils.SetLayer(Utils.GetSpacedPosition(Utils.GetWorldMousePosition(Input.mousePosition, mainCamera)), brushLayer);
        // brushObject.transform.position = Utils.SetLayer(Utils.GetSpacedPosition(Utils.GetWorldMousePosition(Input.mousePosition, mainCamera)), brushLayer);
        // brushBorderObject.transform.position = Utils.SetLayer(brushObject.transform.position, brushBorderLayer);

        Debug.Log(isTouchingPlacedObject);

        if(Input.GetMouseButton(0)) PlaceMaterial(brushObject.transform.position);
        if(Input.GetMouseButton(1)) DeleteMaterial(brushObject.transform.position);

        if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
            brushSpriteRenderer.sprite = hotbar[hotbarSelectedItem + 1 >= hotbar.Count ? hotbarSelectedItem = 0 : ++hotbarSelectedItem]; 
            FindObjectOfType<SetHotbarMaterials>().SetSelected(hotbarSelectedItem);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
            brushSpriteRenderer.sprite = hotbar[hotbarSelectedItem - 1 < 0 ? hotbarSelectedItem = hotbar.Count - 1 : --hotbarSelectedItem]; 
            FindObjectOfType<SetHotbarMaterials>().SetSelected(hotbarSelectedItem);
        }
    }

    private GameObject NewMaterialObject() {
        GameObject mat = materialPrefab;
        mat.GetComponent<SpriteRenderer>().sprite = hotbar[hotbarSelectedItem];
        return mat;
    }

    private void DeleteMaterial(Vector3 position) {
        deleteTilePosition = GetMaterialElementIndex(position);

        if(deleteTilePosition != -1) {
            Destroy(placedObjects[deleteTilePosition]);
            placedObjects.RemoveAt(deleteTilePosition);
        }
    }

    private void PlaceMaterial(Vector3 position) {
        deleteTilePosition = GetMaterialElementIndex(position);

        if(!Utils.isOverUI()) {
            if(deleteTilePosition != -1) {
                Destroy(placedObjects[deleteTilePosition]);
                placedObjects.RemoveAt(deleteTilePosition);
            }

            placedObjects.Add(Instantiate(NewMaterialObject(), Utils.SetLayer(position, materialLayer), Quaternion.identity));
            placedObjects[placedObjects.Count - 1].transform.SetParent(materialParent.transform); 
            placedObjects[placedObjects.Count - 1].tag = "PlacedObject"; 
        }
    }

    int GetMaterialElementIndex(Vector3 position) {
        for(int i = 0; i < placedObjects.Count; i++) {
            if(Utils.GetSpacedPosition(placedObjects[i].transform.position) == Utils.GetSpacedPosition(position))
                return i;
        }

        return -1;
    }

    bool isOccuped(GameObject child) {
        foreach (GameObject tile in placedObjects)
            if(Utils.GetSpacedPosition(child.transform.position) == Utils.GetSpacedPosition(tile.transform.position))
                return true;

        return false;
    }
   
    public List<Sprite> GetHotbar() {
        return hotbar;
    }

    public Sprite GetHotbarItem(int index) {
        return hotbar[index];
    }

    public int GetHotbarIndex() {
        return hotbarSelectedItem;
    }

    public List<GameObject> GetPlacedObjects() {
        return placedObjects;
    }

    void OnCollisionStay2D(Collision2D collision) {
        isTouchingPlacedObject = collision.gameObject.tag == "PlacedObject" ? true : false;
    }
}
