using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChangeElement : MonoBehaviour
{
        [SerializeField] UIManager uiManager;
        public void UIElementAction(int index)
        {
                uiManager.EnableUIElement(index);
        }
}
