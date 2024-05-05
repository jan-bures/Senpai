/* Senpai
 * Copyright (C) 2024  munix
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using BepInEx;
using HarmonyLib;
using I2.Loc;
using JetBrains.Annotations;
using SpaceWarp;
using SpaceWarp.API.Mods;
using UnityEngine;

namespace Senpai;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
public class SenpaiPlugin : BaseSpaceWarpPlugin
{
    [PublicAPI] public static SenpaiPlugin Instance { get; set; }

    public override void OnInitialized()
    {
        base.OnInitialized();

        Instance = this;

        Harmony.CreateAndPatchAll(typeof(SenpaiPlugin));
    }

    [HarmonyPostfix]
    [HarmonyPatch(
        typeof(LocalizationManager),
        nameof(LocalizationManager.GetTranslation),
        [
            typeof(string), typeof(bool), typeof(int), typeof(bool), typeof(bool), typeof(GameObject), typeof(string),
            typeof(bool)
        ]
    )]
    // ReSharper disable once InconsistentNaming
    private static void GetTranslationPatch(ref string __result)
    {
        __result = Uwuinator.Uwuinate(__result);
    }

    [HarmonyPostfix]
    [HarmonyPatch(
        typeof(LocalizationManager),
        nameof(LocalizationManager.TryGetTranslation)
    )]
    private static void TryGetTranslationPatch(ref bool __result, ref string Translation)
    {
        if (__result)
        {
            Translation = Uwuinator.Uwuinate(Translation);
        }
    }
}