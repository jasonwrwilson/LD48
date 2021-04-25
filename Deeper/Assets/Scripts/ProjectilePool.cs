using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public Projectile[] projectilePrefabs;

    private List<Projectile>[] projectilePools;

    // Start is called before the first frame update
    void Start()
    {
        projectilePools = new List<Projectile>[projectilePrefabs.Length];

        for (int i = 0; i < projectilePools.Length; i++)
        {
            projectilePools[i] = new List<Projectile>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Projectile GetProjectile(int index)
    {
        Projectile projectile;
        if (projectilePools[index].Count == 0)
        {
            Debug.Log("Create a projectile");
            projectile = Instantiate(projectilePrefabs[index], new Vector3(0, 0, 1), Quaternion.identity);
            projectile.SetPool(this, index);
        }
        else
        {
            projectile = projectilePools[index][0];
            projectile.gameObject.SetActive(true);
            projectilePools[index].Remove(projectile);
        }

        return projectile;
    }

    public void ReplaceProjectile(int index, Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectilePools[index].Add(projectile);
    }
}
