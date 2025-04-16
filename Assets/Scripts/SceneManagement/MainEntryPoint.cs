using System;
using Global.SaveSystem;
using UnityEngine;

namespace SceneManagement
{
    public class MainEntryPoint : MonoBehaviour
    {
        private const string COMMON_OBJECT_TAG = "CommonObject";

        public void Awake()
        {
            if (GameObject.FindGameObjectWithTag(COMMON_OBJECT_TAG)) return;
            
            var commonObjectPrefab = Resources.Load<CommonObject>("CommonObject");
            var commonObject = Instantiate(commonObjectPrefab);
            DontDestroyOnLoad(commonObject);
            
            commonObject.SaveSystem = new();

            commonObject.SceneLoader.LoadMetaScene();
        }
    }
}