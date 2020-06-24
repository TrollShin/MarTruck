using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSettingFunctionLibrary : MonoBehaviour
{
    public GameObject Content;

    public void OnClickGraphic()
    {
        /*
        GameObject GraphicOptions = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/4Prefabs/UI/Setting/GraphicOptions.prefab");
        Content.transform.DetachChildren();
        GraphicOptions.transform.SetParent(Content.transform);
        */
    }

    public void OnClickControl()
    {

    }

    public void OnClickGameplay()
    {

    }

    public void OnClickExit()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
