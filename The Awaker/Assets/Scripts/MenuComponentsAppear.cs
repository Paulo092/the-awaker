using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuComponentsAppear : MonoBehaviour
{
    public List<GameObject> objectOrder = new List<GameObject>();
    public float timeInterval = 1f, fadeInTime = 0.5f;

    public Color32 defaultColor = new Color32(255, 255, 255, 1);
    public Color32 invColor = new Color32(255, 255, 255, 0);

    private YieldInstruction fadeInstruction = new YieldInstruction();

    // Start is called before the first frame update
    void Start() {
        foreach (GameObject comp in objectOrder) {
            TextMeshProUGUI textmeshPro = comp.GetComponent<TextMeshProUGUI>();
            textmeshPro.color = invColor;
        }
        StartCoroutine(ShowComponents());
    }

    IEnumerator ShowComponents() {
        yield return new WaitForSeconds(timeInterval);
        foreach (GameObject comp in objectOrder) {
            StartCoroutine(FadeIn(comp));
            yield return new WaitForSeconds(timeInterval);
        }
    }

    IEnumerator FadeIn(GameObject imageGameObject) {
        var image = imageGameObject.GetComponent<TextMeshProUGUI>();
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeInTime) {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime ;
            c.a = Mathf.Clamp01(elapsedTime / fadeInTime);
            image.color = c;
        }
}
}
