using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


// Last Time Organized: 01/05/23
public class PropScript : MonoBehaviour
{
    public List<GameObject> hotbar = new List<GameObject>();
    
    [SerializeField, ReadOnly] 
    private bool canPlace = true,       // If is not colliding with other prop
                 isDrawnable = false;   // If prop hotbar is active
    
    private int hotbarSelectedIndex;
    private GameObject tileGameObject;
    private Tile tile;
    private Tilemap tilemap;
    private Vector3Int currentCell;

    void Start() {
        tilemap = GetComponent<Tilemap>();
        tile = ScriptableObject.CreateInstance<Tile>();
        hotbarSelectedIndex = 0;
    }

    void Update() {

        if(Input.GetMouseButton(0) && !Utils.isOverUI()  && canPlace  && isDrawnable  && FindObjectOfType<EnergyManager>().GetEnergyAmount() > 0) {
            currentCell = tilemap.WorldToCell(Utils.GetWorldMousePosition(Input.mousePosition));

            if(!tilemap.HasTile(currentCell)) FindObjectOfType<EnergyManager>().DecrementEnergy(1);

            tile.gameObject = hotbar[hotbarSelectedIndex];
            tilemap.SetTile(currentCell, tile);

            tileGameObject = tilemap.GetInstantiatedObject(currentCell); 
            tileGameObject.transform.position = Utils.GetLayeredPosition(tileGameObject.transform.position);
        }  
        else if(Input.GetMouseButton(1) && isDrawnable) {
            currentCell = tilemap.WorldToCell(Utils.GetWorldMousePosition(Input.mousePosition));

            if(tilemap.HasTile(currentCell)) FindObjectOfType<EnergyManager>().IncrementEnergy(1);

            tilemap.SetTile(currentCell, null);
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f && isDrawnable) {
            hotbarSelectedIndex = (hotbarSelectedIndex + 1) % hotbar.Count; 
            FindObjectOfType<SetHotbarProps>().SetSelected(hotbarSelectedIndex);
            FindObjectOfType<Preview>().SetPropBrushPrefab(GetHotbarSelectedPrefab());
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0f && isDrawnable) {
            hotbarSelectedIndex = hotbarSelectedIndex - 1 < 0 ? hotbarSelectedIndex = hotbar.Count - 1 : --hotbarSelectedIndex; 
            FindObjectOfType<SetHotbarProps>().SetSelected(hotbarSelectedIndex);
            FindObjectOfType<Preview>().SetPropBrushPrefab(GetHotbarSelectedPrefab());
        }
    }

    public void SetHotbarItem(int index, GameObject newPrefab) {
        hotbar[index] = newPrefab;
    }

    // Getters & Setters
    public List<GameObject> GetHotbar() { return hotbar; }
    public GameObject GetHotbarSelectedPrefab() { return hotbar[hotbarSelectedIndex]; }
    public int GetHotbarIndex() { return hotbarSelectedIndex; }
    public void SetDrawnable(bool value) { isDrawnable = value; }
    public void SetCanPlace(bool value) { canPlace = value; }
}
