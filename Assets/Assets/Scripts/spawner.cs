using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject toSpawn;

    [Range(1f, 100f)]
    public float SpawnInterval = 60f;

    private float time;

    private void Awake()
    {
        time = SpawnInterval;
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            Instantiate(toSpawn, transform.position, Quaternion.identity);
            try
            {
                Settings.Instance.GetSetting("town_hard_mode");
                time = 7;
            }
            catch (System.Exception)
            {
                time = SpawnInterval;
            }
        }
    }

}
