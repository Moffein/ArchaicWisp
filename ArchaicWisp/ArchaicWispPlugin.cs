﻿using RoR2;
using UnityEngine;
using BepInEx;
using R2API;
using BepInEx.Configuration;
using System.Linq;
using System.Collections.Generic;
using RoR2.ContentManagement;
using R2API.Utils;
using MonoMod.Cil;
using System;
using System.Reflection;

namespace ArchaicWisp
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInDependency(R2API.PrefabAPI.PluginGUID)]
    [BepInDependency(R2API.DirectorAPI.PluginGUID)]
    [BepInPlugin("com.Moffein.ArchaicWisp", "Archaic Wisp", "1.1.2")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    public class ArchaicWispPlugin : BaseUnityPlugin
    {
        public static PluginInfo pluginInfo;
        public static List<StageSpawnInfo> StageList = new List<StageSpawnInfo>();
        public void Awake()
        {
            pluginInfo = Info;
            new LanguageTokens();
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchaicWisp.archwispbundle"))
            {
                ArchaicWispContent.assets = AssetBundle.LoadFromStream(stream);
            }

            ReadConfig();

            Prefab.Init();
            Director.Init();
            ModifySkills.Init();

            RoR2Application.onLoad += LateSetup;
            ContentManager.collectContentPackProviders += ContentManager_collectContentPackProviders;

            On.EntityStates.GreaterWispMonster.ChargeCannons.OnEnter += (orig, self) =>
            {
                orig(self);
                Debug.Log(self.baseDuration);
            };
        }

        public void LateSetup()
        {
            ItemDisplays.DisplayRules(ArchaicWispContent.ArchaicWispObject);
        }

        public void ReadConfig()
        {
            string stages = base.Config.Bind<string>(new ConfigDefinition("Spawns", "Stage List"), "goldshores, dampcavesimple, itdampcave, sulfurpools, skymeadow, itskymeadow, wispgraveyard - loop", new ConfigDescription("What stages the monster will show up on. Add a '- loop' after the stagename to make it only spawn after looping. List of stage names can be found at https://github.com/risk-of-thunder/R2Wiki/wiki/List-of-scene-names")).Value;
            string gwRemoveStages = base.Config.Bind<string>(new ConfigDefinition("Spawns", "Remove Greater Wisps"), "goldshores, dampcavesimple, itdampcave, sulfurpools, skymeadow, itskymeadow", new ConfigDescription("Remove Greater Wisps from these stages to prevent role overlap.")).Value;

            //parse stage
            stages = new string(stages.ToCharArray().Where(c => !System.Char.IsWhiteSpace(c)).ToArray());
            string[] splitStages = stages.Split(',');
            foreach (string str in splitStages)
            {
                string[] current = str.Split('-');

                string name = current[0];
                int minStages = 0;
                if (current.Length > 1)
                {
                    minStages = 5;
                }

                StageList.Add(new StageSpawnInfo(name, minStages));
            }

            //parse removeGW
            gwRemoveStages = new string(gwRemoveStages.ToCharArray().Where(c => !System.Char.IsWhiteSpace(c)).ToArray());
            string[] splitimpRemoveStages = stages.Split(',');
            foreach (string str in splitimpRemoveStages)
            {
                string[] current = str.Split('-');  //in case people try to use the Stage List format

                string name = current[0];

                SceneDef sd = ScriptableObject.CreateInstance<SceneDef>();
                sd.baseSceneNameOverride = name;

                DirectorAPI.Helpers.RemoveExistingMonsterFromStage(DirectorAPI.Helpers.MonsterNames.GreaterWisp, DirectorAPI.GetStageEnumFromSceneDef(sd), name);
            }
        }

        private void ContentManager_collectContentPackProviders(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(new ArchaicWispContent());
        }
    }
    public class StageSpawnInfo
    {
        private string stageName;
        private int minStages;

        public StageSpawnInfo(string stageName, int minStages)
        {
            this.stageName = stageName;
            this.minStages = minStages;
        }

        public string GetStageName() { return stageName; }
        public int GetMinStages() { return minStages; }
    }
}
