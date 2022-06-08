using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGenerateLevel : MonoBehaviour
{       
        [Header("Level configuration")]
        [SerializeField] UIManager uiManager;
        [SerializeField] LevelManager levelManager;
        [SerializeField] AudioSource music;
        [SerializeField] AudioClip gameplay;
        [SerializeField] TextMeshProUGUI seedText;
        [SerializeField] PopulationManager theQueen;
        [SerializeField] GameObject player;
        public void UIElementAction(int index)
        {
                if ( seedText.text.Length > 0)
                {
                        Random.InitState(seedText.text.GetHashCode());
                }
                else
                {
                        Random.InitState(Random.Range(int.MinValue, int.MaxValue));
                }
                levelManager.GenerateLevel();
                uiManager.EnableUIElement(index);
                theQueen.StartRound();
                music.clip = gameplay;
                music.Play();
                player.gameObject.SetActive(true);
                uiManager.EnableUIElement(2);
        }
}
