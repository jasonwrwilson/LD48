using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public DudeController dude;

    public int mapWidth;
    public int mapHeight;
    private int[,] tileMap;

    public DeepTile[] tilePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMap();
        InstantiateTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeMap()
    {
        tileMap = new int[mapWidth,mapHeight];
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tileMap[i,j] = 2;
            }
            for (int j = 3; j < mapHeight; j++)
            {
                if (i > mapHeight / 2 - 3 && i < mapHeight / 2 + 3)
                {
                    tileMap[i,j] = 1;
                }
                else
                {
                    tileMap[i,j] = 0;
                }
            }
        }
    }

    private void InstantiateTiles()
    {
        for(int i = 0; i < mapWidth; i++)
        {
            for(int j = 0; j < mapHeight; j++)
            {
                DeepTile newTile = Instantiate(tilePrefabs[tileMap[i,j]], new Vector3(i - mapWidth / 2, -j + 3, 0), Quaternion.identity);
            }
        }
    }
}
