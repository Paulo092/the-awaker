using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillEvent : MonoBehaviour {
    [SerializeField] public AudioClip dieSound;

    private YieldInstruction fadeInstruction = new YieldInstruction();
    private float fadeTime = 0.5f;

    // void OnCollisionStay2D(Collision2D collider) {
    //     if(collider.gameObject.tag == "Enemy" && Input.GetKey(KeyCode.E)) {
    //         SoundManager.Instance.PlaySound(dieSound);
    //         StartCoroutine(FadeOut(collider.gameObject));
    //         FindObjectOfType<EnemySpawn>().isEnemySpawned = false;
    //     }
    // }

    IEnumerator FadeOut(GameObject target) {
        float elapsedTime = 0.0f;
        Color c = target.GetComponent<Renderer>().material.color;

        while (elapsedTime < fadeTime) {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            if(target != null) 
                target.GetComponent<Renderer>().material.color = c;
        }

        Destroy(target);     
    }
}


