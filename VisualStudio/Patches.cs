using Il2Cpp;
using HarmonyLib;
using UnityEngine;
using Il2CppSteamworks;

namespace Leatherworks
{
    internal class Patches
    {
        [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
        internal class LeatherworksInitialization
        {
            private static void Postfix(Panel_Inventory __instance)
            {
                LeatherworksUtils.inventory = __instance;
                LWFunctionalities.InitializeMTB(__instance.m_ItemDescriptionPage);
            }
        }
        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateScrapeFurButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                LWFunctionalities.furItem = gi?.GetComponent<GearItem>();
                if (gi != null && LeatherworksUtils.IsFur(gi.name) == true)
                {
                    LWFunctionalities.SetScrapeFurActive(true);
                }
                else
                {
                    LWFunctionalities.SetScrapeFurActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateLeatherAddButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                LWFunctionalities.recipientBoxItem = gi?.GetComponent<GearItem>();
                if (gi != null && LeatherworksUtils.IsTanFilled(gi.name) == true)
                {
                    LWFunctionalities.SetLeatherAddActive(true);
                }
                else
                {
                    LWFunctionalities.SetLeatherAddActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateTanningAddButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                LWFunctionalities.tanningItem = gi?.GetComponent<GearItem>();
                if (gi != null && LeatherworksUtils.IsTanEmpty(gi.name) == true)
                {
                    LWFunctionalities.SetTanningAddActive(true);
                }
                else
                {
                    LWFunctionalities.SetTanningAddActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateCrushBarkButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                LWFunctionalities.barkItem = gi?.GetComponent<GearItem>();
                if (gi != null && LeatherworksUtils.IsFriedBark(gi.name) == true)
                {
                    LWFunctionalities.SetCrushBarkActive(true);
                }
                else
                {
                    LWFunctionalities.SetCrushBarkActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdatePileBarkButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                LWFunctionalities.pileItem = gi?.GetComponent<GearItem>();
                if (gi != null && LeatherworksUtils.IsFriedBarkPileable(gi.name) == true)
                {
                    LWFunctionalities.SetPileBarkActive(true);
                }
                else
                {
                    LWFunctionalities.SetPileBarkActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdatePlaceBoxButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                LWFunctionalities.placeBoxItem = gi?.GetComponent<GearItem>();
                if (gi != null && gi.name.ToLowerInvariant().Contains("boxtanning"))
                {
                    LWFunctionalities.SetPlaceBoxActive(true);
                }
                else
                {
                    LWFunctionalities.SetPlaceBoxActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(RadialObjectSpawner), "GetNextPrefabToSpawn")]
        internal class AddTreebark
        {
            private static void Postfix(RadialObjectSpawner __instance, ref GameObject __result)
            {
                if (__instance != null && __instance.name.Contains("RadialSpawn_sticks") && LeatherworksUtils.treebark != null)
                {
                    if (Utils.RollChance(Settings.instance.treebarkChance))
                    {
                        __result = LeatherworksUtils.treebark;
                    }
                }
            }
        }

       

    }
}
    