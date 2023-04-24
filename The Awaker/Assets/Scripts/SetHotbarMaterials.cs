using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHotbarMaterials : MonoBehaviour {

    [ReadOnly, SerializeField] private List<Transform> hotbarItems = new List<Transform>();
    [ReadOnly, SerializeField] private int selectedIndex;
    
    void Start() {
        List<Sprite> items = FindObjectOfType<Brush>().GetHotbar();
        selectedIndex = FindObjectOfType<Brush>().GetHotbarIndex();

        foreach (Transform child in transform) {
            if(child.tag == "HotbarItem") {
                hotbarItems.Add(child);
                hotbarItems[hotbarItems.Count - 1].Find("ImageMaterial").gameObject.GetComponent<Image>().sprite = items[hotbarItems.Count - 1];
            } 
        }

        hotbarItems[selectedIndex].transform.localScale = hotbarItems[selectedIndex].transform.localScale * 1.3f;
    }

    // public void ChangeHotbarMaterial(int index, Sprite material) {
    //     hotbarItems[index].Find("ImageMaterial").GetComponent<Image> = material
    // }

    public void SetSelected(int index) {
        hotbarItems[selectedIndex].transform.localScale = hotbarItems[selectedIndex].transform.localScale / 1.3f;
        hotbarItems[index].transform.localScale = hotbarItems[selectedIndex].transform.localScale * 1.3f;
        selectedIndex = index;
    }

}
