using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{
    public Tile highlightTile;
    public Tilemap highlightMap;
    // public Camera mainCamera;
    public List<Tile> hotbar = new List<Tile>();
    [ReadOnly, SerializeField] private int hotbarSelectedItem;
    public List<Vector3> availablePlaces;
    public int tilesPlaced;
    private bool isDrawnable = true;
    private float lowestPoint;

    public GameObject prefabA;
    public Tile ptile;

    public TileBase[] tileb;
    public TileBase tile;

    // Start is called before the first frame update
    void Start() {
        FindObjectOfType<Preview>().SetMaterialsBrushSprite(this.GetSelectedHotbar());
    }

    // Update is called once per frame
    void Update()
    {

        // GetRandomCellGlobalPosition();
        tileb = highlightMap.GetTilesBlock(highlightMap.cellBounds);
        // tile = tileb[0] != null ? tileb[0] : null;
        // Debug.Log(highlightMap.GetTilesBlock(highlightMap.cellBounds).Length);
        // Debug.Log(Random.Range(0, highlightMap.GetTilesBlock(highlightMap.cellBounds).Length - 1));
        // Debug.Log(highlightMap.GetTilesBlock(highlightMap.cellBounds)[Random.Range(0, highlightMap.GetTilesBlock(highlightMap.cellBounds).Length - 1)]);

        // GetRandomCellGlobalPosition();        

        Vector3Int currentCell = highlightMap.WorldToCell(Utils.GetWorldMousePosition(Input.mousePosition));

        if(Input.GetMouseButton(0) && !Utils.isOverUI() && isDrawnable) {
            // ptile.gameObject = prefabA;
            // highlightMap.SetTile(currentCell, ptile);
            if(FindObjectOfType<EnergyManager>().GetEnergyAmount() > 0) {
                if(highlightMap.GetTile(currentCell) == null)
                    FindObjectOfType<EnergyManager>().DecrementEnergy(1);
                highlightMap.SetTile(currentCell, hotbar[hotbarSelectedItem]);
                tilesPlaced++;
                
                // if(lowestPoint != null) if(currentCell.y < lowestPoint) lowestPoint = highlightMap.CellToWorld(currentCell);
                // else lowestPoint = highlightMap.CellToWorld(currentCell);

                // highlightMap.SetTileFlags(currentCell, TileFlags.None);

                // tileGameObject.transform.position = new Vector3(tileGameObject.transform.position.x, tileGameObject.transform.position.y, tileGameObject.transform.position.y);

                // Vector3 cellWorldPosition = highlightMap.CellToWorld(currentCell);
                // Debug.Log(">>> " + cellWorldPosition.y);
                // Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(1, 1, this.transform.position.y), Quaternion.Euler(0f, 0f, 0f), Vector3.one);
                // Debug.Log(">>>> " + currentCell);
                // highlightMap.SetTransformMatrix(currentCell, Matrix4x4.Translate(new Vector3(0, 0, highlightMap.CellToWorld(currentCell.y))));
                // Debug.Log(">>> " + highlightMap.GetTransformMatrix(currentCell));

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
            FindObjectOfType<SetHotbarMaterials>().SetSelected(hotbarSelectedItem);
            FindObjectOfType<Preview>().SetMaterialsBrushSprite(this.GetSelectedHotbar());
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0f && isDrawnable) {
            hotbarSelectedItem = hotbarSelectedItem - 1 < 0 ? hotbarSelectedItem = hotbar.Count - 1 : --hotbarSelectedItem; 
            FindObjectOfType<SetHotbarMaterials>().SetSelected(hotbarSelectedItem);
            FindObjectOfType<Preview>().SetMaterialsBrushSprite(this.GetSelectedHotbar());
        }
    }

    public void setDrawnable(bool value) {
        isDrawnable = value;
    }

    public void SetHotbarItem(int index, Tile newTile) {
        hotbar[index] = newTile;
    }

    public List<Tile> GetHotbar() {
        return hotbar;
    }

    public Tile GetHotbarItem(int index) {
        return hotbar[index];
    }

    public int GetHotbarIndex() {
        return hotbarSelectedItem;
    }

    public Sprite GetSelectedHotbar() {
        return hotbar[hotbarSelectedItem].sprite;
    }

    public int GetTilesPlaced() {
        return tilesPlaced;
    }

    public Tilemap GetTilemap() {
        return GetComponent<Tilemap>();
    }

    public float GetLayer(Vector3 position) {
        return highlightMap.CellToWorld(highlightMap.WorldToCell(position)).y;
    }

    public Vector3 GetRandomCellGlobalPosition() {
        // tileMap = transform.GetComponentInParent<Tilemap>();
        availablePlaces = new List<Vector3>();
 
        for (int n = highlightMap.cellBounds.xMin; n < highlightMap.cellBounds.xMax; n++) {
            for (int p = highlightMap.cellBounds.yMin; p < highlightMap.cellBounds.yMax; p++) {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)highlightMap.transform.position.y));
                Vector3 place = highlightMap.CellToWorld(localPlace);

                if (highlightMap.HasTile(localPlace)) {
                    //Tile at "place"
                    availablePlaces.Add(place);
                } else {
                    //No tile at "place"
                }
            }
        }

        return availablePlaces[Random.Range(0, availablePlaces.Count - 1)];
    }

    public void DestroyTileAt(Vector2 coordinate, int size) {
        Vector3Int circleCenter = highlightMap.WorldToCell(coordinate);

        for (int i = -size; i <= size; i++) {
            for (int j = -size; j <= size; j++) {
                if(!(Mathf.Abs(i) == size && Mathf.Abs(j) == size))
                    highlightMap.SetTile(circleCenter + new Vector3Int(i, j), null);
            }
        }
    }
}
