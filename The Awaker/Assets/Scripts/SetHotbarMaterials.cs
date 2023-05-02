using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class SetHotbarMaterials : MonoBehaviour {

    [ReadOnly, SerializeField] private List<Transform> hotbarItems = new List<Transform>();
    [ReadOnly, SerializeField] private int selectedIndex;
    
    void Start() {
        List<Tile> items = FindObjectOfType<TileScript>().GetHotbar();
        selectedIndex = FindObjectOfType<TileScript>().GetHotbarIndex();

        // Debug.Log(items.Count);
        foreach (Transform child in transform) {
            if(child.tag == "HotbarItem") {
                hotbarItems.Add(child);
                hotbarItems[hotbarItems.Count - 1].Find("ImageMaterial").gameObject.GetComponent<Image>().sprite = items[hotbarItems.Count - 1].sprite;
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

    public void SetHotbarItem(int index, Sprite newSprite) {
        hotbarItems[index].Find("ImageMaterial").gameObject.GetComponent<Image>().sprite = newSprite;
    }

    public void ToggleMaterials(GameObject panel) {
        panel.SetActive(!panel.activeSelf);
    }

}
