using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
        public string difficulty;
        public string seed;
        public LevelInformation info;
        [SerializeField] NavMeshSurface2d surface;
        // Start is called before the first frame update
        void Start()
        {
                info = new LevelInformation(difficulty, seed);
                surface.BuildNavMesh();
        }
        //update to do this once player decides to build a level;
}
