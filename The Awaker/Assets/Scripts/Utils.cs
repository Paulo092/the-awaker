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

    public static float offset = 0.16f;

    public static Vector3 SetLayer(Vector3 coordinate, int newLayer) {
        return new Vector3(coordinate.x, coordinate.y, newLayer);
    }

    public static Vector3 GetWorldMousePosition(Vector3 screenPosition, Camera mainCamera) {
        return Utils.SetLayer(mainCamera.ScreenToWorldPoint(screenPosition), 0);
    }

    public static bool isOverUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }

    public static Vector3 GetSpacedPosition(Vector3 position) {
        return new Vector3((float) Mathf.Round(position.x / offset) * offset, (float) Mathf.Round(position.y / offset) * offset, 0);
    }
}
