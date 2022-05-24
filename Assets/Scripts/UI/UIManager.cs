using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
        [SerializeField] List<GameObject> UIElements;

        public void EnableUIElement(int index)
        {
                if (index >= UIElements.Count)
                        return;

                for(int i = 0; i < UIElements.Count;i++)
                        UIElements[index].SetActive(i == index);
        }
}
