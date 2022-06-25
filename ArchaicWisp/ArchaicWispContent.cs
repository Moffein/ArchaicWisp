
using System;
using RoR2.ContentManagement;
using System.Collections;
using UnityEngine;
using R2API;
using System.Collections.Generic;

namespace ArchaicWisp
{
    public class ArchaicWispContent : IContentPackProvider
    {
        public static AssetBundle assets;
        public static ContentPack content = new ContentPack();
        public static GameObject ArchaicWispObject;
        public static GameObject ArchaicWispMaster;
        //public static DamageAPI.ModdedDamageType ClayGooClayMan;
        public static DirectorAPI.DirectorCardHolder ArchaicWispCard;
        public static DirectorAPI.DirectorCardHolder ArchaicWispLoopCard;

        public static List<GameObject> projectilePrefabs = new List<GameObject>();

        public string identifier => "MoffeinArchaicWisp.content";

        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }

        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(content, args.output);
            yield break;
        }

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            content.bodyPrefabs.Add(new GameObject[] { ArchaicWispContent.ArchaicWispObject });
            content.masterPrefabs.Add(new GameObject[] { ArchaicWispContent.ArchaicWispMaster });
            content.projectilePrefabs.Add(projectilePrefabs.ToArray());
            yield break;
        }
    }
}
