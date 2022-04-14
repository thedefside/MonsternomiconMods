// MonsternomiconMods
// a Valheim mod skeleton using Jötunn
// 
// File:    MonsternomiconMods.cs
// Project: MonsternomiconMods

using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Monsternomicon
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class MonsternomiconMods : BaseUnityPlugin
    {
        public const string PluginGUID = "Belasias.Monsternomicon";
        public const string PluginName = "Monsternomicon";
        public const string PluginVersion = "0.0.1";
        
        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        private void Awake()
        {
            // Before we can copy vanilla items, we need to wait until they are loaded. This code will subscribe to an event. When that event occurs, our method will be executed.
            PrefabManager.OnVanillaPrefabsAvailable += AddClonedItems;
        }

        private void AddClonedItems()
        {
            Jotunn.Logger.LogInfo("Adding armors");

            // helmet

            // clone the vanilla prefab into a new prefab
            var helmetMuspelheim = new CustomItem("HelmetMuspelHeim", "HelmetPadded", new ItemConfig
            {
                Name = "$item_helmetmuspelheim_name",  // this is a translation token. Anything that starts with $ will be replaced with the matching value in the /Assets/Translations for the language the game is set to
                Description = "$item_helmetmuspelheim_description",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 3,
                Icons = new Sprite[] { LoadEmbeddedSprite("helmetmuspelheimicon") },
                Requirements = new RequirementConfig[]
                {
                    new RequirementConfig
                    {
                        Item = "BlackMetal",
                        Amount = 20,
                        AmountPerLevel = 5,
                        Recover = true // this is true by default, so unless you are setting it to false it's not actually requred to set it
                    },
                    new RequirementConfig
                    {
                        Item = "JuteRed",
                        Amount = 5,
                        AmountPerLevel = 1,
                        Recover = true
                    },
                }
            });

            // we need to create a new 'material' based on the original material, this way we won't change the original item's textures and colors
            var muspelheimMaterial = helmetMuspelheim.ItemPrefab.GetComponentInChildren<MeshRenderer>().material;
            var newMuspelheimMaterial = new Material(muspelheimMaterial);


            // change the textures on the new material
            newMuspelheimMaterial.SetTexture("_MainTex", LoadEmbeddedTexture("Muspelheim_MainTex"));
            newMuspelheimMaterial.SetTexture("_BumpMap", LoadEmbeddedTexture("Muspelheim_BumpMap"));
            newMuspelheimMaterial.SetTexture("_MetallicGlossMap", LoadEmbeddedTexture("Muspelheim_MetallicGlossMap"));

            // set the new material to the the new prefab
            helmetMuspelheim.ItemPrefab.ChangeMaterials(newMuspelheimMaterial);

            // most of the fields for items can be found in the 'ItemDrop' component
            var helmetItem = helmetMuspelheim.ItemPrefab.GetComponent<ItemDrop>();
            helmetItem.m_itemData.m_shared.m_armor = 44;
            helmetItem.m_itemData.m_shared.m_maxDurability = 1500;

            // add our new custom item to the game
            ItemManager.Instance.AddItem(helmetMuspelheim);
            
            
            // legs armor
            var armorMuspelHeimLegs = new CustomItem("ArmorMuspelHeimLegs", "ArmorPaddedGreaves", new ItemConfig
            {
                Name = "$item_armormuspelheimlegs_name",
                Description = "$item_armormuspelheimlegs_description",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 3,
                Icons = new Sprite[] { LoadEmbeddedSprite("helmetmuspelheimicon") },
                Requirements = new RequirementConfig[]
                {
                    new RequirementConfig
                    {
                        Item = "BlackMetal",
                        Amount = 20,
                        AmountPerLevel = 5
                    },
                    new RequirementConfig
                    {
                        Item = "JuteRed",
                        Amount = 5,
                        AmountPerLevel = 1
                    },
                }
            });

            var muspelheimLegsMaterial = armorMuspelHeimLegs.ItemPrefab.GetComponentInChildren<MeshRenderer>().material;
            var newMuspelheimLegsMaterial = new Material(muspelheimLegsMaterial);


            // change the textures on the new material
            newMuspelheimLegsMaterial.SetTexture("_LegsTex", LoadEmbeddedTexture("Muspelheim_MainTex"));
            newMuspelheimLegsMaterial.SetTexture("_LegsBumpMap", LoadEmbeddedTexture("Muspelheim_BumpMap"));
            newMuspelheimLegsMaterial.SetTexture("_LegsMetal", LoadEmbeddedTexture("Muspelheim_MetallicGlossMap"));
            
            armorMuspelHeimLegs.ItemPrefab.ChangeMaterials(newMuspelheimMaterial);

            var legsItem = armorMuspelHeimLegs.ItemPrefab.GetComponent<ItemDrop>();
            legsItem.m_itemData.m_shared.m_armor = 44;
            legsItem.m_itemData.m_shared.m_maxDurability = 1500;

            ItemManager.Instance.AddItem(armorMuspelHeimLegs);

            // chest armor
            var armorMuspelHeimChest = new CustomItem("ArmorMuspelHeimChest", "ArmorPaddedCuirass", new ItemConfig
            {
                Name = "$item_armormuspelheimchest_name",
                Description = "$item_armormuspelheimchest_description",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 3,
                Icons = new Sprite[] { LoadEmbeddedSprite("helmetmuspelheimicon") },
                Requirements = new RequirementConfig[]
                {
                    new RequirementConfig
                    {
                        Item = "BlackMetal",
                        Amount = 20,
                        AmountPerLevel = 5
                    },
                    new RequirementConfig
                    {
                        Item = "JuteRed",
                        Amount = 5,
                        AmountPerLevel = 1
                    },
                }
            });

            var muspelheimChestMaterial = armorMuspelHeimChest.ItemPrefab.GetComponentInChildren<MeshRenderer>().material;
            var newMuspelheimChestMaterial = new Material(muspelheimChestMaterial);


            // change the textures on the new material
            newMuspelheimChestMaterial.SetTexture("_ChestTex", LoadEmbeddedTexture("Muspelheim_MainTex"));
            newMuspelheimChestMaterial.SetTexture("_ChestBumpMap", LoadEmbeddedTexture("Muspelheim_BumpMap"));
            newMuspelheimChestMaterial.SetTexture("_ChestMetal", LoadEmbeddedTexture("Muspelheim_MetallicGlossMap"));

            armorMuspelHeimChest.ItemPrefab.ChangeMaterials(newMuspelheimChestMaterial);

            var chestItem = armorMuspelHeimChest.ItemPrefab.GetComponent<ItemDrop>();
            chestItem.m_itemData.m_shared.m_armor = 44;
            chestItem.m_itemData.m_shared.m_maxDurability = 1500;

            ItemManager.Instance.AddItem(armorMuspelHeimChest);


            // after adding our items, unsubscribe from the event so it doesn't get called again. otherwise there will be errors that the item already exists
            PrefabManager.OnVanillaPrefabsAvailable -= AddClonedItems;
        }
               

        private Texture2D LoadEmbeddedTexture(string imageName)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            var resourceName = myAssembly.GetManifestResourceNames().Single(str => str == $"Monsternomicon.Assets.Textures.{imageName}.png");
            if (resourceName is null) throw new Exception($"{imageName}.png not found.");
            Jotunn.Logger.LogDebug($"Resource name : {resourceName}");

            Stream stream = myAssembly.GetManifestResourceStream(resourceName);
            var byteArray = ReadFully(stream);
                ;
            var tex = new Texture2D(2, 2);
            tex.LoadImage(byteArray);
            return tex;
        }

        private Sprite LoadEmbeddedSprite(string imageName)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            var resourceName = myAssembly.GetManifestResourceNames().SingleOrDefault(str => str == $"Monsternomicon.Assets.Sprites.{imageName}.png");
            if (resourceName is null) throw new Exception($"{imageName}.png not found.");
            Jotunn.Logger.LogDebug($"Resource name : {resourceName}");

            Stream stream = myAssembly.GetManifestResourceStream(resourceName);
            var byteArray = ReadFully(stream);
                ;
            var tex = new Texture2D(2, 2);
            tex.LoadImage(byteArray);
            var sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), Vector2.zero);
            return sprite;
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }

    public static class GameObjectExtensions
    {
        public static GameObject ChangeMaterials(this GameObject go, Material newMaterial)
        {

            foreach (var renderer in go.GetComponentsInChildren<MeshRenderer>(true))
            {
                renderer.material = newMaterial;
            }

            foreach (var renderer in go.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                renderer.material = newMaterial;
            }

            return go;
        }
    }
}

