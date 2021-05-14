using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
   
   public static AudioClip newPointSound;
   private static AudioSource audioSrc;

   private void Start() {
      newPointSound = Resources.Load<AudioClip>("NewPoint");

      audioSrc = GetComponent<AudioSource>();
   }

   public static void PlaySound(string clip) {
      switch (clip) {
         case "point":
            audioSrc.volume = 0.3f;
            audioSrc.PlayOneShot(newPointSound);
            break;
      }
   }
}
