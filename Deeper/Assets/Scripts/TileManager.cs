using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public DudeController dude;

    public int mapWidth;
    public int mapHeight;

    private enum MapChambers
    {
        OverWorldMiddle,
        OverWorldLeft,
        OverWorldRight,
        OverWorldOpening,
        PathUpDown,
        PathLeftRight,
        PathLeftDown,
        PathLeftUp,
        PathRightDown,
        PathRightUp,
        PathCross,
        ChamberAir,
        ChamberStraight,
        ChamberWater,
        ChamberBent,
        ChamberSolid
    }

    private MapChambers[,] chamberMap;
    private int[,] tileMap;
    private DeepTile[,] tileMapTiles;
    private int[,] enemyMap;
    private DeepCreature[,] enemyMapCreatures;
    private List<TreasureChest> treasureChests;

    private int[,] airChamber = {
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                                    { 0, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                                    { 0, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                                    { 0, 3, 3, 0, 0, 0, 0, 3, 3, 0 },
                                    { 0, 1, 1, 0, 0, 0, 0, 1, 1, 0 },
                                    { 1, 1, 1, 0, 0, 0, 0, 1, 1, 1 },
                                    { 1, 1, 1, 0, 0, 0, 0, 1, 1, 1 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] straightChamber = {
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] waterChamber = {
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 },
                                    { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 },
                                    { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 },
                                    { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 },
                                    { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] bentChamber = {
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                                    { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                                    { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                                    { 0, 1, 1, 0, 0, 0, 0, 1, 1, 0 },
                                    { 0, 1, 1, 0, 0, 0, 0, 1, 1, 0 },
                                    { 1, 1, 1, 0, 0, 0, 0, 1, 1, 1 },
                                    { 1, 1, 1, 0, 0, 0, 0, 1, 1, 1 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] solidChamber = {
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] upDownPath = {
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 }
                                };

    private int[,] crossPath = {
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 }
                                };

    private int[,] leftRightPath = {
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] leftDownPath = {
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 }
                                };

    private int[,] righDownPath = {
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 }
                                };

    private int[,] leftUpPath = {
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] righUpPath = {
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                                    { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] path = {
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                                };

    private int[,] overworldLeft = {
                                    { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2 },
                                    { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2 },
                                    { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2 },
                                    { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2 },
                                    { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2 },
                                    { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2 },
                                    { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] overworldRight = {
                                    { 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                    { 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                    { 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                    { 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                    { 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                    { 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                    { 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] overworldMiddle = {
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                                };

    private int[,] overworldOpening = {
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                                };

    private TilePool tilePool;
    private CreaturePool creaturePool;

    public DeepCreature[] creaturePrefabs;
    public TreasureChest[] treasureChestPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        tilePool = GetComponent<TilePool>();
        creaturePool = GetComponent<CreaturePool>();

        treasureChests = new List<TreasureChest>();

        InitializeMap();
    }

    // Update is called once per frame
    void Update()
    {
        int dudeX = (int)dude.transform.position.x + mapWidth * 5;
        int dudeY = -(int)dude.transform.position.y;
        UpdateTiles(dudeX, dudeY);
    }

    private void InitializeMap()
    {
        chamberMap = new MapChambers[mapWidth, mapHeight];

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                chamberMap[i, j] = MapChambers.ChamberSolid;
            }
        }

        //overworld
        chamberMap[0, 0] = MapChambers.OverWorldLeft;
        chamberMap[mapWidth - 1, 0] = MapChambers.OverWorldRight;
        for (int i = 1; i < mapWidth - 1; i++)
        {
            chamberMap[i, 0] = MapChambers.OverWorldMiddle;
        }
        chamberMap[mapWidth / 2, 0] = MapChambers.OverWorldOpening;

        //first row
        bool firstRowBranchLeft = Random.Range(0, 2) == 1;
        int firstRowBranchLength = 3;
        int firstRowChambersToCreate = firstRowBranchLength;
        bool firstRowAirChamberUsed = false;
        int firstRowConnectionIndex = mapWidth / 2;
        for (int i = 0; i < mapWidth; i++)
        {

            if (chamberMap[i, 0] == MapChambers.OverWorldOpening)
            {
                chamberMap[i, 1] = MapChambers.PathUpDown;
            }
            else
            {
                if ((firstRowBranchLeft && i > Mathf.Max(0, firstRowConnectionIndex - firstRowBranchLength - 1) && i < firstRowConnectionIndex)
                    || (!firstRowBranchLeft && i < Mathf.Min(mapWidth - 1, firstRowConnectionIndex + firstRowBranchLength + 1) && i > firstRowConnectionIndex))
                {
                    float randChamberNum = Random.Range(0, 4);
                    if (firstRowAirChamberUsed && randChamberNum == 0)
                    {
                        randChamberNum = Random.Range(1, 4);
                    }

                    firstRowChambersToCreate--;

                    if (firstRowChambersToCreate == 0 && !firstRowAirChamberUsed)
                    {
                        randChamberNum = 0;
                    }

                    if (randChamberNum == 0)
                    {
                        firstRowAirChamberUsed = true;
                        chamberMap[i, 1] = MapChambers.ChamberAir;
                    }
                    else if (randChamberNum == 1)
                    {
                        chamberMap[i, 1] = MapChambers.ChamberBent;
                    }
                    else if (randChamberNum == 2)
                    {
                        chamberMap[i, 1] = MapChambers.ChamberStraight;
                    }
                    else if (randChamberNum == 3)
                    {
                        chamberMap[i, 1] = MapChambers.ChamberWater;
                    }
                }
                else
                {
                    chamberMap[i, 1] = MapChambers.ChamberSolid;
                }
            }
        }
        
        //subsequent rows
        for(int j = 2; j < mapHeight - 1; j++)
        {
            //previous row has up/down
            int connectionIndex = -1;
            bool upDownConnection = false;
            for (int i = 0; i < mapWidth; i++)
            {
                if (chamberMap[i, j - 1] == MapChambers.PathUpDown)
                {
                    upDownConnection = true;
                    connectionIndex = i;
                }
                else if (chamberMap[i, j - 1] == MapChambers.PathCross ||
                    chamberMap[i, j - 1] == MapChambers.PathLeftDown ||
                    chamberMap[i, j - 1] == MapChambers.PathRightDown )
                {
                    connectionIndex = i;
                }
            }

            if (upDownConnection)
            {
                bool canBendLeft = false;
                bool canBendRight = false;
                if (connectionIndex > 3)
                {
                    canBendLeft = true;
                }
                if (connectionIndex < mapWidth - 3)
                {
                    canBendRight = true;
                }

                bool bendLeft = false;
                if (canBendLeft && canBendRight)
                {
                    float r = Random.Range(0, 2);
                    bendLeft = r == 1;
                }
                else if (canBendLeft)
                {
                    bendLeft = true;
                }

                for (int i = 0; i < mapWidth; i++)
                {
                    if (i == connectionIndex)
                    {
                        if (bendLeft)
                        {
                            chamberMap[i, j] = MapChambers.PathLeftUp;
                        }
                        else
                        {
                            chamberMap[i, j] = MapChambers.PathRightUp;
                        }
                    }
                    else if (i == connectionIndex - 1 && bendLeft)
                    {
                        chamberMap[i, j] = MapChambers.PathRightDown;
                    }
                    else if (i == connectionIndex + 1 && !bendLeft)
                    {
                        chamberMap[i, j] = MapChambers.PathLeftDown;
                    }
                    else
                    {
                        chamberMap[i, j] = MapChambers.ChamberSolid;
                    }
                }
            }
            else
            {
                bool branchLeft = false;
                bool airChamberUsed = false;
                int branchLength = Mathf.Max(3, j / 4);
                int chambersToCreate = branchLength;
                if (connectionIndex > mapWidth / 2 )
                {
                    branchLeft = true;
                }
                else
                {
                    branchLeft = false;
                }

                for (int i = 0; i < mapWidth; i++)
                {
                    if (i == connectionIndex)
                    {
                        chamberMap[i, j] = MapChambers.PathUpDown;
                    }
                    else
                    {
                        if ((branchLeft && i > Mathf.Max(0, connectionIndex - branchLength - 1) && i < connectionIndex)
                            || (!branchLeft && i < Mathf.Min(mapWidth - 1, connectionIndex + branchLength + 1) && i > connectionIndex))
                        {
                            float randChamberNum = Random.Range(0, 4);
                            if (airChamberUsed && randChamberNum == 0)
                            {
                                randChamberNum = Random.Range(1, 4);
                            }

                            chambersToCreate--;

                            if (chambersToCreate == 0 && !airChamberUsed)
                            {
                                randChamberNum = 0;
                            }

                            if (randChamberNum == 0)
                            {
                                airChamberUsed = true;
                                chamberMap[i, j] = MapChambers.ChamberAir;
                            }
                            else if (randChamberNum == 1)
                            {
                                chamberMap[i, j] = MapChambers.ChamberBent;
                            }
                            else if (randChamberNum == 2)
                            {
                                chamberMap[i, j] = MapChambers.ChamberStraight;
                            }
                            else if (randChamberNum == 3)
                            {
                                chamberMap[i, j] = MapChambers.ChamberWater;
                            }
                        }
                        else
                        {
                            chamberMap[i, j] = MapChambers.ChamberSolid;
                        }
                    }
                }
            }
        }

        int tileMapWidth = mapWidth * 10;
        int tileMapHeight = mapHeight * 10;

        tileMap = new int[tileMapWidth, tileMapHeight];
        enemyMap = new int[tileMapWidth, tileMapHeight];

        for (int i = 0; i < tileMapWidth; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tileMap[i, j] = 2;
            }
            for (int j = 3; j < tileMapHeight; j++)
            {
                if (i > tileMapWidth / 2 - 3 && i < tileMapWidth / 2 + 3)
                {
                    tileMap[i, j] = 1;
                }
                else
                {
                    tileMap[i, j] = 0;
                }
            }
        }

        for (int j = 0; j < mapHeight; j++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                bool placedChest = false;
                switch (chamberMap[i,j])
                {
                    case MapChambers.OverWorldLeft:
                    {
                        BuildOverworldLeft(i * 10, j * 10, false, 0);
                        break;
                    }
                    case MapChambers.OverWorldMiddle:
                    {
                        BuildOverworldMiddle(i * 10, j * 10, false, 0);
                        break;
                    }
                    case MapChambers.OverWorldRight:
                    {
                        BuildOverworldRight(i * 10, j * 10, false, 0);
                        break;
                    }
                    case MapChambers.OverWorldOpening:
                    {
                        BuildOverworldOpening(i * 10, j * 10, false, 0);
                        break;
                    }
                    case MapChambers.PathCross:
                    {
                        int enemies = Random.Range(1, 5);
                        BuildCrossPath(i * 10, j * 10, false, enemies);
                        break;
                    }
                    case MapChambers.PathUpDown:
                    {
                        int enemies = Random.Range(1, 5);
                        BuildUpDownPath(i * 10, j * 10, false, enemies);
                        break;
                    }
                    case MapChambers.PathLeftDown:
                    {
                        int enemies = Random.Range(1, 5);
                        BuildLeftDownPath(i * 10, j * 10, false, enemies);
                        break;
                    }
                    case MapChambers.PathLeftRight:
                    {
                        int enemies = Random.Range(1, 5);
                        BuildLeftRightPath(i * 10, j * 10, false, enemies);
                        break;
                    }
                    case MapChambers.PathLeftUp:
                    {
                        int enemies = Random.Range(1, 5);
                        BuildLeftUpPath(i * 10, j * 10, false, enemies);
                        break;
                    }
                    case MapChambers.PathRightDown:
                    {
                        int enemies = Random.Range(1, 5);
                        BuildRightDownPath(i * 10, j * 10, false, enemies);
                        break;
                    }
                    case MapChambers.PathRightUp:
                    {
                        int enemies = Random.Range(1, 5);
                        BuildRightUpPath(i * 10, j * 10, false, enemies);
                        break;
                    }
                    case MapChambers.ChamberAir:
                    {
                        bool chest = false;
                        if(!placedChest)
                        {
                            chest = true;
                            placedChest = chest;
                        }
                        else
                        {
                            chest = Random.Range(0, 4) == 0;
                        }

                        int enemies = Random.Range(1, 4);
                        BuildAirChamber(i * 10, j * 10, chest, enemies);
                        break;
                    }
                    case MapChambers.ChamberBent:
                    {
                        bool chest = false;
                        if (!placedChest)
                        {
                            chest = Random.Range(0, 4) == 0;
                            placedChest = chest;
                        }
                        else
                        {
                            chest = Random.Range(0, 10) == 0;
                        }

                        int enemies = Random.Range(1, 4);
                        BuildBentChamber(i * 10, j * 10, chest, enemies);
                        break;
                    }
                    case MapChambers.ChamberSolid:
                    {
                        BuildSolidChamber(i * 10, j * 10, false, 0);
                        break;
                    }
                    case MapChambers.ChamberStraight:
                    {
                        bool chest = false;
                        if (!placedChest)
                        {
                            chest = Random.Range(0, 4) == 0;
                            placedChest = chest;
                        }
                        else
                        {
                            chest = Random.Range(0, 10) == 0;
                        }

                        int enemies = Random.Range(1, 4);
                        BuildStraightChamber(i * 10, j * 10, chest, enemies);
                        break;
                    }
                    case MapChambers.ChamberWater:
                    {
                        bool chest = false;
                        if (!placedChest)
                        {
                            chest = true;
                            placedChest = chest;
                        }
                        else
                        {
                            chest = Random.Range(0, 4) == 0;
                        }

                        int enemies = Random.Range(1, 4);
                        BuildWaterChamber(i * 10, j * 10, chest, enemies);
                        break;
                    }
                }
            }
        }

        tileMapTiles = new DeepTile[tileMapWidth, tileMapHeight];
        enemyMapCreatures = new DeepCreature[tileMapWidth, tileMapHeight];
    }

    private void UpdateTiles(int x, int y)
    {
        for (int i = Mathf.Max(0, x - 30); i < Mathf.Min(mapWidth * 10, x + 30); i++)
        {
            for (int j = Mathf.Max(0, y - 30); j < Mathf.Min(mapHeight * 10, y + 30); j++)
            {
                if (i < x - 20 || i > x + 20 || j < y - 20 || j > y + 20)
                {
                    if (tileMapTiles[i,j] != null)
                    {
                        tilePool.ReplaceTile(tileMap[i, j], tileMapTiles[i, j]);
                        tileMapTiles[i, j] = null;
                    }
                }
                else
                {
                    if (tileMapTiles[i, j] == null)
                    {
                        tileMapTiles[i, j] = tilePool.GetTile(tileMap[i, j]);
                        tileMapTiles[i, j].transform.position = new Vector3(i - mapWidth * 5, -j, 1);
                    }
                }

                if (i < x - 10 || i > x + 10 || j < y - 10 || j > y + 10)
                {
                    if (enemyMapCreatures[i, j] != null)
                    {
                        creaturePool.ReplaceCreature(0, enemyMapCreatures[i, j]);
                        enemyMapCreatures[i, j] = null;
                    }
                }
                else
                {
                    if (enemyMap[i, j] >= 0 && enemyMapCreatures[i, j] == null)
                    {
                        enemyMapCreatures[i, j] = creaturePool.GetCreature(enemyMap[i, j]);
                        enemyMapCreatures[i, j].transform.position = new Vector3(i - mapWidth * 5, -j, 1);
                        enemyMapCreatures[i, j].MultiplyHealth(Mathf.Max(1, (int)((j - 3) / 10)));
                    }
                }
            }
        }
    }

    private void ClearMap(int x, int y)
    {
        for (int i = Mathf.Max(0, x - 30); i < Mathf.Min(mapWidth * 10, x + 30); i++)
        {
            for (int j = Mathf.Max(0, y - 30); j < Mathf.Min(mapHeight * 10, y + 30); j++)
            {
                if (tileMapTiles[i, j] != null)
                {
                    tilePool.ReplaceTile(tileMap[i, j], tileMapTiles[i, j]);
                    tileMapTiles[i, j] = null;
                }
                if (enemyMapCreatures[i, j] != null)
                {
                    creaturePool.ReplaceCreature(enemyMap[i,j], enemyMapCreatures[i, j]);
                    enemyMapCreatures[i, j] = null;
                }
            }
        }

        while(treasureChests.Count > 0)
        {
            TreasureChest chest = treasureChests[0];
            treasureChests.Remove(chest);
            chest.gameObject.SetActive(false);
            Destroy(chest.gameObject);
        }
    }

    private void BuildAirChamber(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = airChamber[j, i];

                if (i == chestX && placeChest && j > 0 && airChamber[j, i] == 0 && airChamber[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildStraightChamber(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = straightChamber[j, i];

                if (i == chestX && placeChest && j > 0 && straightChamber[j, i] == 0 && straightChamber[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }           
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildWaterChamber(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = waterChamber[j, i];

                if (i == chestX && placeChest && j > 0 && waterChamber[j, i] == 0 && waterChamber[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildBentChamber(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = bentChamber[j, i];

                if (i == chestX && placeChest && j > 0 && bentChamber[j, i] == 0 && bentChamber[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildSolidChamber(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = solidChamber[j, i];

                if (i == chestX && placeChest && j > 0 && solidChamber[j, i] == 0 && solidChamber[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildUpDownPath(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];

                if (i == chestX && placeChest && j > 0 && path[j, i] == 0 && path[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildLeftRightPath(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + j, y + j] = path[j, i];

                if (i == chestX && placeChest && i > 0 && path[j, i] == 0 && path[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildLeftDownPath(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];

                if (i == chestX && placeChest && j > 0 && path[j, i] == 0 && path[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildLeftUpPath(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];

                if (i == chestX && placeChest && j > 0 && path[j, i] == 0 && path[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildRightUpPath(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];

                if (i == chestX && placeChest && j > -1 && path[j, i] == 0 && path[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildRightDownPath(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];

                if (i == chestX && placeChest && j > -1 && path[j, i] == 0 && path[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildCrossPath(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];

                if (i == chestX && placeChest && j > 0 && path[j, i] == 0 && path[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildOverworldLeft(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = overworldLeft[j, i];

                if (i == chestX && placeChest && j > 0 && overworldLeft[j, i] == 0 && overworldLeft[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildOverworldRight(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = overworldRight[j, i];

                if (i == chestX && placeChest && j > 0 && overworldRight[j, i] == 0 && overworldRight[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildOverworldMiddle(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = overworldMiddle[j, i];

                if (i == chestX && placeChest && j > 0 && overworldMiddle[j, i] == 0 && overworldMiddle[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void BuildOverworldOpening(int x, int y, bool chest, int enemies)
    {
        bool placeChest = chest;
        int chestX = Random.Range(2, 8);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = overworldOpening[j, i];

                if (i == chestX && placeChest && j > 0 && overworldOpening[j, i] == 0 && overworldOpening[j - 1, i] != 0)
                {
                    CreateChest(x + i, y + j - 1);
                    placeChest = false;
                }

                enemyMap[x + i, y + j] = -1;
            }
        }

        for (int i = 0; i < enemies; i++)
        {
            bool enemyPlaced = false;

            while (!enemyPlaced)
            {
                int enemyX = Random.Range(2, 8);
                int enemyY = Random.Range(2, 8);
                if (enemyMap[x + enemyX, y + enemyY] == -1 && tileMap[x + enemyX, y + enemyY] == 1)
                {
                    enemyMap[x + enemyX, y + enemyY] = Random.Range(0, 2);
                    enemyPlaced = true;
                }
            }
        }
    }

    private void CreateChest(int x, int y)
    {
        TreasureChest newChest = Instantiate(treasureChestPrefabs[0], new Vector3(x - mapWidth * 5, -y, 1), Quaternion.identity);
        newChest.SetGold(250 + 5 * y);

        treasureChests.Add(newChest);
    }

    public void ResetMap()
    {
        int dudeX = (int)dude.transform.position.x + mapWidth * 5;
        int dudeY = -(int)dude.transform.position.y;
        
        //clear old map
        ClearMap(dudeX, dudeY);

        //initialize a new map
        InitializeMap();
    }
}
