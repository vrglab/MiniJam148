using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AICreature
{

    public bool AlreadyTargeted { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TownBuilding>())
        {
            collision.gameObject.GetComponent<TownBuilding>().TakeDamage(20);
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        base.Update();

        if(InputManager.Instance.GetKeyDown("akt_place"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform the raycast
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
               if (hit.collider.gameObject == gameObject)
               {
                    TakeDamage(5);
               }
            }
        }
    }
}
