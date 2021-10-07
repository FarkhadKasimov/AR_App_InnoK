using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class BundleCreator
{
    [MenuItem("AssetBundle_Creator/Build Android Bundle")]
    static void BuildBundleAndroid()
    {
        string path = EditorUtility.SaveFolderPanel("Save Bundle", "", "");
        path += "/Android_Bundle";
        Directory.CreateDirectory(path);

        if (path.Length != 0)
        {
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.Android);
        }

        FileUtil.DeleteFileOrDirectory(path + "/Android_Bundle");
        FileUtil.DeleteFileOrDirectory(path + "/Android_Bundle.manifest");
    }


    [MenuItem("AssetBundle_Creator/Build IOS Bundle")]
    static void BuildBundleIOS()
    {
        string path = EditorUtility.SaveFolderPanel("Save Bundle", "", "");
        path += "/IOS_Bundle";
        Directory.CreateDirectory(path);
        if (path.Length != 0)
        {
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.iOS);
        }

        FileUtil.DeleteFileOrDirectory(path + "/IOS_Bundle");
        FileUtil.DeleteFileOrDirectory(path + "/IOS_Bundle.manifest");
    }
}
