using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{

    public Projectiles CurrentProjectile;
    public Gun CurrentGun;
    public Melee CurrentMelee;


    public Transform GunMuzzel;
    public Transform ObjParents;

    public GunHandler gunHandler { get; private set; }
    public MeleeHandler meleeHandler { get; private set; }


    public UnityEvent<GameObject, GameObject> OnProjectileShotHit = new UnityEvent<GameObject, GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        gunHandler = new GunHandler(GunMuzzel, ObjParents, transform, OnProjectileShotHit, gameObject);
        meleeHandler = new MeleeHandler(GunMuzzel, ObjParents, transform, gameObject);

        if(CurrentGun != null && CurrentProjectile != null)
            gunHandler.SetGun(CurrentGun, CurrentProjectile);

        if(CurrentMelee != null)
            meleeHandler.SetMeleeWeapon(CurrentMelee);
    }

    public void Update()
    {
        gunHandler.SetShootState();
        meleeHandler.SetMeleeState();
    }

    public void Shoot(Transform target, bool effects = false, bool infiniteAmmo = false)
    {
        gunHandler.Shoot(Utils.LooAt(transform.position, target.position), effects);

        if(infiniteAmmo)
            gunHandler.IncreaseAmmoBy(gunHandler.GetCurrentGun().MaxAmmo + 1);
    }

    public void AttackMelee()
    {
        meleeHandler.Attack();
    }
}
