using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationTokenSet : DefaultTokenFunctionalityHandlers
{
   public static string townBuildCost(object caller)
   {
        TownBuildingUiHandler uidata = ((TownBuildingUiHandler)caller);
        return $"R: {uidata.townBuilding.R_Cost}, M: {uidata.townBuilding.M_Cost}";
   }
}
