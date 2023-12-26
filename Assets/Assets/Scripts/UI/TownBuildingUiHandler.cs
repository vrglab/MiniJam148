using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownBuildingUiHandler : MonoBehaviour
{
    public TownBuildingData townBuilding;

    public Button BuyButton;
    public TextMeshProUGUI TitleText, DescriptionText, CostText;

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

    public void Start()
    {
        JObject localizationData =
            StringProccessor.Instance.GetMultiDataEntry<LocalizationTokenSet>(townBuilding.LocalizationID);

        TitleText.text = localizationData.Property("title").Value.ToString();
        DescriptionText.text = localizationData.Property("desc").Value.ToString();

        CostText.text = StringProccessor.Instance.GetEntry<LocalizationTokenSet>("ui_cost", this);
    }


    private void Update()
    {
        int current_r = (int)Settings.Instance.GetSetting("town_stored_res"), current_m = (int)Settings.Instance.GetSetting("town_stored_mon");
    
        if(current_r >= townBuilding.R_Cost && current_m >= townBuilding.M_Cost)
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
        Settings.Instance.SetSetting("town_stored_res", current_r - townBuilding.R_Cost);
        Settings.Instance.SetSetting("town_stored_mon", current_m - townBuilding.M_Cost);

        //Initiate the placer object
        GameObject obj = new GameObject();
        obj.AddComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        obj.AddComponent<BuildingPlacer>().objToPlace = townBuilding.BuildingObj;
    }
}
