using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIncreaser : MonoBehaviour
{
    public int increase_amount;

    public void Increase()
    {
        int current_m = (int)Settings.Instance.GetSetting("town_stored_mon");
        Settings.Instance.SetSetting("town_stored_mon", current_m + increase_amount);
    }
}
