using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public DeepTile[] tilePrefabs;

    private List<DeepTile>[] tilePools;
    
    // Start is called before the first frame update
    void Start()
    {
        tilePools = new List<DeepTile>[tilePrefabs.Length];

        for (int i = 0; i < tilePools.Length; i++)
        {
            tilePools[i] = new List<DeepTile>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DeepTile GetTile(int index)
    {
        DeepTile tile;
        if (tilePools[index].Count == 0)
        {
            tile = Instantiate(tilePrefabs[index], new Vector3(0, 0, 1), Quaternion.identity);
        }
        else
        {
            tile = tilePools[index][0];
            tile.gameObject.SetActive(true);
            tilePools[index].Remove(tile);
        }

        return tile;
    }

    public void ReplaceTile(int index, DeepTile tile)
    {
        tile.gameObject.SetActive(false);
        tilePools[index].Add(tile);
    }
}
