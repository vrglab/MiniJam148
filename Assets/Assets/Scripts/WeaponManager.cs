using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{

    public Projectiles CurentProjectile;
    public Gun CurentGun;
    public Transform GunMuzzel;
    public Transform ObjParents;

    public GunHandler gunHandler { get; private set; }

    public UnityEvent<GameObject, GameObject> OnProjectileShotHit = new UnityEvent<GameObject, GameObject>();
    public UnityEvent<GameObject> OnProjectileStart = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnProjectileUpdate = new UnityEvent<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gunHandler = new GunHandler(GunMuzzel, ObjParents, transform, OnProjectileShotHit, gameObject);
        gunHandler.SetGun(CurentGun, CurentProjectile);
    }

    public void Update()
    {
        gunHandler.SetShootState();
        gunHandler.IncreaseAmmoBy(gunHandler.GetCurrentGun().MaxAmmo + 1);
    }

    public void Shoot(Transform target, bool effects = false)
    {
        gunHandler.Shoot(Utils.LooAt(transform.position, target.position), effects);
    }
}
