using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformation 
{
        #region Parameters

        string difficulty;

        string seed;

        List<List<byte>> level;

        #endregion
        
        #region Constructor

        public LevelInformation(string difficulty, string seed)
        {
                int size = 0;
                switch (difficulty)
                {
                        case "Easy":
                                size = 30;
                                break;

                        case "Normal":
                                size = 40;
                                break;

                        case "Hard":
                                size = 50;
                                break;
                }

                if (seed == "")
                        Random.InitState(Random.Range(-10000, 10000));
                else
                        Random.InitState (seed.GetHashCode());

                Vector2Int wanderer = new Vector2Int(0, Random.Range(0, size));
                Vector2Int seeker = new Vector2Int(size - 1, Random.Range(0, size));

                float levelSize = Random.Range(0.45f, 0.8f);
                TerrainCreator terrain = new TerrainCreator(wanderer, seeker, size, levelSize);
                string s = "";
                for (int i = 0; i < size; i++)
                {
                        string a = "|";
                        for(int j = 0; j < size; j++)
                        {
                                a += (terrain.level[i][j] == 0) ? "x|": "  |";
                        }
                        s += a + "|\n";
                }
                Debug.Log(s);
        }

        #endregion
}
