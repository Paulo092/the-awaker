using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public Sprite asset;
    public Camera mainCamera;

    public GameObject childObject, borderObject;
    private List<Vector3> tileObjects;
    private SpriteRenderer childSRenderer;

    private Vector3 placeCoord;
    private float offset = 0.16f;

    void Start() {
        childSRenderer = childObject.GetComponent<SpriteRenderer>();
        tileObjects = new List<Vector3>();
    }
    void Update() {

        childObject.transform.position = GetWorldMousePosition(Input.mousePosition, mainCamera);
        borderObject.transform.position = new Vector3(childObject.transform.position.x, childObject.transform.position.y, -2) ;

        childSRenderer.sprite = asset;

        // Place
        if(Input.GetMouseButton(0)) {
            if(!isOccuped(childObject.transform.position))
                tileObjects.Add(Instantiate(childObject, childObject.transform.position, Quaternion.identity).transform.position);
        }

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
}
