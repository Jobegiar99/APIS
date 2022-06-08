using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformation 
{
        #region Parameters

        string difficulty;
        public byte beaconAmount;
        string seed;

        List<List<byte>> level;

        public List<Vector2Int> entrances = new List<Vector2Int>();

        public List<List<byte>> terrainMatrix;
        #endregion
        
        #region Constructor

        public LevelInformation(string difficulty, string seed, GameObject player)
        {
                int size = 0;
                switch (difficulty)
                {
                        case "Easy":
                                size = 30;
                                break;

                        case "Normal":
                                size = 50;
                                break;

                        case "Hard":
                                size = 80;
                                break;
                }

                if (seed == "")
                        Random.InitState(Random.Range(int.MinValue, int.MaxValue));
                else
                        Random.InitState (seed.GetHashCode());

                Vector2Int wanderer = new Vector2Int( 1 , Random.Range(1, size - 2));
                Vector2Int seeker = new Vector2Int(size - 2, Random.Range(1, size - 2));

                float levelSize = Random.Range(0.45f, 0.8f);

                TerrainCreator terrain = new TerrainCreator(wanderer, seeker, size, levelSize);
                TerrainBuilder builder = new TerrainBuilder(ref terrain.terrain, ref size, player);
                entrances = builder.entrances;
                terrainMatrix = terrain.terrain; 
        }

        #endregion
}
