//using HarmonyLib;
//using MelonLoader;

//namespace Leatherworks
//{
   // internal class SettingsPatches
  //  {
       // [HarmonyPatch] // ModComponent patch of gear spawns
        //class ManageSpawnsToolbox
    //    {
          //  public static System.Reflection.MethodBase TargetMethod()
      //      {
            //    var type = AccessTools.TypeByName("ModComponent.Mapper.ZipFileLoader");
              //  return AccessTools.FirstMethod(type, method => method.Name.Contains("TryHandleTxt"));
        //    }
           // public static bool Prefix(string zipFilePath, string internalPath, ref string text, ref bool __result)
          //  {
             //   if (zipFilePath.EndsWith("ToolUpgrade_Copy.modcomponent"))
            //    {
               //     string fileName = internalPath.Replace("gear-spawns/", "").Replace(".txt", "");           

                 //   if (Settings.instance.noBattery && fileName == "Batteries")
              //      {
                   //     MelonLogger.Msg(ConsoleColor.DarkYellow, "Skipping based on settings: " + fileName);
                     //   text = "";
                //    }
             //   }


               // return true;
      //      }
    //    }
  //  }
//}

