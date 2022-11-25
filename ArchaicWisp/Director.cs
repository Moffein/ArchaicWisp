using R2API;
using RoR2;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchaicWisp
{
    public class Director
    {
        private static bool initialized = false;
        public static void Init()
        {
            if (initialized) return;
            initialized = true;

            CharacterSpawnCard GreaterWispCard = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion();

            CharacterSpawnCard archWispCSC = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            archWispCSC.name = "cscArchWisp";
            archWispCSC.prefab = ArchaicWispContent.ArchaicWispMaster;
            archWispCSC.sendOverNetwork = true;
            archWispCSC.hullSize = HullClassification.Human;
            archWispCSC.nodeGraphType = MapNodeGroup.GraphType.Air;
            archWispCSC.requiredFlags = NodeFlags.None;
            archWispCSC.forbiddenFlags = NodeFlags.NoCharacterSpawn;
            archWispCSC.directorCreditCost = 240;    //GW 200
            archWispCSC.occupyPosition = false;
            archWispCSC.loadout = new SerializableLoadout();
            archWispCSC.noElites = false;
            archWispCSC.forbiddenAsBoss = false;

            DirectorCard archWispDC = new DirectorCard
            {
                spawnCard = archWispCSC,
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorAPI.DirectorCardHolder archWispCard = new DirectorAPI.DirectorCardHolder
            {
                Card = archWispDC,
                MonsterCategory = DirectorAPI.MonsterCategory.Minibosses
            };

            DirectorCard archWispLoopDC = new DirectorCard
            {
                spawnCard = archWispCSC,
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorAPI.DirectorCardHolder archWispLoopCard = new DirectorAPI.DirectorCardHolder
            {
                Card = archWispLoopDC,
                MonsterCategory = DirectorAPI.MonsterCategory.Minibosses
            };

            ArchaicWispContent.ArchaicWispCard = archWispCard;
            ArchaicWispContent.ArchaicWispLoopCard = archWispLoopCard;

            int categoryIndex = -1;
            DirectorCardCategorySelection dissonanceSpawns = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/MixEnemy/dccsMixEnemy.asset").WaitForCompletion();
            categoryIndex = FindCategoryIndexByName(dissonanceSpawns, "Minibosses");
            if (categoryIndex >= 0)
            {
                dissonanceSpawns.AddCard(categoryIndex, archWispDC);
            }

            DirectorCardCategorySelection familySpawns = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/Common/dccsWispFamily.asset").WaitForCompletion();

            //int categoryIndex = familySpawns.FindCategoryIndexByName("Minibosses"); //THIS FUNCTION DOESN'T WORK IN VANILLA
            categoryIndex = FindCategoryIndexByName(familySpawns, "Minibosses");
            if (categoryIndex >= 0)
            {
                familySpawns.AddCard(categoryIndex, archWispDC);
            }

            foreach (StageSpawnInfo ssi in ArchaicWispPlugin.StageList)
            {
                DirectorAPI.DirectorCardHolder toAdd = ssi.GetMinStages() == 0 ? ArchaicWispContent.ArchaicWispCard : ArchaicWispContent.ArchaicWispLoopCard;

                SceneDef sd = ScriptableObject.CreateInstance<SceneDef>();
                sd.baseSceneNameOverride = ssi.GetStageName();

                DirectorAPI.Helpers.AddNewMonsterToStage(toAdd, false, DirectorAPI.GetStageEnumFromSceneDef(sd), ssi.GetStageName());
            }
        }

        //Minibosses
        //Basic Monsters
        //Champions
        public static int FindCategoryIndexByName(DirectorCardCategorySelection dcs, string categoryName)
        {
            for (int i = 0; i < dcs.categories.Length; i++)
            {
                if (string.CompareOrdinal(dcs.categories[i].name, categoryName) == 0)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
