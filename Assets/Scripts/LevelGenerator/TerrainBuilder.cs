using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainBuilder
{

        #region Constructor
        /// <summary>
        /// Builds the terrain in the game's world
        /// </summary>
        /// <param name="terrain">Square matrix that contains the information of the level</param>
        /// <param name="terrainSize">Matrix size</param>
        public TerrainBuilder(ref List<List<byte>> terrain, ref  int terrainSize)
        {
                int resizeRate = 3;
                int newSize = terrainSize * resizeRate;
                terrain = ResizeMatrix(ref terrain, resizeRate, terrainSize);
                FillTilesetWithMatrixInfo(ref terrain, ref newSize);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Creates an empty matrix with the new target size
        /// </summary>
        /// <param name="size">The target size of the new matrix</param>
        /// <param name="terrainSize">The amount of rows and columns that the original matrix has</param>
        /// <returns>The empty matrix with the target size</returns>
        private List<List<byte>> CreateMatrixHolder(int size, int terrainSize)
        {
                List<List<byte>> newMatrix = new List<List<byte>>();
                for (int i = 0; i < terrainSize * size; i++)
                {
                        List<byte> tempRow = new List<byte>();
                        for (int j = 0; j < terrainSize * size; j++)
                                tempRow.Add(0);

                        newMatrix.Add(tempRow);
                }
                return newMatrix;
        }

        /// <summary>
        /// Fills the matrix holder with the original level information
        /// </summary>
        /// <param name="newMatrix">The matrix holder created with CreateMatrixHolder function</param>
        /// <param name="terrain">The level read from the files now transformed into a matrix</param>
        /// <param name="size">The target size of the new matrix</param>
        /// <returns></returns>
        private List<List<byte>> FillResizedMatrixHolder(List<List<byte>> newMatrix, ref List<List<byte>> level, int size)
        {

                for (int i = 0; i < level.Count; i++)
                {
                        for (int j = 0; j < level[i].Count; j++)
                        {
                                for (int newRow = i * size; newRow < ((i * size) + size); newRow++)
                                {

                                        for (int newColumn = j * size; newColumn < ((j * size) + size); newColumn++)
                                        {
                                                if (level[i][j] == 1)
                                                        newMatrix[newRow][newColumn] = 1;
                                                else
                                                        newMatrix[newRow][newColumn] = level[i][j];

                                        }
                                }
                        }
                }
                return newMatrix;
        }

        /// <summary>
        /// Takes the matrix's information to the tileset.
        /// </summary>
        /// <param name="terrain">Matrix that contains the information of the level</param>
        /// <param name="newSize">Matrix's size after resizing it</param>
        private void FillTilesetWithMatrixInfo(ref List<List<byte>> terrain, ref int newSize)
        {
                Tilemap freeTilemap = GameObject.Find("freeTilemap").GetComponent<Tilemap>();
                Tilemap blockedTilemap = GameObject.Find("blockedTilemap").GetComponent<Tilemap>();

                TileBase freeTile = (TileBase)Resources.Load("Tiles/freeTile");
                TileBase blockedTile = (TileBase)Resources.Load("Tiles/blockedTile");

                for (int column = newSize - 1; column > -1; column--)
                {
                        for (int row = 0; row < newSize; row++)
                        {
                                if (terrain[row][column] == 0)
                                {
                                        blockedTilemap.SetTile(new Vector3Int(row, column, 0), blockedTile);
                                }
                                else
                                {
                                        freeTilemap.SetTile(new Vector3Int(row, column, 0), freeTile);
                                }

                        }
                }
        }

        /// <summary>
        /// Makes the matrix bigger
        /// </summary>
        /// <param name="terrain">The matrix that contains the terrain information</param>
        /// <param name="targetSize">The target size of the new matrix</param>
        /// <param name="terrainSize">The amount of rows and columns that the original matrix has</param>
        /// <returns>The level after resizing it</returns>
        private List<List<byte>> ResizeMatrix(ref List<List<byte>> terrain, int targetSize, int terrainSize)
        {
                List<List<byte>> newMatrix = CreateMatrixHolder(targetSize, terrainSize);
                return FillResizedMatrixHolder(newMatrix,ref  terrain, targetSize);
        }
        #endregion
}
