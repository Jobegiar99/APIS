using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
        #region Parameters
        public string gene;
        public char type;
        public Vector2Int entrance;
        public char[] objectives = new char[3];
        public float hostility;
        public float fear;
        public float bravery;
        public float stamina;
        public float tolerance;
        public float weakness;
        private List<char> enemyTypes = new List<char> { 'A', 'B', 'C' };
        List<char> objectiveTypes = new List<char>() { 'P', 'P', 'P' };
        List<Vector2Int> entrances;
        #endregion

        #region Constructor
        public DNA(ref List<Vector2Int> entrances)
        {
                RandomizeGenes(ref entrances);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Initialize each gene with a random value
        /// </summary>
        /// <param name="entrances">Empty entrances within the level</param>
        public void RandomizeGenes(ref List<Vector2Int> entrances)
        {
                this.entrances = entrances;
                type = enemyTypes[Random.Range(0, enemyTypes.Count)];
                entrance = entrances[Random.Range(0, entrances.Count)];
                hostility = Random.Range(0.01f, 1f);
                fear = Random.Range(0.01f, 1f);
                bravery = 1 - fear;
                stamina = Random.Range(0.01f, 0.5f);
                weakness = 1 - stamina;
                tolerance = Random.Range(0.1f, 0.9f);
        }

        public void Combine(DNA parentA, DNA parentB)
        {
                for (int i = 0; i < 3; i++)
                {
                        objectives[i] = parentA.objectives[i];

                }

                type = parentA.type;
                entrance = parentA.entrance;
                hostility = parentA.hostility;
                fear = parentB.fear;
                bravery = parentB.bravery;
                stamina = parentB.stamina;
                weakness = parentB.weakness;
                tolerance = parentB.tolerance;
        }

        public void Mutate()
        {

                entrance = entrances[Random.Range(0, entrances.Count)];

                switch (Random.Range(0, 4))
                {
                        case 0:
                                {
                                        hostility = Random.Range(0.1f, 0.5f);
                                        break;
                                }
                        case 1:
                                {
                                        fear = Random.Range(0.01f, 0.5f);
                                        bravery = 1 - fear;
                                        break;
                                }
                        case 2:
                                {
                                        stamina = Random.Range(0.01f, 0.5f);
                                        weakness = 1 - stamina;
                                        break;
                                }
                        case 3:
                                {
                                        tolerance = Random.Range(0.1f, 0.9f);
                                        break;
                                }
                }
        }
        #endregion
}