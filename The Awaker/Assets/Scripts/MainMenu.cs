using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour {
    public List<GameObject> menuComponents = new List<GameObject>();
    private YieldInstruction fadeInstruction = new YieldInstruction();
    private float fadeTime = 2f;

    public GameObject intro;

    public void PlayGame() {
        intro.SetActive(true);
        Invoke("GotoGame", 5.0f);

        foreach (GameObject component in menuComponents) {
            component.SetActive(false);
        }

    }

    void GotoGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator FadeOut(GameObject imageGameObject) {
        var image = imageGameObject.GetComponent<TextMeshProUGUI>();

        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime ;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }

        Destroy(imageGameObject);
    }
}
