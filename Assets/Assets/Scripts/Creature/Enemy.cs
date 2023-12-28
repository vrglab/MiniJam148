using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AICreature
{

    public bool AlreadyTargeted { get; set; }

    private bool inContact;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TownBuilding>())
        {
            inContact = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TownBuilding>())
        {
            inContact = false;
        }
    }

    public void Update()
    {
        base.Update();
        if (inContact)
        {
            GetComponent<WeaponManager>().AttackMelee();
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public override void OnClicked()
    {
        TakeDamage(5);
    }
}
