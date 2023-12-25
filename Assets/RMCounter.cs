using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RMCounter : MonoBehaviour
{
    public TextMeshProUGUI rcounter, mcounter;


    public void Update()
    {
        int current_max_res_storage = (int)Settings.Instance.GetSetting("town_max_res_storage_amnt"), current_stored_res = (int)Settings.Instance.GetSetting("town_stored_res"), current_m = (int)Settings.Instance.GetSetting("town_stored_mon");

        rcounter.text = $"R: {current_stored_res}/{current_max_res_storage}";
        mcounter.text = $"M: {current_m}";
    }
}
