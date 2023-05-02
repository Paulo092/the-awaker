using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{

    public GameObject propHotbar, materialHotbar; 

    void Start()
    {
        materialHotbar.SetActive(true);    
        propHotbar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleHotbar() {
        materialHotbar.SetActive(!materialHotbar.activeSelf);
        propHotbar.SetActive(!propHotbar.activeSelf);
    }

    public void SetDrawnable() {
        if(materialHotbar.activeSelf) FindObjectOfType<TileScript>().setDrawnable(true);
        else FindObjectOfType<TileScript>().setDrawnable(false);

        if(propHotbar.activeSelf) FindObjectOfType<PropScript>().SetDrawnable(true);
        else FindObjectOfType<PropScript>().SetDrawnable(false);
    }
}
