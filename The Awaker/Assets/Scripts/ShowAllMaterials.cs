using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine;


public class ShowAllMaterials : MonoBehaviour
{
    public List<Tile> materials;
    public GameObject content, itemPrefab, instance;

    // Start is called before the first frame update
    void Start()
    {
        // itemPrefab.transform.localScale = new Vector3(1, 1, 1);

        foreach (Tile mat in materials) {
            instance = Instantiate(itemPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            instance.transform.SetParent(content.transform);
            instance.transform.localScale = new Vector3(1, 1, 1);
            instance.transform.Find("ImageMaterial").gameObject.GetComponent<Image>().sprite = mat.sprite;

            instance.GetComponent<Button>().onClick.AddListener ( delegate {
                int hotbarSelectedIndex = FindObjectOfType<TileScript>().GetHotbarIndex();

                FindObjectOfType<TileScript>().SetHotbarItem(hotbarSelectedIndex, mat);
                FindObjectOfType<SetHotbarMaterials>().SetHotbarItem(hotbarSelectedIndex, mat.sprite);
                FindObjectOfType<Preview>().SetMaterialsBrushSprite(mat.sprite);
                this.transform.gameObject.SetActive(false);
            });
        }
    }
}
