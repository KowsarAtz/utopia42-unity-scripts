using System.IO;
using Models;
using UnityEditor;
using UnityEngine;

class FbxAnimationExtractor
{
    static void Execute()
    {
        // PlayerSettings.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
        // PlayerSettings.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
        // PlayerSettings.SetStackTraceLogType(LogType.Assert, StackTraceLogType.None);

        var orgClip =
            (AnimationClip) AssetDatabase.LoadAssetAtPath("Assets/Animations/Ch31_nonPBR@BodyBlock.fbx",
                typeof(AnimationClip));
        Debug.Log("clip name: " + orgClip.name);


        var myPrefab = new GameObject();
        myPrefab.SetActive(false);
        var c = myPrefab.AddComponent<CustomAnimation>();
        c.customAnim = orgClip;

        PrefabUtility.SaveAsPrefabAsset(myPrefab,
            "Assets/Prefabs/" + orgClip.name + ".prefab");
        AssetDatabase.Refresh();


        const string assetBundleDirectory = "Assets/StreamingAssets";
        if (!Directory.Exists(Application.streamingAssetsPath))
            Directory.CreateDirectory(assetBundleDirectory);

        var buildMap = new AssetBundleBuild[1];
        buildMap[0].assetBundleName = orgClip.name;
        buildMap[0].assetNames = new[] {"Assets/Prefabs/" + orgClip.name + ".prefab"};
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", buildMap, BuildAssetBundleOptions.None,
            BuildTarget.WebGL);
    }
}