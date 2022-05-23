using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshComponents;
using UnityEngine.AI;

public class Helper : MonoBehaviour
{
        public string difficulty;
        public string seed;
        [SerializeField] NavMeshSurface2d surface;
        // Start is called before the first frame update
        void Start()
        {
                LevelInformation info = new LevelInformation(difficulty, seed);
                surface.BuildNavMesh();
        }

        // Update is called once per frame
        void Update()
        {

        }
}
