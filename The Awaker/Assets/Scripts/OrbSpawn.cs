using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawn : MonoBehaviour
{
    public GameObject player, orbPrefab, objectParent;
    public float spawnRadius = 1, spawnTick = 20f, time = 0;

    void Start() {
        InvokeRepeating("SpawnOrb", 10.0f, spawnTick);
    }

    void SpawnOrb() {
        Instantiate(orbPrefab, new Vector3(
            Random.Range(player.transform.position.x - spawnRadius * Utils.offset, player.transform.position.x + spawnRadius * Utils.offset),
            Random.Range(player.transform.position.y - spawnRadius * Utils.offset, player.transform.position.y + spawnRadius * Utils.offset),
            this.transform.position.z), Quaternion.identity).transform.SetParent(objectParent.transform);
    }
}
