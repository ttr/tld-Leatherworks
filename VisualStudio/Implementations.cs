using Il2Cpp;
using Leatherworks;
using MelonLoader;
using UnityEngine;

namespace ModNamespace;
internal sealed class Implementations : MelonMod
{
    
    public override void OnInitializeMelon()
	{
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Scraping hides...");
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Distributing tree bark...");
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Filling bottles...");
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Green, "Leatherworks Loaded!");
        Settings.instance.AddToModSettings("Leatherworks");
    }

   
}
