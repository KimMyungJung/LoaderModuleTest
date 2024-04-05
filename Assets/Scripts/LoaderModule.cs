using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class LoaderModule : MonoBehaviour
{
    public Action<GameObject> OnLoadCompleted;

    public void LoadAsset(string assetName)
    {
        string relativePath = ConvertToRelativePath(assetName);

        if (string.IsNullOrEmpty(relativePath))
        {
            Debug.LogError("error path: " + assetName);
            return;
        }

        GameObject prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(relativePath);

        if (prefab != null)
        {
            GameObject loadedObject = Instantiate(prefab);
            OnLoadCompleted?.Invoke(loadedObject);
        }
        else
        {
            Debug.LogError("fail obj file path: " + relativePath);
        }
    }

    private string ConvertToRelativePath(string fullPath)
    {
        if (!Application.isEditor)
        {
            return string.Empty;
        }
        string projectPath = Application.dataPath;
        Uri projectUri = new Uri(projectPath + "/");
        Uri fileUri = new Uri(fullPath);
        Uri relativeUri = projectUri.MakeRelativeUri(fileUri);
        string relativePath = "Assets/" + Uri.UnescapeDataString(relativeUri.ToString());

        return relativePath;
    }

}
