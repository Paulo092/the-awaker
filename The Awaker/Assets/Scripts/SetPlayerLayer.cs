using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerLayer : MonoBehaviour
{
    public GameObject player;
    public float backLayer = 0f, frontLayer = -2f;

    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        // defaultZ = player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other) {
        position = player.transform.position;
        position.z = frontLayer;
        player.transform.position = position;
    }

    void OnTriggerExit2D(Collider2D other) {
        position = player.transform.position;
        position.z = backLayer;
        player.transform.position = position;
    }
    
}
