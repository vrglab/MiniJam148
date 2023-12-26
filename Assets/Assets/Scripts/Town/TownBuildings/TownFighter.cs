using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownFighter : TownBuilding
{
    private Enemy target;
    private WeaponManager weaponManager;

    public override string GetID()
    {
        return "vrglab:/fighter";
    }

    public override void OnBuild()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    public override void OnDestroyed()
    {
        
    }

    public override void TownTick(Dictionary<string, TownBuilding> town)
    {
       if(target != null)
       {
            if(!target.IsDead)
            {
                weaponManager.Shoot(target.transform, true, true);
            } 
            else
            {
                target = null;
            }
       }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(target == null)
        {
            if (collision.gameObject.GetComponent<Enemy>() != null)
            {
                if(!collision.gameObject.GetComponent<Enemy>().AlreadyTargeted && !collision.gameObject.GetComponent<Enemy>().IsDead) 
                {
                    target = collision.gameObject.GetComponent<Enemy>();
                    target.AlreadyTargeted = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(target != null)
        {
            if (collision.gameObject.GetComponent<Enemy>() == target)
            {
                target.AlreadyTargeted = false;
                target = null;
            }
        }
    }
}
