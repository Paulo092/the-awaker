using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHotbarMaterials : MonoBehaviour {

    public List<Transform> hotbarItems;
    public Image test;
    public Sprite test2;
    public int selectedIndex = 0;
    
    void Start()
    {
        hotbarItems = new List<Transform>();
        
        foreach (Transform child in transform)
            if(child.name != "HotItemMore") hotbarItems.Add(child);
                
        // test = hotbarItems[0].Find("ImageMaterial").gameObject.GetComponent<Image>();
        // hotbarItems[0].Find("ImageMaterial").gameObject.GetComponent<Image>().sprite = test2;
        // hotbarItems[selectedIndex].Find("ImageSelected").gameObject.SetActive(true);
        hotbarItems[selectedIndex].transform.localScale = hotbarItems[selectedIndex].transform.localScale * 1.3f;
        // Debug.Log(hotbarItems[0].Find("ImageMaterial").gameObject.GetComponent<Image>().sprite);


        List<Sprite> items = FindObjectOfType<Brush>().GetHotbar();
        for(int i = 0; i < items.Count; i++) {
            hotbarItems[i].Find("ImageMaterial").gameObject.GetComponent<Image>().sprite = items[i];
        }

    }

    // public void ChangeHotbarMaterial(int index, Sprite material) {
    //     // hotbarItems[index].Find("ImageMaterial").GetComponent<Image> = material
    // }

    public void setSelected(int index) {
        hotbarItems[selectedIndex].transform.localScale = hotbarItems[selectedIndex].transform.localScale / 1.3f;
        hotbarItems[index].transform.localScale = hotbarItems[selectedIndex].transform.localScale * 1.3f;
        selectedIndex = index;
    }

}
