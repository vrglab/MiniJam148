using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureAmmoBarHandler : MonoBehaviour
{
    private WeaponManager weaponMannager;
    private Slider bar;

    void Start()
    {
        weaponMannager = GetComponentInParent<WeaponManager>();
        bar = GetComponentInChildren<Slider>();

        bar.maxValue = weaponMannager.gunHandler.GetMaxAmmo();
        bar.value = weaponMannager.gunHandler.GetCurrentAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        bar.value = weaponMannager.gunHandler.GetCurrentAmmo();
    }
}
