using System;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace ArchaicWisp
{
    public class Prefab
    {
        private static bool initialized = false;

        public static void Init()
        {
            if (initialized) return;
            initialized = true;

            CreateBody();
            CreateMaster();
            AddSniperTarget();
        }

        private static void AddSniperTarget()
        {
            ModelLocator ml = ArchaicWispContent.ArchaicWispObject.GetComponent<ModelLocator>();
            HurtBoxGroup hbg = ml.modelTransform.gameObject.GetComponent<HurtBoxGroup>();

            GameObject transformTarget = hbg.mainHurtBox.transform.gameObject;

            Collider oldCollider = hbg.mainHurtBox.GetComponent<Collider>();
            UnityEngine.Object.Destroy(hbg.mainHurtBox);
            UnityEngine.Object.Destroy(oldCollider);

            CapsuleCollider c = transformTarget.AddComponent<CapsuleCollider>();
            c.height = 3.6f;
            c.radius = 1f;
            c.direction = 2;

            HurtBox hb = transformTarget.AddComponent<HurtBox>();
            hb.isBullseye = true;
            hb.healthComponent = ArchaicWispContent.ArchaicWispObject.GetComponent<HealthComponent>();
            hb.damageModifier = HurtBox.DamageModifier.Normal;
            hb.isSniperTarget = true;
            hb.hurtBoxGroup = hbg;
            hb.indexInGroup = 0;

            hbg.mainHurtBox = hb;
            hbg.hurtBoxes[0] = hb;
        }

        private static void CreateMaster()
        {
            ArchaicWispContent.ArchaicWispMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Junk/ArchWisp/ArchWispMaster.prefab").WaitForCompletion().InstantiateClone("MoffeinArchWispMaster", true);
            CharacterMaster cm = ArchaicWispContent.ArchaicWispMaster.GetComponent<CharacterMaster>();
            cm.bodyPrefab = ArchaicWispContent.ArchaicWispObject;
        }

        private static void CreateBody()
        {
            ArchaicWispContent.ArchaicWispObject = Addressables.LoadAssetAsync<GameObject>("RoR2/Junk/ArchWisp/ArchWispBody.prefab").WaitForCompletion().InstantiateClone("MoffeinArchWisp", true);

            CharacterBody cb = ArchaicWispContent.ArchaicWispObject.GetComponent<CharacterBody>();
            cb.baseNameToken = "MOFFEIN_ARCHWISP_BODY_NAME";
            cb.baseMaxHealth = 900f;    //GW 750
            cb.baseDamage = 15f;    //GW 15
            cb.baseRegen = 0f;
            cb.bodyFlags = CharacterBody.BodyFlags.None;
            cb.portraitIcon = ArchaicWispContent.assets.LoadAsset<Texture2D>("icon");
            cb.baseArmor = 0f;

            cb.levelMaxHealth = cb.baseMaxHealth * 0.3f;
            cb.levelDamage = cb.baseDamage * 0.2f;
            cb.levelRegen = cb.baseRegen * 0.2f;
            cb.levelArmor = 0f;

            cb.preferredInitialStateType = new EntityStates.SerializableEntityStateType(typeof(EntityStates.GreaterWispMonster.SpawnState));

            AddSSoH(ArchaicWispContent.ArchaicWispObject);

            DeathRewards dr = ArchaicWispContent.ArchaicWispObject.GetComponent<DeathRewards>();
            ArchaicWispContent.ArchaicWispLogbookUnlockable = ScriptableObject.CreateInstance<UnlockableDef>();
            ArchaicWispContent.ArchaicWispLogbookUnlockable.nameToken = "UNLOCKABLE_LOG_MOFFEIN_MOFFEIN_ARCHWISP";
            ArchaicWispContent.ArchaicWispLogbookUnlockable.cachedName = "MOFFEIN_ARCHWISP_BODY_NAME";
            dr.logUnlockableDef = ArchaicWispContent.ArchaicWispLogbookUnlockable;
        }

        private static void AddSSoH(GameObject enemyObject)
        {
            EntityStateMachine body = null;
            EntityStateMachine weapon = null;
            EntityStateMachine[] stateMachines = enemyObject.GetComponents<EntityStateMachine>();
            foreach (EntityStateMachine esm in stateMachines)
            {
                switch (esm.customName)
                {
                    case "Body":
                        body = esm;
                        break;
                    case "Weapon":
                        weapon = esm;
                        break;
                    default:
                        break;
                }
            }

            SetStateOnHurt ssoh = enemyObject.GetComponent<SetStateOnHurt>();
            if (!ssoh) ssoh = enemyObject.AddComponent<SetStateOnHurt>();
            ssoh.canBeFrozen = true;
            ssoh.canBeStunned = true;
            ssoh.canBeHitStunned = false;
            ssoh.hitThreshold = 0.5f;
            ssoh.targetStateMachine = body;
            ssoh.idleStateMachine = new EntityStateMachine[] { weapon };
            ssoh.hurtState = new EntityStates.SerializableEntityStateType(typeof(EntityStates.HurtStateFlyer));

            body.initialStateType = new EntityStates.SerializableEntityStateType(typeof(EntityStates.GreaterWispMonster.SpawnState));
        }
    }
}
