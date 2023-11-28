using Il2Cpp;
using Il2CppProCore.Decals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static Il2CppMono.Security.X509.X520;

namespace Leatherworks
{
    internal class LWFunctionalities
    {

        internal static string scrapeText;
        private static GameObject scrapeButton;
        internal static GearItem furItem;
        internal static string furName;

        internal static string addLeatherText;
        private static GameObject addLeatherButton;
        internal static GearItem recipientBoxItem;
        internal static string recipientBoxItemName;

        internal static string addTanningText;
        private static GameObject addTanningButton;
        internal static GearItem tanningItem;
        internal static string tanningName;

        internal static string crushBarkText;
        private static GameObject crushBarkButton;
        internal static GearItem barkItem;
        internal static string barkName;

        internal static string pileBarkText;
        private static GameObject pileBarkButton;
        internal static GearItem pileItem;
        internal static string pileName;

        internal static string placeBoxText;
        private static GameObject placeBoxButton;
        internal static GearItem placeBoxItem;
        internal static void InitializeMTB(ItemDescriptionPage itemDescriptionPage)
        {
            scrapeText = Localization.Get("GAMEPLAY_LW_ScrapeLabel");
            addLeatherText = Localization.Get("GAMEPLAY_LW_AddLeatherLabel");
            addTanningText = Localization.Get("GAMEPLAY_LW_AddTanningLabel");
            crushBarkText = Localization.Get("GAMEPLAY_LW_CrushBarkLabel");
            pileBarkText = Localization.Get("GAMEPLAY_LW_PileBarkLabel");
            placeBoxText = Localization.Get("GAMEPLAY_LW_PlaceBoxLabel");

            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;
            scrapeButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            scrapeButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(scrapeButton).text = scrapeText;

            addLeatherButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            addLeatherButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(addLeatherButton).text = addLeatherText;

            addTanningButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            addTanningButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(addTanningButton).text = addTanningText;

            crushBarkButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            crushBarkButton.transform.Translate(0, -0.1f, 0);
            Utils.GetComponentInChildren<UILabel>(crushBarkButton).text = crushBarkText;

            pileBarkButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            pileBarkButton.transform.Translate(0.345f, 0, 0);
            Utils.GetComponentInChildren<UILabel>(pileBarkButton).text = pileBarkText;

            placeBoxButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            placeBoxButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(placeBoxButton).text = placeBoxText;

            AddAction(scrapeButton, new System.Action(OnScrapeFur));
            AddAction(addLeatherButton, new System.Action(OnLeatherAdd));
            AddAction(addTanningButton, new System.Action(OnTanningAdd));
            AddAction(crushBarkButton, new System.Action(OnCrushBark));
            AddAction(pileBarkButton, new System.Action(OnPileBark));
            AddAction(placeBoxButton, new System.Action(OnPlaceBox));

            SetScrapeFurActive(false);
            SetLeatherAddActive(false);
            SetTanningAddActive(false);
            SetCrushBarkActive(false);
            SetPileBarkActive(false);
            SetPlaceBoxActive(false);

        }
        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }
        internal static void SetScrapeFurActive(bool active)
        {
            NGUITools.SetActive(scrapeButton, active);
        }
        internal static void SetLeatherAddActive(bool active)
        {
            NGUITools.SetActive(addLeatherButton, active);
        }

        internal static void SetTanningAddActive(bool active)
        {
            NGUITools.SetActive(addTanningButton, active);
        }

        internal static void SetCrushBarkActive(bool active)
        {
            NGUITools.SetActive(crushBarkButton, active);
        }

        internal static void SetPileBarkActive(bool active)
        {
            NGUITools.SetActive(pileBarkButton, active);
        }

        internal static void SetPlaceBoxActive(bool active)
        {
            NGUITools.SetActive(placeBoxButton, active);
        }

        private static void OnScrapeFur()
        {
            var thisGearItem = LWFunctionalities.furItem;
            furName = thisGearItem.gameObject.name;        
        
            if (thisGearItem == null) return;

            if (GameManager.GetInventoryComponent().GearInInventory(LeatherworksUtils.knife1, 1) || GameManager.GetInventoryComponent().GearInInventory(LeatherworksUtils.knife2, 1))
            {
                GameAudioManager.PlayGuiConfirm();
                thisGearItem.m_CurrentHP = thisGearItem.m_CurrentHP - 10;
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_LW_ScrapeProgressBar"), 5f, 0f, 0f,
                                "PLAY_HARVESTINGLEATHER", null, false, true, new System.Action<bool, bool, float>(OnScrapeFurFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(furName, 1);
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoScrape"));
                GameAudioManager.PlayGUIError();
            }

        }
        private static void OnScrapeFurFinished(bool success, bool playerCancel, float progress)
        {
            float rabbitamount = Settings.instance.rabbitYield;
            int rabbityield = Convert.ToInt32(rabbitamount);

            float wolfdeeramount = Settings.instance.wolfdeerYield;
            int wolfdeeryield = Convert.ToInt32(wolfdeeramount);

            float moosebearamount = Settings.instance.moosebearYield;
            int moosebearyield = Convert.ToInt32(moosebearamount);

            if (furName.ToLowerInvariant().Contains("wolf") || furName.ToLowerInvariant().Contains("leatherhide"))
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(LeatherworksUtils.leatherParts, rabbityield);
            }
            else if (furName.ToLowerInvariant().Contains("rabbit"))
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(LeatherworksUtils.leatherParts, wolfdeeryield);
            }
            else if (furName.ToLowerInvariant().Contains("moose") || furName.ToLowerInvariant().Contains("bear"))
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(LeatherworksUtils.leatherParts, moosebearyield);
            }


        }
        private static void OnLeatherAdd()
        {
            var thisGearItem = LWFunctionalities.recipientBoxItem;
            GearItem leatherScraped = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_LeatherScraped");

            if (thisGearItem == null) return;
            if (leatherScraped == null)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoLeatherScraped"));
                GameAudioManager.PlayGUIError();
                return;
            }
            if (thisGearItem.name == "GEAR_MetalBoxTanFilled")
            {
                recipientBoxItemName = thisGearItem.name;
                if (leatherScraped.m_StackableItem.m_Units < 5)
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoLeatherScraped"));
                    GameAudioManager.PlayGUIError();
                    return;
                }
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_LW_TanLeatherProgressBar"), 5f, 0f, 0f,
                                "PLAY_PUTINPOTWATERACORNSSHELLED", null, false, true, new System.Action<bool, bool, float>(OnLeatherAddFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(leatherScraped.name, 5);
                GearItem.Destroy(thisGearItem);
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoLeatherScraped"));
                GameAudioManager.PlayGUIError();
            }

        }
        private static void OnLeatherAddFinished(bool success, bool playerCancel, float progress)
        {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(LeatherworksUtils.boxTanStart, 1);
        }

        private static void OnTanningAdd()
        {
            var thisGearItem = LWFunctionalities.tanningItem;
            GearItem tanningliquid = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_CookedTanning");

            if (thisGearItem == null) return;
            if (tanningliquid == null)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoTanning"));
                GameAudioManager.PlayGUIError();
                return;
            }
            if (thisGearItem.name == "GEAR_MetalBoxForge")
            {
                tanningName = thisGearItem.name;
                if (tanningliquid.m_StackableItem.m_Units < 1)
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoTanning"));
                    GameAudioManager.PlayGUIError();
                    return;
                }
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_LW_AddTanningProgressBar"), 5f, 0f, 0f,
                                "PLAY_PUTINPOTWATER", null, false, true, new System.Action<bool, bool, float>(OnTanningAddFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(tanningliquid.name, 1);
                GearItem.Destroy(thisGearItem);
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoTanning"));
                GameAudioManager.PlayGUIError();
            }

        }
        private static void OnTanningAddFinished(bool success, bool playerCancel, float progress)
        {
            GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(LeatherworksUtils.tanFilledBox, 1);
        }
        private static void OnCrushBark()
        {

            var thisGearItem = LWFunctionalities.barkItem;
            // float amount = Settings.instance.flourAmount;
            // int amountint = Convert.ToInt32(amount);
            // Settings.instance.noGrind == true

            GearItem fried1 = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_BarkPreparedFried");
            GearItem fried2 = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_BarkPreparedFriedPile");

            if (thisGearItem == null) return;
            if (Settings.instance.noGrind == true)
            {
                if (GameManager.GetInventoryComponent().GearInInventory(LeatherworksUtils.hammer2, 1) || GameManager.GetInventoryComponent().GearInInventory(LeatherworksUtils.hammer1, 1))
                {
                    if (thisGearItem.name == "GEAR_BarkPreparedFried")
                    {
                        barkName = thisGearItem.name;

                        if (fried1.m_StackableItem.m_Units < 35)
                        {
                            HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoBarkSmall"));
                            GameAudioManager.PlayGUIError();
                            return;
                        }
                        GameAudioManager.PlayGuiConfirm();
                        InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_LW_CrushProgressBar"), 3f, 0f, 0f,
                                        "PLAY_CRAFTINGACORNSGRINDING", null, false, true, new System.Action<bool, bool, float>(OnCrushBarkFinished));
                        GameManager.GetInventoryComponent().RemoveGearFromInventory(fried1.name, 35);
                    }
                    else if (thisGearItem.name == "GEAR_BarkPreparedFriedPile")
                    {
                        barkName = thisGearItem.name;

                        if (fried2.m_StackableItem.m_Units < 7)
                        {
                            HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoBarkLarge"));
                            GameAudioManager.PlayGUIError();
                            return;
                        }
                        GameAudioManager.PlayGuiConfirm();
                        InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_LW_CrushProgressBar"), 3f, 0f, 0f,
                                        "PLAY_CRAFTINGACORNSGRINDING", null, false, true, new System.Action<bool, bool, float>(OnCrushBarkFinished));
                        GameManager.GetInventoryComponent().RemoveGearFromInventory(fried2.name, 7);
                    }
                }
                else
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoBark"));
                    GameAudioManager.PlayGUIError();
                }
            }
            else
            {
                if (GameManager.GetInventoryComponent().GearInInventory(LeatherworksUtils.hammer1, 1))
                {
                    if (thisGearItem.name == "GEAR_BarkPreparedFried")
                    {
                        barkName = thisGearItem.name;

                        if (fried1.m_StackableItem.m_Units < 35)
                        {
                            HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoBarkSmall"));
                            GameAudioManager.PlayGUIError();
                            return;
                        }
                        GameAudioManager.PlayGuiConfirm();
                        InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_LW_CrushProgressBar"), 3f, 0f, 0f,
                                        "PLAY_CRAFTINGACORNSGRINDING", null, false, true, new System.Action<bool, bool, float>(OnCrushBarkFinished));
                        GameManager.GetInventoryComponent().RemoveGearFromInventory(fried1.name, 35);
                    }
                    else if (thisGearItem.name == "GEAR_BarkPreparedFriedPile")
                    {
                        barkName = thisGearItem.name;

                        if (fried2.m_StackableItem.m_Units < 7)
                        {
                            HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoBarkLarge"));
                            GameAudioManager.PlayGUIError();
                            return;
                        }
                        GameAudioManager.PlayGuiConfirm();
                        InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_LW_CrushProgressBar"), 3f, 0f, 0f,
                                        "PLAY_CRAFTINGACORNSGRINDING", null, false, true, new System.Action<bool, bool, float>(OnCrushBarkFinished));
                        GameManager.GetInventoryComponent().RemoveGearFromInventory(fried2.name, 7);
                    }
                }
                else
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoBark"));
                    GameAudioManager.PlayGUIError();
                }
            }
        }
        private static void OnCrushBarkFinished(bool success, bool playerCancel, float progress)
        {
            GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(LeatherworksUtils.flour, 1);
        }
        private static void OnPileBark()
        {

            var thisGearItem = LWFunctionalities.pileItem;

            GearItem notfried = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_BarkPrepared");
            GearItem fried = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_BarkPreparedFried");

            if (thisGearItem == null) return;

            if (thisGearItem.name == "GEAR_BarkPreparedFried")
            {
                pileName = thisGearItem.name;

                if (fried.m_StackableItem.m_Units < 5)
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoBarkSmallPile"));
                    GameAudioManager.PlayGUIError();
                    return;
                }
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_LW_PileProgressBar"), 1f, 0f, 0f,
                                "PLAY_CRAFTINGACORNSSHELLING", null, false, true, new System.Action<bool, bool, float>(OnPileBarkFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(fried.name, 5);
            }
            else if(thisGearItem.name == "GEAR_BarkPrepared")
            {
                pileName = thisGearItem.name;

                if (notfried.m_StackableItem.m_Units < 5)
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoBarkSmallPile"));
                    GameAudioManager.PlayGUIError();
                    return;
                }
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_LW_PileProgressBar"), 1f, 0f, 0f,
                                "PLAY_CRAFTINGACORNSSHELLING", null, false, true, new System.Action<bool, bool, float>(OnPileBarkFinished));
                GameManager.GetInventoryComponent().RemoveGearFromInventory(notfried.name, 5);
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_LW_NoBark"));
                GameAudioManager.PlayGUIError();
            }

        }
        private static void OnPileBarkFinished(bool success, bool playerCancel, float progress)
        {
            
            if (pileName.ToLowerInvariant().Contains("fried"))
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(LeatherworksUtils.barkFriedPile, 1);
            }
            else
            {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(LeatherworksUtils.barkPile, 1);
            }
        }

        private static void OnPlaceBox()
        {
            var toDrop = placeBoxItem?.m_StackableItem?.m_UnitsPerItem ?? 1;
            toDrop = Mathf.Clamp(toDrop, 0, placeBoxItem?.m_StackableItem?.m_Units ?? 1);
            var dropped = placeBoxItem.Drop(toDrop);
            LeatherworksUtils.inventory.OnBack();
            dropped.PerformAlternativeInteraction();
        }

    }
}
