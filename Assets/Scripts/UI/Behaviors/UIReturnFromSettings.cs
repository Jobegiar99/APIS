using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReturnFromSettings : MonoBehaviour
{
        [SerializeField] UIManager manager;
        public int cameFromIndex;

        public void ReturnFromSettings()
        {
                manager.EnableUIElement(cameFromIndex);
        }

        public void SetIndex(int index)
        {
                this.cameFromIndex = index;
        }

}
