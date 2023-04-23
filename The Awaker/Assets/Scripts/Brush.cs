using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public Sprite asset;
    public Camera mainCamera;

    public GameObject childObject, borderObject;
    private List<Vector3> tileObjects;
    public List<Sprite> hotbar = new List<Sprite>();
    private SpriteRenderer childSRenderer;

    private Vector3 placeCoord;
    private float offset = 0.16f;
    public int textureIndex;

    void Start() {
        childSRenderer = childObject.GetComponent<SpriteRenderer>();
        tileObjects = new List<Vector3>();
        // hotbar = new List<Sprite>();

        textureIndex = 0;
        childSRenderer.sprite = hotbar[textureIndex];
    }

    void Update() {

        childObject.transform.position = GetWorldMousePosition(Input.mousePosition, mainCamera);
        borderObject.transform.position = new Vector3(childObject.transform.position.x, childObject.transform.position.y, -2);

        // childSRenderer.sprite = asset;

        // Place
        if(Input.GetMouseButton(0) && !isOverUI() && !isOccuped(childObject.transform.position)) {
            // if()
                tileObjects.Add(Instantiate(childObject, childObject.transform.position, Quaternion.identity).transform.position);
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
            childSRenderer.sprite = hotbar[textureIndex + 1 >= hotbar.Count ? textureIndex = 0 : ++textureIndex]; 
            FindObjectOfType<SetHotbarMaterials>().setSelected(textureIndex);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
            childSRenderer.sprite = hotbar[textureIndex - 1 < 0 ? textureIndex = hotbar.Count - 1 : --textureIndex]; 
            FindObjectOfType<SetHotbarMaterials>().setSelected(textureIndex);
        }
        // if(Input.GetAxis("Mouse ScrollWheel") > 0f) Debug.Log("Scroll"); 

        // Delete
        // if(Input.GetMouseButtonDown(1)) Destroy(CreateGameObjectFromSprite(asset), GetWorldMousePosition(Input.mousePosition, mainCamera), Quaternion.identity);
    }

    bool isOccuped(Vector3 position) {
        foreach (Vector3 tile in tileObjects) {
            // if((System.Math.Round(position.x / offset) * offset) == tile.x 
            // && (System.Math.Round(position.y / offset) * offset) == tile.y)
            //     return true;
            if(GetSpacedPosition(position) == GetSpacedPosition(tile))
                return true;
        }

        return false;
    }

    private bool isOverUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }

    Vector3 GetSpacedPosition(Vector3 position) {
        return new Vector3((float) Mathf.Round(position.x / offset) * offset, (float) Mathf.Round(position.y / offset) * offset, position.z);
    }

    private Vector3 GetWorldMousePosition(Vector3 screenPosition, Camera mainCamera) {
        Vector3 mapPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        mapPosition.x = (float) Mathf.Round(mapPosition.x / offset) * offset;
        mapPosition.y = (float) Mathf.Round(mapPosition.y / offset) * offset;
        mapPosition.z = -1;

        return mapPosition;
    }
    
    public List<Sprite> GetHotbar() {
        return hotbar;
    }

    public Sprite GetHotbarItem(int index) {
        return hotbar[index];
    }

    public void ChangePalette(string materialName) {
        // Object[] objects = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("Assets/Sprites/Materials/Grass");
        // Object[] objects = Resources.Load("Materials/Grass");
        // var sprites = objects.Where(q => q is Sprite).Cast<Sprite>();
        
        // Debug.Log(childSRenderer.sprite);
        // childSRenderer.sprite = Resources.Load("Materials/Grass_1") as Texture2D;
        // Debug.Log(Resources.Load("Materials/Grass_0") as Texture2D);
        // Debug.Log(Resources.Load<Sprite>(sprites[0]));
        // Debug.Log(objects.Length);
        // Debug.Log(Resources.Load<Sprite>("Materials/Grass"));
    }
}
