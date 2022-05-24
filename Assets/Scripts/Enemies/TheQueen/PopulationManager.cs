using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
        [SerializeField] Transform botContainer;

        public GameObject botPrefab;
        public int populationSize = 50;
        public List<GameObject> population = new List<GameObject>();
        int generation = 1;


        // Start is called before the first frame update
        void Start()
        {
                for (int i = 0; i < populationSize; i++)
                {
                        GameObject bot = 
                                Instantiate(
                                        botPrefab,
                                        transform.position,
                                        Quaternion.identity
                                );
                        bot.GetComponent<Brain>().Init();
                        population.Add(bot);
                        bot.transform.SetParent(botContainer);
                }
        }

        float FitnessFunction(GameObject bot)
        {
                Brain brain = bot.GetComponent<Brain>();
                float productivePercentage = brain.productiveTime / brain.timeAlive;
                float unproductivePercentage = 1 - productivePercentage;
                return productivePercentage - unproductivePercentage;
        }

        void Breed(GameObject parent1, GameObject parent2, int listIndex)
        {
                GameObject offspring = population[listIndex];

                Brain brain = offspring.GetComponent<Brain>();

                brain.Init();

                if (Random.Range(0f, 1f) <= 0.35f)
                {
                        brain.dna.Mutate();
                }
                else
                {  
                        brain.dna.Combine
                        (
                                parent1.GetComponent<Brain>().dna,
                                parent2.GetComponent<Brain>().dna
                        );
                }
        }

        void BreedNewPopulation()
        {
                List<GameObject> sortedList =
                        population.OrderBy(bot => FitnessFunction(bot)).ToList();

                int startingPos = sortedList.Count - (int)(sortedList.Count / 2.0f) - 1;

                for (int index = startingPos - 1; index < sortedList.Count - 1; index++)
                {
                       Breed(sortedList[index], sortedList[index  + 1], index);
                        Breed(sortedList[index + 1], sortedList[index], index + 1);
                }

                generation++;
        }
}
