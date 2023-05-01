using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawn : MonoBehaviour
{
    public GameObject player, orbPrefab, objectParent;
    public float spawnRadius = 1, spawnTick = 20f, time = 0;
    private GameObject instance;
    private float power;

    void Start() {
        InvokeRepeating("SpawnOrb", 10.0f, spawnTick);
    }

    void SpawnOrb() {
        power = Random.Range(1.0f, 1.5f);

        Vector3 instancePosition = new Vector3(
            Random.Range(player.transform.position.x - spawnRadius * Utils.offset, player.transform.position.x + spawnRadius * Utils.offset),
            Random.Range(player.transform.position.y - spawnRadius * Utils.offset, player.transform.position.y + spawnRadius * Utils.offset));
        
        instancePosition.z = instancePosition.y;

        instance = Instantiate(orbPrefab, instancePosition, Quaternion.identity);
        instance.transform.SetParent(objectParent.transform);
        instance.transform.localScale = new Vector3(power, power, 1);
    }
}
