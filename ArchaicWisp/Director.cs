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

            DirectorCardCategorySelection dissonanceSpawns = Addressables.LoadAssetAsync<DirectorCardCategorySelection>("RoR2/Base/MixEnemy/dccsMixEnemy.asset").WaitForCompletion();
            dissonanceSpawns.AddCard(1, archWispDC);
            //0 is Champion
            //1 is Miniboss
            //2 is BasicMonsters

            foreach (StageSpawnInfo ssi in ArchaicWispPlugin.StageList)
            {
                DirectorAPI.DirectorCardHolder toAdd = ssi.GetMinStages() == 0 ? ArchaicWispContent.ArchaicWispCard : ArchaicWispContent.ArchaicWispLoopCard;

                SceneDef sd = ScriptableObject.CreateInstance<SceneDef>();
                sd.baseSceneNameOverride = ssi.GetStageName();

                DirectorAPI.Helpers.AddNewMonsterToStage(toAdd, false, DirectorAPI.GetStageEnumFromSceneDef(sd), ssi.GetStageName());
            }
        }
    }
}
