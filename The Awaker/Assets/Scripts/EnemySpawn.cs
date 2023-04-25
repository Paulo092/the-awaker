using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public int firstSpawnIn = 10;
    public GameObject enemy;

    void Start() {
        InvokeRepeating("ConsumeTheWorld", firstSpawnIn, firstSpawnIn);
    }

    void Update() {
        
    }

    private bool IsGoodToGO() {
        if(FindObjectOfType<Brush>().GetPlacedObjects().Count > 20) {
            return true;
        }

        return false;
    }

    private void ConsumeTheWorld() {
        enemy.SetActive(true);
        if(IsGoodToGO()) {
            enemy.transform.position = FindObjectOfType<Brush>().GetPlacedObjects()[Random.Range(0, FindObjectOfType<Brush>().GetPlacedObjects().Count - 1)].transform.position;
        }
        else Debug.Log("Bad");
    }
}
