using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEditor;

// ReadOnly properties
public class ReadOnlyAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        var previousGUIState = GUI.enabled;
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = previousGUIState;
    }
}

public class Utils : MonoBehaviour {    
    // private Vector3 GetWorldMousePosition(Vector3 screenPosition, Camera mainCamera) {
    //     Vector3 mapPosition = mainCamera.ScreenToWorldPoint(screenPosition);
    //     mapPosition.x = (float) Mathf.Round(mapPosition.x / offset) * offset;
    //     mapPosition.y = (float) Mathf.Round(mapPosition.y / offset) * offset;
    //     mapPosition.z = -1;

    //     return mapPosition;
    // }

    public static Vector3 SetLayer(Vector3 coordinate, int newLayer) {
        return new Vector3(coordinate.x, coordinate.y, newLayer);
    }

    public static Vector3 GetWorldMousePosition(Vector3 screenPosition, Camera mainCamera) {
        return Utils.SetLayer(mainCamera.ScreenToWorldPoint(screenPosition), 0);
    }

    public static bool isOverUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }
}
