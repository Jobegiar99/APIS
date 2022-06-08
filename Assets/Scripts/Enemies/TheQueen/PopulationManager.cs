using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PopulationManager : MonoBehaviour
{
        [SerializeField] Transform botContainer;
        [SerializeField] Transform projectilePool;
        [SerializeField] GameObject botPrefab;
        public int populationSize = 50;
        public List<GameObject> population = new List<GameObject>();
        int generation = 1;

        public void StartRound()
        {
                for (int i = 0; i < populationSize; i++)
                {
                        GameObject enemy = Instantiate(botPrefab, new Vector3(20,20,0), Quaternion.identity);
                        enemy.GetComponent<NavMeshAgent>().updateRotation = false;
                        population.Add(enemy);
                        enemy.transform.parent = botContainer;
                        
                }
                InitEnemies();
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
                GameObject offspring = Instantiate(botPrefab,transform.position,Quaternion.identity);
                offspring.SetActive(false);
                offspring.transform.parent = botContainer;
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
                return offspring;
        }

        void BreedNewPopulation()
        {
                List<GameObject> sortedList =
                        population.OrderBy(bot => FitnessFunction(bot)).ToList();
                population.Clear();

                for (int index = sortedList.Count / 2; index < sortedList.Count - 1; index++)
                {
                       population.Add(Breed(sortedList[index], sortedList[index  + 1]));
                       population.Add(Breed(sortedList[index + 1], sortedList[index]));
                }
                for(int i =0; i < sortedList.Count; i++)
                {
                        Destroy(sortedList[i].gameObject);
                }

                generation++;
                InitEnemies();
        }

        private void InitEnemies()
        {
                for (int i = 0; i < populationSize; i++)
                {
                        Brain brain = population[i].GetComponent<Brain>();
                        for (int j = 0; j < populationSize; j++)
                        {
                                if (i == j)
                                        continue;
                                brain.fellowMinions.Add(population[j]);
                        }

                        population[i].GetComponent<EnemyController>().projectilePool = projectilePool;
                        brain.Init();
                        population[i].transform.position =
                                new Vector3(
                                        brain.dna.entrance.x,
                                        brain.dna.entrance.y,
                                        0
                               );
                }
        }
}
