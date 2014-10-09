using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;


[Serializable]
public class SceneSetting
{
    public string sceneName;
    public int minViewId;
}

public class PunSceneSettings : ScriptableObject
{
    [SerializeField]
    public List<SceneSetting> MinViewIdPerScene = new List<SceneSetting>();

    private static PunSceneSettings instanceField;
    public static PunSceneSettings Instance
    {
        get
        {
            if (instanceField != null)
            {
                return instanceField;
            }

            instanceField = (PunSceneSettings)AssetDatabase.LoadAssetAtPath(SceneSettingsFilePath, typeof(PunSceneSettings));
            if (instanceField == null)
            {
                //Debug.LogWarning("Creating new PunSceneSettings!!");
                instanceField = ScriptableObject.CreateInstance<PunSceneSettings>();
                AssetDatabase.CreateAsset(instanceField, SceneSettingsFilePath);
            }

            //Debug.Log("Instance: " + instanceField);
            return instanceField;
        }
    }
    
    public static readonly string SceneSettingsFilePath = "Assets/Photon Unity Networking/Editor/PhotonNetwork/PunSceneSettingsFile.asset";


    public static int MinViewIdForScene(string scene)
    {
        if (string.IsNullOrEmpty(scene))
        {
            return 0;
        }

        PunSceneSettings pss = Instance;
        if (pss == null)
        {
            Debug.LogError("pss cant be null");
            return 0;
        }

        foreach (SceneSetting setting in pss.MinViewIdPerScene)
        {
            if (setting.sceneName.Equals(scene))
            {
                return setting.minViewId;
            }
        }
        return 0;
    }
}