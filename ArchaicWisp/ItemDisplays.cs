using UnityEngine;
using RoR2;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System;

namespace ArchaicWisp
{
    public class ItemDisplays
    {
        private static Dictionary<string, GameObject> itemDisplayPrefabs = new Dictionary<string, GameObject>();
        public static List<ItemDisplayRuleSet.KeyAssetRuleGroup> equipmentList;
        public static Transform headTransform;
        public static void DisplayRules(GameObject bodyObject)
        {
            ItemDisplayRuleSet idrsArchWisp = ScriptableObject.CreateInstance<ItemDisplayRuleSet>();

            equipmentList = new List<ItemDisplayRuleSet.KeyAssetRuleGroup>();
            equipmentList.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = RoR2Content.Equipment.AffixPoison,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ElitePoison/DisplayEliteUrchinCrown.prefab").WaitForCompletion(),
                            childName = "HealthBarOrigin",
                            localPos = new Vector3(0F, -0.6F, -0.85F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.25F, 0.25F, 0.25F),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            equipmentList.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = RoR2Content.Equipment.AffixHaunted,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/EliteHaunted/DisplayEliteStealthCrown.prefab").WaitForCompletion(),
                            childName = "HealthBarOrigin",
                            localPos = new Vector3(0F, -0.6F, -0.6F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.25F, 0.25F, 0.25F),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            equipmentList.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = RoR2Content.Equipment.AffixWhite,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/EliteIce/DisplayEliteIceCrown.prefab").WaitForCompletion(),
                            childName = "HealthBarOrigin",
                            localPos = new Vector3(0F, -0.6F, -0.45F),
                            localAngles = new Vector3(0F, 0F, 0F),
                            localScale = new Vector3(0.12F, 0.12F, 0.12F),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            GameObject olHorn = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/EliteLightning/DisplayEliteRhinoHorn.prefab").WaitForCompletion();
            equipmentList.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = RoR2Content.Equipment.AffixBlue,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = olHorn,
                            childName = "HealthBarOrigin",
                            localPos = new Vector3(0F, -0.7874F, -0.92505F),
                            localAngles = new Vector3(80.00003F, 0F, 0F),
                            localScale = new Vector3(0.5F, 0.5F, 0.5F),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = olHorn,
                            childName = "HealthBarOrigin",
                            localPos = new Vector3(0F, -0.81623F, -1.2465F),
                            localAngles = new Vector3(90F, 0F, 0F),
                            localScale = new Vector3(0.8F, 0.8F, 0.8F),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            GameObject blazingHorn = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/EliteFire/DisplayEliteHorn.prefab").WaitForCompletion();
            equipmentList.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = RoR2Content.Equipment.AffixRed,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                    {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = blazingHorn,
                            childName = "HealthBarOrigin",
                            localPos = new Vector3(0.7F, -1F, -1.1F),
                            localAngles = new Vector3(0F, 190F, 180F),
                            localScale = new Vector3(0.4F, 0.4F, 0.4F),
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = blazingHorn,
                            childName = "HealthBarOrigin",
                            localPos = new Vector3(-0.7F, -1F, -1.1F),
                            localAngles = new Vector3(0F, 170F, 180F),
                            localScale = new Vector3(-0.4F, 0.4F, 0.4F),
                            limbMask = LimbFlags.None
                        }
                    }
                }
            });

            equipmentList.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = DLC1Content.Elites.Earth.eliteEquipmentDef,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/EliteEarth/DisplayEliteMendingAntlers.prefab").WaitForCompletion(),
                                childName = "HealthBarOrigin",
                                localPos = new Vector3(0F, -0.63F, -0.75F),
                                localAngles = new Vector3(90F, 0F, 0F),
                                localScale = new Vector3(2.5F, 2.5F, 2.5F),
                                limbMask = LimbFlags.None
                            }
                        }
                }
            });

            /*childName = "HealthBarOrigin",
localPos = new Vector3(0.00888F, -0.79673F, -0.84292F), 0 -1.75 -0.84
localAngles = new Vector3(64.50227F, 180F, 180F),   /+48 +180 +180
localScale = new Vector3(2.5F, 2.5F, 2.5F)*/    //increase 25%

            equipmentList.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = DLC1Content.Elites.Void.eliteEquipmentDef,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = new ItemDisplayRule[]
                        {
                            new ItemDisplayRule
                            {
                                ruleType = ItemDisplayRuleType.ParentedPrefab,
                                followerPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/EliteVoid/DisplayAffixVoid.prefab").WaitForCompletion(),
                                childName = "HealthBarOrigin",
                                localPos = new Vector3(0F, -0.98165F, -1.88916F),
                                localAngles = new Vector3(0F, 180F, 180F),
                                localScale = new Vector3(0.68414f, 0.68414f, 0.68414f),
                                limbMask = LimbFlags.None
                            }
                        }
                }
            });

            idrsArchWisp.keyAssetRuleGroups = equipmentList.ToArray();
            CharacterModel characterModel = bodyObject.GetComponent<ModelLocator>().modelTransform.GetComponent<CharacterModel>();
            characterModel.itemDisplayRuleSet = idrsArchWisp;
            characterModel.itemDisplayRuleSet.GenerateRuntimeValues();

            itemDisplayPrefabs.Clear();
        }
    }
}
