using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
        [SerializeField] public List<GameObject> UIElements;
        [SerializeField] AudioSource music;
        [SerializeField] AudioClip menu;

        private void Start()
        {
                for (int i = 1; i < UIElements.Count; i++)
                        UIElements[i].gameObject.SetActive(false);
        }
        public void EnableUIElement(int index)
        {
                for (int i = 0; i < UIElements.Count; i++)
                {
                        UIElements[i].gameObject.SetActive(i == index);
                }
                if (index == 0)
                        music.clip = menu;
        }
}
