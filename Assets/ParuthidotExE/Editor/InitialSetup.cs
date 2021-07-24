// ----------------------------------------------------------------
// Create Folder Structure
// ----------------------------------------------------------------
// Author : @ParuthidotExE
// 
// Notes : 
// Creates folders structure for the project to kick start  
//
// ~~~~
// TODO
// ~~~~
//
// Create folders from String, csv, json
// ParuthidotExE
// ParuthidotExE//Artz
// ParuthidotExE//Artz//Shaders
//
// Save to String, csv, json
//
// ----------------------------------------------------------------

using UnityEngine;
using UnityEditor;
using System.IO;

namespace PLabs
{
    public class InitialSetup : Editor
    {
        /// <summary>
        /// 
        /// Creates folders structure for the unity project
        /// 
        /// </summary>
        [MenuItem("ParuthidotExE/Init/Folder Structure")]
        static void CreateFolderStructure()
        {
            CreateFolderStructure(false);
        }

        /// <summary>
        /// 
        /// Creates folders structure with a readme files
        /// 
        /// </summary>
        [MenuItem("ParuthidotExE/Init/Folders + ReadMe")]
        static void CreateReadMe()
        {
            CreateFolderStructure(true);
        }


        /// <summary>
        ///
        /// Creates scene structure
        ///
        /// </summary>
        [MenuItem("ParuthidotExE/Scene/Scene Structure")]
        static void CreateSceneStructure()
        {
            string[] segments = new string[] { "UI", "Environment", "Gameplay", "Managers" };
            for (int i = 0; i < segments.Length; i++)
            {
                segments[i] = "-------- " + segments[i] + " --------";
                GameObject newObj = new GameObject(segments[i]);
                newObj.transform.position = Vector3.zero;
            }
        }


        /// <summary>
        /// Creates folders structure with/without a readme files
        /// </summary>
        /// <param name="isReadMe">option to create ReadMe.txt files</param>
        static void CreateFolderStructure(bool isReadMe)
        {
            string cwd = "Assets";
            string assetsFolder = "Assets";
            string parentFolder = "ParuthidotExE";
            string[] childFolders = new string[] { "Audios", "Artz", "Editor", "Fonts", "Scenes", "Scripts", "CineMachines", "Timelines", "InputSystems", "VisualScripts", "ScriptableObjects" };
            string[] artFolders = new string[] { "ShaderGraphs", "Models", "Materials", "Textures", "Animations", "UI", "Sprites", "VFXs", "Animators", "UI_ToolKit", "PostProcessing" };
            string[] audioFolders = new string[] { "SFXs", "Musics", "AudioMixers" };

            string guid = "";
            string curFolderPath = "";

            cwd = assetsFolder;
            if (!AssetDatabase.IsValidFolder($"{cwd}/{parentFolder}"))
            {
                guid = AssetDatabase.CreateFolder(cwd, parentFolder);
                curFolderPath = AssetDatabase.GUIDToAssetPath(guid);
            }

            cwd = $"{assetsFolder}/{parentFolder}";
            foreach (string folder in childFolders)
            {
                if (AssetDatabase.IsValidFolder($"{cwd}/{folder}"))
                    continue;
                guid = AssetDatabase.CreateFolder($"{cwd}", folder);
                curFolderPath = AssetDatabase.GUIDToAssetPath(guid);
            }

            cwd = $"{assetsFolder}/{parentFolder}/Artz";
            foreach (string folder in artFolders)
            {
                if (AssetDatabase.IsValidFolder($"{cwd}/{folder}"))
                    continue;
                guid = AssetDatabase.CreateFolder($"{cwd}", folder);
                curFolderPath = AssetDatabase.GUIDToAssetPath(guid);

                if (isReadMe)
                {
                    CreateReadMeFiles($"{cwd}/{folder}", $"This folder contains files related to {folder}.\n ");
                }

                //TextAsset readme = new TextAsset($"This folder contains files related to {folder}");
                //AssetDatabase.CreateAsset(readme, $"{cwd}/{folder}/Readme.txt");
            }

            cwd = $"{assetsFolder}/{parentFolder}/Audios";
            foreach (string folder in audioFolders)
            {
                if (AssetDatabase.IsValidFolder($"{cwd}/{folder}"))
                    continue;
                guid = AssetDatabase.CreateFolder($"{cwd}", folder);
                curFolderPath = AssetDatabase.GUIDToAssetPath(guid);
            }
        }

        /// <summary>
        /// Created ReadMe.txt file 
        /// Notes: 
        /// Some times it takes long to create files
        /// Minimize and Open Unity to get files reflected
        /// </summary>
        /// <param name="filePath">Path to save file</param>
        /// <param name="contents">Contents of the file</param>
        static void CreateReadMeFiles(string filePath, string contents)
        {
            filePath = filePath + "/zReadme.txt";
            StreamWriter writer = new StreamWriter(filePath, false);
            writer.WriteLine("ReadMe\n~~~~~~\n\n" + contents);
            writer.Close();
        }


    }


}

