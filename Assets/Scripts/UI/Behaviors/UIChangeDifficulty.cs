using UnityEngine;

public class UIChangeDifficulty : MonoBehaviour
{
        [SerializeField] LevelManager levelManager;
        [SerializeField] PopulationManager theQueen;
        public void UIElementAction(string difficulty)
        {
                switch (difficulty)
                {
                        case "Easy":
                                theQueen.populationSize = 50;
                                break;
                        case "Normal":
                                theQueen.populationSize = 60;
                                break;
                        case "Hard":
                                theQueen.populationSize = 80;
                                break;
                }
                levelManager.difficulty = difficulty;
        }
}
