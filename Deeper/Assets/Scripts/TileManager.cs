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

    private int[,] airChamber = {
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 0, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                                    { 0, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                                    { 0, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                                    { 0, 1, 1, 0, 0, 0, 0, 1, 1, 0 },
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
                                    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                                };



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
        for(int i = 0; i < mapWidth; i++)
        {
            if (chamberMap[i, 0] == MapChambers.OverWorldOpening)
            {
                chamberMap[i, 1] = MapChambers.PathUpDown;
            }
            else
            {
                chamberMap[i, 1] = MapChambers.ChamberSolid;
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

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                switch (chamberMap[i,j])
                {
                    case MapChambers.OverWorldLeft:
                    {
                        BuildOverworldLeft(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.OverWorldMiddle:
                    {
                        BuildOverworldMiddle(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.OverWorldRight:
                    {
                        BuildOverworldRight(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.OverWorldOpening:
                    {
                        BuildOverworldOpening(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.PathCross:
                    {
                        BuildCrossPath(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.PathUpDown:
                    {
                        BuildUpDownPath(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.PathLeftDown:
                    {
                        BuildLeftDownPath(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.PathLeftRight:
                    {
                        BuildLeftRightPath(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.PathLeftUp:
                    {
                        BuildLeftUpPath(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.PathRightDown:
                    {
                        BuildRightDownPath(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.PathRightUp:
                    {
                        BuildRightUpPath(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.ChamberAir:
                    {
                        BuildAirChamber(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.ChamberBent:
                    {
                        BuildBentChamber(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.ChamberSolid:
                    {
                        BuildSolidChamber(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.ChamberStraight:
                    {
                        BuildStraightChamber(i * 10, j * 10);
                        break;
                    }
                    case MapChambers.ChamberWater:
                    {
                        BuildWaterChamber(i * 10, j * 10);
                        break;
                    }
                }
            }
        }
    }

    private void InstantiateTiles()
    {
        for(int i = 0; i < mapWidth * 10; i++)
        {
            for(int j = 0; j < mapHeight * 10; j++)
            {
                DeepTile newTile = Instantiate(tilePrefabs[tileMap[i,j]], new Vector3(i - mapWidth * 5, -j, 1), Quaternion.identity);
            }
        }
    }

    private void BuildAirChamber(int x, int y)
    {
        Debug.Log("AirChamber");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = airChamber[j, i];
            }
        }
    }

    private void BuildStraightChamber(int x, int y)
    {
        Debug.Log("StraightChamber");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = straightChamber[j, i];
            }
        }
    }

    private void BuildWaterChamber(int x, int y)
    {
        Debug.Log("WaterChamber");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = waterChamber[j, i];
            }
        }
    }

    private void BuildBentChamber(int x, int y)
    {
        Debug.Log("BentChamber");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = bentChamber[j, i];
            }
        }
    }

    private void BuildSolidChamber(int x, int y)
    {
        Debug.Log("SolidChamber");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = solidChamber[j, i];
            }
        }
    }

    private void BuildUpDownPath(int x, int y)
    {
        Debug.Log("UpDownPath");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];
            }
        }
    }

    private void BuildLeftRightPath(int x, int y)
    {
        Debug.Log("LeftRightPath");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];
            }
        }
    }

    private void BuildLeftDownPath(int x, int y)
    {
        Debug.Log("LeftDownPath");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];
            }
        }
    }

    private void BuildLeftUpPath(int x, int y)
    {
        Debug.Log("LeftUpPath");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];
            }
        }
    }

    private void BuildRightUpPath(int x, int y)
    {
        Debug.Log("RightUpPath");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];
            }
        }
    }

    private void BuildRightDownPath(int x, int y)
    {
        Debug.Log("RightDownPath");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];
            }
        }
    }

    private void BuildCrossPath(int x, int y)
    {
        Debug.Log("CrossPath");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = path[j, i];
            }
        }
    }

    private void BuildOverworldLeft(int x, int y)
    {
        Debug.Log("OverLeft");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = overworldLeft[j, i];
            }
        }
    }

    private void BuildOverworldRight(int x, int y)
    {
        Debug.Log("OverRight");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = overworldRight[j, i];
            }
        }
    }

    private void BuildOverworldMiddle(int x, int y)
    {
        Debug.Log("OverMiddle");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = overworldMiddle[j, i];
            }
        }
    }

    private void BuildOverworldOpening(int x, int y)
    {
        Debug.Log("OverOpening");
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tileMap[x + i, y + j] = overworldOpening[j, i];
            }
        }
    }
}
