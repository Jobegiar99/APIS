using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
        
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
                }
        }

        float FitnessFunction(GameObject bot)
        {
                Brain brain = bot.GetComponent<Brain>();
                float productivePercentage = brain.productiveTime / brain.timeAlive;
                float unproductivePercentage = 1 - productivePercentage;
                return productivePercentage - unproductivePercentage;
        }

        GameObject Breed(GameObject parent1, GameObject parent2)
        {

                GameObject offspring =
                        Instantiate(botPrefab, transform.position, transform.rotation);

                Brain brain = offspring.GetComponent<Brain>();
                brain.Init();

                if (Random.Range(0f, 1f) <= 0.35f)
                {
                        brain.dna.Mutate(ref GameObject.Find("Main Camera").GetComponent<LevelInformation>().entrances);
                }
                else
                {  
                        brain.dna.Combine
                        (
                                parent1.GetComponent<Brain>().dna,
                                parent2.GetComponent<Brain>().dna
                        );
                }
                return offspring;
        }

        void BreedNewPopulation()
        {
                List<GameObject> sortedList =
                        population.OrderBy(bot => FitnessFunction(bot)).ToList();
    
                population.Clear();

                int startingPos = sortedList.Count - (int)(sortedList.Count / 2.0f) - 1;

                for (int i = startingPos - 1; i < sortedList.Count - 1; i++)
                {
                        population.Add(Breed(sortedList[i], sortedList[i + 1]));
                        population.Add(Breed(sortedList[i + 1], sortedList[i]));
                }
                for (int i = 0; i < sortedList.Count; i++)
                {
                        Destroy(sortedList[i].gameObject);
                }
                generation++;
        }
}
