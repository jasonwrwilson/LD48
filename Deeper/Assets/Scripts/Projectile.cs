using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private int attack;
    private ProjectilePool projectilePool;
    private int poolIndex;

    private Vector3 direction;

    public float projectileSpeed;

    public SpriteRenderer spriteRenderer;

    private float timeOut = 30.0f;
    private float timeOutCountdown;

    private DudeController dude;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOutCountdown > 0)
        {
            timeOutCountdown -= Time.deltaTime;
            if (timeOutCountdown <= 0)
            {
                KillProjectile();
            }
        }

        Vector2 movementInput = direction * projectileSpeed;

        rigidBody.velocity = movementInput;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        if (dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (dir.y > 0)
        {
            spriteRenderer.flipY = false;
        }
        else
        {
            spriteRenderer.flipY = true;
        }

        timeOutCountdown = timeOut;
    }

    public void SetPool(ProjectilePool pp, int index)
    {
        projectilePool = pp;
        poolIndex = index;
    }
    
    public void SetAttack(int amount)
    {
        attack = amount;
    }

    public void SetDude(DudeController d)
    {
        dude = d;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DeepCreature creature = collision.gameObject.GetComponent<DeepCreature>();

        if (creature != null && !creature.IsDead())
        {
            creature.TakeDamage(attack);
            if ( creature.IsDead() )
            {
                dude.EarnBones(creature.GetBones());
            }
            KillProjectile();
        }

        DeepTile tile = collision.gameObject.GetComponent<DeepTile>();
        
        if (tile != null && !tile.isAir && !tile.isWater)
        {
            KillProjectile();
        }
    }

    private void KillProjectile()
    {
        projectilePool.ReplaceProjectile(poolIndex, this);
    }
}
