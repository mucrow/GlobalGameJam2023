using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour {
  [SerializeField]
  AudioSource _source;

  public AudioClip[] Build;
  public AudioClip[] Clang;
  public AudioClip[] Dig;

  public void PlaySound(AudioClip[] roundRobins) {
    AudioClip clip = PickRoundRobin(roundRobins);
    _source.PlayOneShot(clip);
  }

  AudioClip PickRoundRobin(AudioClip[] roundRobins) {
    int index = Random.Range(0, roundRobins.Length);
    return roundRobins[index];
  }
}
