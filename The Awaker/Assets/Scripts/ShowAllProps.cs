using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine;

public class ShowAllProps : MonoBehaviour
{
    public List<GameObject> props;
    public GameObject content, itemPrefab, instance;

    // Start is called before the first frame update
    void Start()
    {
        // itemPrefab.transform.localScale = new Vector3(1, 1, 1);

        foreach (GameObject mat in props) {
            instance = Instantiate(itemPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            instance.transform.SetParent(content.transform);
            instance.transform.localScale = new Vector3(1, 1, 1);
            instance.transform.Find("ImageMaterial").gameObject.GetComponent<Image>().sprite = mat.GetComponent<SpriteRenderer>().sprite;

            instance.GetComponent<Button>().onClick.AddListener ( delegate {
                int hotbarSelectedIndex = FindObjectOfType<PropScript>().GetHotbarIndex();

                FindObjectOfType<PropScript>().SetHotbarItem(hotbarSelectedIndex, mat);
                FindObjectOfType<SetHotbarProps>().SetHotbarItem(hotbarSelectedIndex, mat.GetComponent<SpriteRenderer>().sprite);
                FindObjectOfType<Preview>().SetPropBrushPrefab(mat);
                this.transform.gameObject.SetActive(false);
            });
        }
    }
}
