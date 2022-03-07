using UnityEngine;
using System.Collections.Generic;
public class Brain : MonoBehaviour
{
        public DNA dna;
        public int dnaLength;
        List<List<bool>> level;
        public float timeAlive = 0;
        public int damageDealt = 0;
        // Start is called before the first frame update
        public void Init()
        {
                dna = new DNA(dnaLength, 4);
                StartCoroutine(Move());
        }


        rivate void Update()
        {
                timeAlive += Time.deltaTime;
        }
}
