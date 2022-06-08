using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsHelper : MonoBehaviour
{
        [SerializeField] AudioSource music;
        [SerializeField] public float volume;
        [SerializeField] Scrollbar musicScroll;
        [SerializeField] Scrollbar effectScroll;

        public void UIElementAction(string modifier)
        {
                switch (modifier)
                {
                        case "music":
                                {
                                        music.volume = musicScroll.value;
                                        break;
                                }
                        case "effects":
                                {
                                        volume = effectScroll.value;
                                        break;
                                }
                }
        }
}
