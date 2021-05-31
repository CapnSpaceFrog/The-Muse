using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWeapon : Weapon
{
    private GameObject projectilePrefab;
    private Projectile projectile;
    public Transform projectileStartPOS;
    private Player player;

    private float spellKnockback = 1.25f;
    protected override void Start()
    {
        base.Start();

        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectiles/SpellProjectile");
        player = GameObject.Find("Player").GetComponent<Player>();
        projectile = projectilePrefab.GetComponent<Projectile>();
    }
    public override void InsantiateProjectile()
    {
        projectile.attackDetails[0] = player.playerData.SpellDamage;
        projectile.attackDetails[1] = player.transform.position.x;
        projectile.direction = player.FacingDirection;
        Instantiate(projectilePrefab, projectileStartPOS.position, projectileStartPOS.rotation);
    }
}
