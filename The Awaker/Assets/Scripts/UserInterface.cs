using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UserInterface : MonoBehaviour
{
    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonTileSelect = root.Q<Button>("TileModeSelect");
        Button buttonPropSelect = root.Q<Button>("PropModeSelect");

        // buttonTileSelect += () => ;
        // buttonPropSelect
    }
}
