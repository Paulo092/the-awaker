using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public int spawnTime = 10;
    public GameObject enemy;
    public bool isEnemySpawned = false;

    void Start() {
        // InvokeRepeating("ConsumeTheWorld", spawnTime, spawnTime);
        Invoke("TrySpawnEnemy", spawnTime);
    }

    private void TrySpawnEnemy() {
        Debug.Log("Trying to spawn enemy");
        bool conditions = FindObjectOfType<TileScript>().GetTilesPlaced() > Utils.E_MIN_TILE // Min of tiles placed to be able to spawn
                       && !isEnemySpawned;                                             // Enemy exists in game

        if(conditions) {
            Instantiate(enemy, Utils.SetLayer(FindObjectOfType<TileScript>().GetRandomCellGlobalPosition(), Utils.L_ENEMY), Quaternion.identity);
            isEnemySpawned = true;
        }

        Invoke("TrySpawnEnemy", spawnTime);
    } 

    void Update() {
        // Debug.Log(isEnemySpawned);

     }
}
