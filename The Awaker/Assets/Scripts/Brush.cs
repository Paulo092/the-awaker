using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public Sprite asset;
    public Camera mainCamera;

    public GameObject childObject;
    private List<GameObject> tileObjects;
    private SpriteRenderer childSRenderer;

    private Vector3 placeCoord;
    private float offset = 0.16f;

    void Start() {
        childSRenderer = childObject.GetComponent<SpriteRenderer>();
        tileObjects = new List<GameObject>();
    }
    void Update() {

        childObject.transform.position = GetWorldMousePosition(Input.mousePosition, mainCamera);

        childSRenderer.sprite = asset;

        // Place
        if(Input.GetMouseButton(0)) {
            placeCoord = GetWorldMousePosition(Input.mousePosition, mainCamera);
            if(!isOccuped(placeCoord))
                tileObjects.Add(Instantiate(childObject, placeCoord, Quaternion.identity));
        }

        // Delete
        // if(Input.GetMouseButtonDown(1)) Destroy(CreateGameObjectFromSprite(asset), GetWorldMousePosition(Input.mousePosition, mainCamera), Quaternion.identity);
    }

    bool isOccuped(Vector3 position) {
        foreach (GameObject tile in tileObjects) {
            if((System.Math.Round(position.x / offset) * offset) == tile.transform.position.x 
            && (System.Math.Round(position.y / offset) * offset) == tile.transform.position.y)
                return true;
        }

        return false;
    }

    private Vector3 GetWorldMousePosition(Vector3 screenPosition, Camera mainCamera) {
        Vector3 mapPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        mapPosition.x = (float) System.Math.Round(mapPosition.x / offset) * offset;
        mapPosition.y = (float) System.Math.Round(mapPosition.y / offset) * offset;
        mapPosition.z = -1;

        return mapPosition;
    }
}
