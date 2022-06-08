using UnityEngine;

public class UIChangeDifficulty : MonoBehaviour
{
        [SerializeField] LevelManager levelManager;

        public void UIElementAction(string difficulty)
        {
                levelManager.difficulty = difficulty;
        }
}
