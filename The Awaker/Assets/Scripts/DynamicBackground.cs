using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DynamicBackground : MonoBehaviour
{

    public float TimeCycle = 0.05f;
    public GameObject canvas;
    private YieldInstruction fadeInstruction = new YieldInstruction();
    private float fadeInTime = 0.2f;
    private float fadeTime = 2f;
    private float padding = 100f;
    
    void Start() {
        InvokeRepeating("RandomAquarella", 1.0f, TimeCycle);
        InvokeRepeating("RandomAquarella", 2.0f, TimeCycle + 1);
        InvokeRepeating("RandomAquarella", 3.0f, TimeCycle - 1);
    }

    public void RandomAquarella() {
        Vector2 randomScreenPosition = new Vector2(
                    Random.Range(-Screen.width + padding, Screen.width - padding), 
                    Random.Range(-Screen.height + padding, Screen.height - padding)
                );
        
        float randomImageIndex = Random.Range(1f, 4f);

        GameObject imgo = NewAquarella(randomScreenPosition, randomImageIndex);
    }

    public GameObject NewAquarella(Vector2 position, float imageIndex) {
        GameObject imgObject = new GameObject("Aquarella");

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        // trans.transform.SetParent(this.gameObject.transform);
        trans.localScale = new Vector3(3f, 3f, 1f);
        trans.anchoredPosition = new Vector2(position.x, position.y); // setting position, will be on center

        Image image = imgObject.AddComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
        Texture2D tex = Resources.Load ("Images/aquarella_" + (int) imageIndex) as Texture2D;
        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        imgObject.transform.SetParent(canvas.transform);

        // Debug.Log(this.gameObject);
        imgObject.transform.SetParent(this.gameObject.transform);

        StartCoroutine(FadeIn(imgObject));

        return imgObject;

    }

    IEnumerator FadeIn(GameObject imageGameObject) {
        Image image = imageGameObject.GetComponent<Image>();
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeInTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime ;
            c.a = Mathf.Clamp01(elapsedTime / fadeInTime);
            image.color = c;
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(FadeOut(imageGameObject));
    }

    IEnumerator FadeOut(GameObject imageGameObject)
    {
        Image image = imageGameObject.GetComponent<Image>();

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
