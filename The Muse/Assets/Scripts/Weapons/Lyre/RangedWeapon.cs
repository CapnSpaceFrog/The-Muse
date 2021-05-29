using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    private GameObject projectilePrefab;
    private Projectile projectile;
    public Transform projectileStartPOS;
    private Player player;

    protected override void Start()
    {
        base.Start();

        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectiles/LyreProjectile");
        player = GameObject.Find("Player").GetComponent<Player>();
        projectile = projectilePrefab.GetComponent<Projectile>();
    }
    public override void InsantiateProjectile()
    {
        projectile.direction = player.FacingDirection;
        Instantiate(projectilePrefab, projectileStartPOS.position, projectileStartPOS.rotation);
    }
}
