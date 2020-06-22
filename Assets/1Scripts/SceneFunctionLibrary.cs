using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._1Scripts
{
    public class SceneFunctionLibrary
    {
        public static void LoadTitle()
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }

        /**
         * Remaining Current Scene, Current Scene must have a Camera for overlay target.
         */
        public static void ShowSettingMenu()
        {
            SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Setting")); // Must be Active Scene for instantiate prefabs
        }
    }
}
