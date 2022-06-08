using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class LevelManager : MonoBehaviour
{
        public string difficulty;
        public string seed;
        public LevelInformation levelInformation;
        [SerializeField] NavMeshSurface2d surface;
        [SerializeField] GameObject player;
        // Start is called before the first frame update
        public void GenerateLevel()
        {
                levelInformation = new LevelInformation(difficulty, seed,player);
                surface.BuildNavMesh();
        }
}
