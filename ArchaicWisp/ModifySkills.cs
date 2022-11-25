using UnityEngine;
using RoR2;
using RoR2.Projectile;
using UnityEngine.AddressableAssets;
using R2API;

namespace ArchaicWisp
{
    public class ModifySkills
    {
        private static bool initialized = false;

        public static void Init()
        {
            if (initialized) return;
            initialized = true;

            GameObject archWispProjectile = Addressables.LoadAssetAsync<GameObject>("RoR2/Junk/ArchWisp/ArchWispCannon.prefab").WaitForCompletion().InstantiateClone("MoffeinArchWispCannon", true);
            ArchaicWispContent.projectilePrefabs.Add(archWispProjectile);

            archWispProjectile.GetComponent<Rigidbody>().useGravity = false;

            ProjectileImpactExplosion pie = archWispProjectile.GetComponent<ProjectileImpactExplosion>();
            pie.lifetime = 7f;
            pie.falloffModel = BlastAttack.FalloffModel.SweetSpot;

            ProjectileSimple ps = pie.GetComponent<ProjectileSimple>();
            ps.lifetime = 7f;

            GameObject archWispProjectileGround = pie.childrenProjectilePrefab.InstantiateClone("MoffeinArchWispCannonGround", true);
            pie.childrenProjectilePrefab = archWispProjectileGround;
            ArchaicWispContent.projectilePrefabs.Add(archWispProjectileGround);

            ProjectileDamageTrail pdt = archWispProjectileGround.GetComponent<ProjectileDamageTrail>();
            pdt.damageToTrailDpsFactor = 0.35f;  //0.2 vanilla
            pdt.trailLifetimeAfterExpiration = 8f;  //4f vanilla, this doesn't seem to do anything even when changed

            ProjectileImpactExplosion pie2 = archWispProjectileGround.GetComponent<ProjectileImpactExplosion>();
            //pie2.lifetime = 2f;   //Don't change this. Fireball visual only lasts for 1s and disappears after. Default = 1
            pie2.falloffModel = BlastAttack.FalloffModel.SweetSpot;

            SneedUtils.SneedUtils.SetEntityStateField("EntityStates.ArchWispMonster.ChargeCannons", "baseDuration", "2");
            SneedUtils.SneedUtils.SetEntityStateField("EntityStates.ArchWispMonster.FireCannons", "damageCoefficient", "3.6");
            SneedUtils.SneedUtils.SetEntityStateField("EntityStates.ArchWispMonster.FireCannons", "projectilePrefab", archWispProjectile);
        }
    }
}
