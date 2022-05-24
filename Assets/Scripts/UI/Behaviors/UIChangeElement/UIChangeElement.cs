using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChangeElement : MonoBehaviour
{
        [SerializeField] UIManager UIManager;
        public void UIElementAction(int index)
        {
                UIManager.EnableUIElement(index);
        }
}
