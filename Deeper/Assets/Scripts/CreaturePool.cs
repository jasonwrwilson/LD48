using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePool : MonoBehaviour
{
    public DeepCreature[] creaturePrefabs;

    private List<DeepCreature>[] creaturePools;

    // Start is called before the first frame update
    void Start()
    {
        creaturePools = new List<DeepCreature>[creaturePrefabs.Length];

        for (int i = 0; i < creaturePools.Length; i++)
        {
            creaturePools[i] = new List<DeepCreature>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public DeepCreature GetCreature(int index)
    {
        DeepCreature creature;
        if (creaturePools[index].Count == 0)
        {
            Debug.Log("Create a creature");
            creature = Instantiate(creaturePrefabs[index], new Vector3(0, 0, 1), Quaternion.identity);
        }
        else
        {
            creature = creaturePools[index][0];
            creature.gameObject.SetActive(true);
            creaturePools[index].Remove(creature);
        }

        return creature;
    }

    public void ReplaceCreature(int index, DeepCreature creature)
    {
        creature.gameObject.SetActive(false);
        creaturePools[index].Add(creature);
    }
}
