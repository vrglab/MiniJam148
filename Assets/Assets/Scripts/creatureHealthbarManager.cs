using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creatureHealthbarManager : MonoBehaviour
{
    private Creature creature;
    private Slider bar;

    void Start()
    {
        creature= GetComponentInParent<Creature>();
        bar = GetComponentInChildren<Slider>();

        bar.maxValue = creature.MaxHealth;
        bar.value = creature.CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        bar.value = creature.CurrentHealth;
    }
}
