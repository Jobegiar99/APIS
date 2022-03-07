using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{

        public int DNALength = 5;
        public byte DNAOptions;
        public List<string> genes = new List<string>();
        private int geneIndex;
        public float furtherstDistance;
        public int repeatedCells = 0;
        public DNA(int length, byte options)
        {
                DNALength = length;
                DNAOptions = options;
                geneIndex = 0;
                RandomizeGenes();
        }

        public void RandomizeGenes()
        {
                genes.Clear();
                for (int i = 0; i < DNALength; i++)
                {
                        genes.Add("");
                }
        }

        public void Combine(DNA parentA, DNA parentB)
        {
                if (genes.Count < DNALength)
                        RandomizeGenes();
                for (int i = 0; i < genes.Count; i++)
                {
                        if (i < DNALength / 2 && i < parentA.genes.Count)
                        {
                                genes[i] = parentA.genes[i];
                        }
                        else if (i < parentB.genes.Count)
                        {
                                genes[i] = parentB.genes[i];
                        }
                }
        }

        public void Mutate()
        {
                for (int i = 0; i < Random.Range(1, genes.Count + 1); i++)
                {
                        genes[Random.Range(0, genes.Count)] = "";

                }

                while (genes.Count < DNALength)
                {
                        genes.Add("");
                }
        }

        public string GetGene()
        {

                if (geneIndex < genes.Count)
                {
                        return genes[geneIndex++];
                }
                return -1;
        }
}