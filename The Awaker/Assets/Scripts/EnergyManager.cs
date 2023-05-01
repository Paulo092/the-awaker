using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyManager : MonoBehaviour
{
    public int energy = 0;
    public TMP_Text energyMetterTMP;
    public AudioClip collectSound;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() { }

    public void IncrementEnergy(int amount) {
        energy += amount;
        energyMetterTMP.SetText(energy.ToString());
    }

    public void DecrementEnergy(int amount) {
        energy -= amount;
        energyMetterTMP.SetText(energy.ToString());
    }

    public int GetEnergyAmount() {
        return energy;
    }

    void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.tag == "Consumible") {
            IncrementEnergy(10 + (int) (10 * (collision.gameObject.transform.localScale.x - 1)));
            SoundManager.Instance.PlaySound(collectSound);
            Destroy(collision.gameObject);
        }
    }
}
