using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawn : MonoBehaviour
{
    public GameObject player, orbPrefab, objectParent;
    public float spawnRadius = .10f, spawnTick = 20f, time = 0;

    void Start() {
        InvokeRepeating("SpawnOrb", 10.0f, spawnTick);
    }

    void SpawnOrb() {
        Instantiate(orbPrefab, new Vector3(
            Random.Range(player.transform.position.x - spawnRadius, player.transform.position.x + spawnRadius),
            Random.Range(player.transform.position.y - spawnRadius, player.transform.position.y + spawnRadius),
            this.transform.position.z), Quaternion.identity).transform.SetParent(objectParent.transform);
    }
}
