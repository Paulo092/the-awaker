using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicPeriodicaly : MonoBehaviour {
    [SerializeField] public List<AudioClip> SoundTrack;
    private static float minutes = 60.0f;

    void Start() {
        InvokeRepeating("PlayRandomMusic", 0.5f * minutes, 5f * minutes);
    }

    private void PlayRandomMusic() {
        SoundManager.Instance.PlaySound(SoundTrack[Random.Range(0, SoundTrack.Count - 1)]);
    }

}
