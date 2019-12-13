using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FbxPostprocessor : AssetPostprocessor
{
    private string fbxValue = "UDP3DSMAX";
    private static string path;

    [MenuItem("Assets/export fbx2fov")]
    public static void Create()
    {
        var processor = new FbxPostprocessor();
        path = AssetDatabase.GetAssetPath(Selection.activeObject);
        processor.assetPath = path;
        Debug.Log(path);
        AssetDatabase.ImportAsset(processor.assetPath, ImportAssetOptions.ForceUpdate);
    }

    [MenuItem("Assets/export fbx2fov", true)]
    static bool ValidateSelectedFbx()
    {
        string file_path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return file_path.ToLower().Contains(".fbx");
    }

    void OnPostprocessGameObjectWithUserProperties(GameObject go, string[] propNames, System.Object[] values)
    {
        List<Vector2> result = ParseUDP3DSMAXList(go, propNames, values);
        GenerateAnimationClip.Generate(result, "Camera", typeof(Udp3dsMax), "Fov", path.ToLower().Replace(".fbx", ".anim"), 30f);
    }

    public List<Vector2> ParseUDP3DSMAXList(GameObject go, string[] names, System.Object[] values)
    {
        List<Vector2> fovList = new List<Vector2>();

        string FileName_path = assetPath.Replace(Path.GetFileName(assetPath), "");

        for (int i = 0; i < names.Length; i++)
        {
            string propertyName = names[i];
            object propertyValue = values[i];

            if (0 == propertyName.CompareTo(fbxValue))
            {
                string[] arr = propertyValue.ToString().Split(',');
                for (int j = 0; j < arr.Length; j++)
                {
                    string[] fov = arr[j].Replace("f", "").Split(':');
                    if (fov.Length > 1 && fov[1] != null)
                    {
                        Vector2 v = new Vector2(float.Parse(fov[0]), float.Parse(fov[1]));
                        fovList.Add(v);
                    }
                }
            }
        }
        return fovList;
    }
}
