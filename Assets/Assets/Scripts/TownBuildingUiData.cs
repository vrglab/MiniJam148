using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownBuildingUiData : MonoBehaviour
{
    public int R_Cost, M_Cost;
    public GameObject BuildingObj;
    public Button BuyButton;

    private void Awake()
    {
        try
        {
            Settings.Instance.GetSetting("town_stored_mon");
        }
        catch (System.Exception)
        {
            Settings.Instance.SetSetting("town_stored_mon", 0);
        }
    }


    private void Update()
    {
        int current_r = (int)Settings.Instance.GetSetting("town_stored_res"), current_m = (int)Settings.Instance.GetSetting("town_stored_mon");
    
        if(current_r >= R_Cost && current_m >= M_Cost)
        {
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.interactable= false;
        }
    }

    public void Buy()
    {
        int current_r = (int)Settings.Instance.GetSetting("town_stored_res"),
            current_m = (int)Settings.Instance.GetSetting("town_stored_mon");
        Settings.Instance.SetSetting("town_stored_res", current_r - R_Cost);
        Settings.Instance.SetSetting("town_stored_mon", current_m - M_Cost);
        GameObject obj = new GameObject();
        obj.AddComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        obj.AddComponent<BuildingPlacer>().objToPlace = BuildingObj;
    }
}
