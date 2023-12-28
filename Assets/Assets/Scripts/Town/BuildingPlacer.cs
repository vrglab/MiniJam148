using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject objToPlace;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = objToPlace.GetComponent<SpriteRenderer>().sprite;
    }

    public void Update()
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1);
        if(InputManager.Instance.GetKeyDown("akt_interact"))
        {
            Instantiate(objToPlace, transform.position, Quaternion.identity, null);
            Destroy(gameObject);
        }
    }
}
