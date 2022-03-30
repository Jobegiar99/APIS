using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
        public string difficulty;
        public string seed;
        // Start is called before the first frame update
        void Start()
        {
                LevelInformation info = new LevelInformation(difficulty, seed);
        }

        // Update is called once per frame
        void Update()
        {

        }
}
