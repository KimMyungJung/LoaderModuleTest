using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class AssetLoader : MonoBehaviour
{

    [field: SerializeField]
    public LoaderModule LoaderModule { get; set; }

    private void Start()
    {
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        Load(selectedAssetName);
    }

    public void Load(string assetName)
    {
        LoaderModule.OnLoadCompleted += OnLoadCompleted;
        LoaderModule.LoadAsset(assetName);
    }

    private void OnLoadCompleted(GameObject loadedAsset)
    {
        loadedAsset.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
