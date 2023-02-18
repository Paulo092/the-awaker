using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public Sprite asset;
    public Camera mainCamera;

    public GameObject childObject;
    private SpriteRenderer childSRenderer;

    private float offset = 0.16f;

    // 0, 0
    // 0, 0

    // 1.00, 0
    // 0.16, 0

    private Vector3 GetWorldMousePosition(Vector3 screenPosition, Camera mainCamera) {
        Vector3 mapPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        mapPosition.x = (float) System.Math.Round(mapPosition.x / offset) * offset;
        mapPosition.y = (float) System.Math.Round(mapPosition.y / offset) * offset;
        mapPosition.z = -1;

        return mapPosition;
    }

    void Start() {
        childSRenderer = childObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        childObject.transform.position = GetWorldMousePosition(Input.mousePosition, mainCamera);

        childSRenderer.sprite = asset;

        // Place
        if(Input.GetMouseButtonDown(0)) Instantiate(childObject, GetWorldMousePosition(Input.mousePosition, mainCamera), Quaternion.identity);
        // Delete
        // if(Input.GetMouseButtonDown(1)) Destroy(CreateGameObjectFromSprite(asset), GetWorldMousePosition(Input.mousePosition, mainCamera), Quaternion.identity);
    }
}
