using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOrb : MonoBehaviour {
    public float energyMetter;

    void Start() {
        energyMetter = 0f;
    }

    void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag == "Consumible") {
            energyMetter += 1;
            Destroy(collision.gameObject);
        }
    }
}
