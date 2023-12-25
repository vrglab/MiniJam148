using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationTokenSet : DefaultTokenFunctionalityHandlers
{
   public static string townBuildCost(object caller)
   {
        TownBuildingUiData uidata = ((GameObject)caller).transform.parent.gameObject.GetComponent<TownBuildingUiData>();
        return $"R: {uidata.R_Cost}, M: {uidata.M_Cost}";
   }
}
