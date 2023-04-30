using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PropScript : MonoBehaviour
{
    public Tile highlightTile;
    public Tilemap highlightMap;
    public Camera mainCamera;
    public List<GameObject> hotbar = new List<GameObject>();
    [ReadOnly, SerializeField] private int hotbarSelectedItem;
    public List<Vector3> availablePlaces;
    public int tilesPlaced;

    // public GameObject prefabA;
    public Tile ptile;

    public TileBase[] tileb;
    public TileBase tile;

    private bool canPlace = true;
    private bool isDrawnable = false;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // GetRandomCellGlobalPosition();
        // tileb = highlightMap.GetTilesBlock(highlightMap.cellBounds);
        // tile = tileb[0] != null ? tileb[0] : null;
        // Debug.Log(highlightMap.GetTilesBlock(highlightMap.cellBounds).Length);
        // Debug.Log(Random.Range(0, highlightMap.GetTilesBlock(highlightMap.cellBounds).Length - 1));
        // Debug.Log(highlightMap.GetTilesBlock(highlightMap.cellBounds)[Random.Range(0, highlightMap.GetTilesBlock(highlightMap.cellBounds).Length - 1)]);

        // GetRandomCellGlobalPosition();        

        Vector3Int currentCell = highlightMap.WorldToCell(Utils.GetWorldMousePosition(Input.mousePosition, mainCamera));

        if(Input.GetMouseButton(0) && !Utils.isOverUI() && canPlace && isDrawnable) {
            if(FindObjectOfType<EnergyManager>().GetEnergyAmount() > 0) {
                if(highlightMap.GetTile(currentCell) == null)
                    FindObjectOfType<EnergyManager>().DecrementEnergy(1);

                ptile.gameObject = hotbar[0];
                highlightMap.SetTile(currentCell, ptile);
                // highlightMap.SetTile(currentCell, hotbar[hotbarSelectedItem]);
                tilesPlaced++;
            }
        } 
        else if(Input.GetMouseButton(1) && isDrawnable) {
            if(highlightMap.GetTile(currentCell) != null)
                FindObjectOfType<EnergyManager>().IncrementEnergy(1);
            highlightMap.SetTile(currentCell, null);
            tilesPlaced--;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f && isDrawnable) {
            hotbarSelectedItem = (hotbarSelectedItem + 1) % hotbar.Count; 
            FindObjectOfType<SetHotbarProps>().SetSelected(hotbarSelectedItem);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0f && isDrawnable) {
            hotbarSelectedItem = hotbarSelectedItem - 1 < 0 ? hotbarSelectedItem = hotbar.Count - 1 : --hotbarSelectedItem; 
            FindObjectOfType<SetHotbarProps>().SetSelected(hotbarSelectedItem);
        }
    }

    public void setDrawnable(bool value) {
        isDrawnable = value;
    }

    public void SetCanPlace(bool value) {
        canPlace = value;
    }

    public List<GameObject> GetHotbar() {
        return hotbar;
    }

    public GameObject GetHotbarItem(int index) {
        return hotbar[index];
    }

    public int GetHotbarIndex() {
        return hotbarSelectedItem;
    }

    // public int GetTilesPlaced() {
    //     return tilesPlaced;
    // }

    // public Vector3 GetRandomCellGlobalPosition() {
    //     // tileMap = transform.GetComponentInParent<Tilemap>();
    //     availablePlaces = new List<Vector3>();
 
    //     for (int n = highlightMap.cellBounds.xMin; n < highlightMap.cellBounds.xMax; n++) {
    //         for (int p = highlightMap.cellBounds.yMin; p < highlightMap.cellBounds.yMax; p++) {
    //             Vector3Int localPlace = (new Vector3Int(n, p, (int)highlightMap.transform.position.y));
    //             Vector3 place = highlightMap.CellToWorld(localPlace);

    //             if (highlightMap.HasTile(localPlace)) {
    //                 //Tile at "place"
    //                 availablePlaces.Add(place);
    //             } else {
    //                 //No tile at "place"
    //             }
    //         }
    //     }

    //     return availablePlaces[Random.Range(0, availablePlaces.Count - 1)];
    // }

    // public void DestroyTileAt(Vector3 coordinate) {
    //     Vector3Int circleCenter = highlightMap.WorldToCell(coordinate);

    //     highlightMap.SetTile(circleCenter, null);
    //     highlightMap.SetTile(circleCenter + new Vector3Int(1, 0), null);
    //     highlightMap.SetTile(circleCenter + new Vector3Int(0, 1), null);
    //     highlightMap.SetTile(circleCenter + new Vector3Int(-1, 0), null);
    //     highlightMap.SetTile(circleCenter + new Vector3Int(0, -1), null);
    // }

    // public void DebugRTile() {
    //     // Debug.Log(GetRandomCellGlobalPosition());
    //     GetRandomCellGlobalPosition();
    // }

}