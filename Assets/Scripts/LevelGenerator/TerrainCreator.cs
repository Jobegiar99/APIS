using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreator 
{
        #region Parameters


        /// <summary>
        /// The list of cells visited either by Wanderer or Seeker.
        /// </summary>
        List<Vector2Int> generalVisited = new List<Vector2Int>();

        /// <summary>
        /// The set that will contain the information of the visited positions within the level
        /// </summary>
        public HashSet<Vector2Int> generalVisitedSet = new HashSet<Vector2Int>();

        /// <summary>
        /// The percentage of visited cells compared to the total amount of cells
        /// </summary>
        float levelSize;

        /// <summary>
        /// Starting Seeker's position
        /// </summary>
       public  Vector2Int seeker;

        /// <summary>
        /// The list of current cells visited by Seeker
        /// </summary>
        List<Vector2Int> seekerVisited = new List<Vector2Int>();

        /// <summary>
        /// The amount of rows and columns that the terrain will have
        /// </summary>
        int size;

        /// <summary>
        /// The matrix that will contain the information of the terrain
        /// </summary>
        public List<List<byte>> terrain = new List<List<byte>>();

        /// <summary>
        /// Starting Wanderer's position
        /// </summary>
        public Vector2Int wanderer;

       

        #endregion

        #region Constructor

        public TerrainCreator(Vector2Int wanderer, Vector2Int seeker, int size, float lSize)
        {
                this.wanderer = wanderer;
                this.seeker = seeker;
                this.size = size;
                this.levelSize = lSize;
                GenerateLevel();
        }
        #endregion

        #region Functions

        /// <summary>
        /// Wanderer and Seeker Algorithm
        /// </summary>
        void CreateLevel()
        {
                Vector2Int s = seeker;
                Vector2Int w = wanderer;
                float currentRoomSize = 0;

                while (
                                (!generalVisitedSet.Contains(s)) || 
                                (currentRoomSize < this.levelSize) ||
                                (seekerVisited.Count > 0)
                        )
                {
                        terrain[w.x][w.y] = 1;
                        seekerVisited.Add(s);
                        generalVisited.Add(w);
                        generalVisitedSet.Add(w);
                        s = GetSeekerNextMove(s, w);
                        w = GetWandererNextMove(w);
                        currentRoomSize = (float)((float)generalVisitedSet.Count /  (float)(size * size));
                }
        }

        /// <summary>
        /// Calculates the Manhattan's distance between a point P and wanderer
        /// </summary>
        /// <param name="p">A point's position</param>
        /// <param name="w">Wanderer's position</param>
        /// <returns>The Manhattan's distance between point P and Wanderer</returns>
        float Distance(Vector2Int p, Vector2Int w)
        {
                //Manhattan Distance
                return Mathf.Abs(p.x - w.x) + Mathf.Abs(p.y - w.y);
        }

        /// <summary>
        /// Iterates through the amount of rows and columns to fill the empty level matrix
        /// </summary>
        void FillLevel()
        {
                for (int r = 0; r < size; r++)
                {
                        List<byte> row = new List<byte>();

                        for (int c = 0; c < size; c++)
                        {
                                row.Add(0);
                        }
                        terrain.Add(row);
                }
        }

        /// <summary>
        /// The main function that controls the flow of generating the level using the matrix
        /// </summary>
        void GenerateLevel()
        {

                FillLevel();

                CreateLevel();

        }

        /// <summary>
        /// Gets a list of valid movement options for a given point
        /// </summary>
        /// <param name="point">The point from which the function will try to obtain the valid movement list</param>
        /// <returns>A list of valid points to which the current point can move to</returns>
        List<Vector2Int> GetMoves(Vector2Int point)
        {
                Vector2Int up = new Vector2Int(point.x - 1, point.y);
                Vector2Int down = new Vector2Int(point.x + 1, point.y);
                Vector2Int left = new Vector2Int(point.x, point.y - 1);
                Vector2Int right = new Vector2Int(point.x, point.y + 1);
                List<Vector2Int> moves = new List<Vector2Int>() { up, down, left, right };

                return moves.FindAll(p =>
                                1 <= p.x &&
                                p.x < size - 1 &&
                                1 <= p.y &&
                                p.y < size - 1 &&
                                !generalVisitedSet.Contains(p)
                        );

        }

        /// <summary>
        /// Obtains the next position for Seeker
        /// </summary>
        /// <param name="s">Current seeker's position</param>
        /// <param name="w">Current wanderer's position</param>
        /// <returns></returns>
        Vector2Int GetSeekerNextMove(Vector2Int s, Vector2Int w)
        {
                float distance = Distance(s, w);

                List<Vector2Int> moves = GetMoves(s).FindAll
                        (p =>
                                Distance(p, w) < distance &&
                                !seekerVisited.Contains(p)
                        );

                if (moves.Count > 0)
                        return moves[Random.Range(0, moves.Count)];

                if (generalVisitedSet.Contains(s))
                {
                        foreach (Vector2Int p in seekerVisited)
                        {
                                terrain[p.x][p.y] = 1;
                                generalVisited.Add(p);
                                generalVisitedSet.Add(p);
                        }

                }
                seekerVisited.Clear();
                return new Vector2Int(Random.Range(1, size - 1), Random.Range(1, size - 1 ));
        }

        /// <summary>
        /// Obtains the next position for wanderer
        /// </summary>
        /// <param name="w">Current wanderer's position</param>
        /// <returns>Wanderer's next position</returns>
        Vector2Int GetWandererNextMove(Vector2Int w)
        {
                List<Vector2Int> moves = GetMoves(w);

                if (moves.Count > 0)
                {
                        return moves[Random.Range(0, moves.Count)];
                }

                return generalVisited[Random.Range(0, generalVisited.Count)];
        }

        #endregion
}
