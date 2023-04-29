using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillEvent : MonoBehaviour {
    void OnCollisionStay2D(Collision2D collider) {
        if(collider.gameObject.tag == "Enemy" && Input.GetKey(KeyCode.E)) {
            Destroy(collider.gameObject);
            FindObjectOfType<EnemySpawn>().isEnemySpawned = false;
        }
    }

}
