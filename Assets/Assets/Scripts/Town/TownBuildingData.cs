using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Town Building")]
public class TownBuildingData : ScriptableObject
{
    public string LocalizationID;
    public int R_Cost, M_Cost;
    public GameObject BuildingObj;
}
