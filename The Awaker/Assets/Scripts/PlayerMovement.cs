using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.005f;

    void Start() {
        speed = 0.005f;
    }

    void Update() {
        this.transform.position = new Vector3(
            this.transform.position.x + Input.GetAxis("Horizontal") * speed, 
            this.transform.position.y + Input.GetAxis("Vertical") * speed, 
            this.transform.position.z);
    }
}
