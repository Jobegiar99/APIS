using UnityEngine;
using UnityEngine.SceneManagement;
public class UIPauseMenu : MonoBehaviour
{
        [Header("Containers")]
        [SerializeField] Transform containers;
        [SerializeField] Transform botContainer;
        [SerializeField] Transform playerContainer;
        [Header("Game Objects")]
        [SerializeField] GameObject UIManager;
        [SerializeField] UIManager manager;
        [SerializeField] UIReturnFromSettings settingsHelper;


        public void ActivatePause()
        {
                for(int i = 0; i < containers.childCount; i++)
                {
                        if (containers.GetChild(i) == UIManager)
                                continue;
                        containers.GetChild(i).gameObject.SetActive(false);
                }
        }

        public void DeactivatePause()
        {
                for(int i = 0; i < containers.childCount; i ++)
                {
                        containers.GetChild(i).gameObject.SetActive(true);
                }
                manager.EnableUIElement(2);
        }

        public void GoToSettings()
        {
                settingsHelper.cameFromIndex = 3;
                manager.EnableUIElement(4);
        }
        public void GoToMainMenu()
        {
                DeactivatePause();
                SceneManager.LoadScene(0);
        }
        
}
