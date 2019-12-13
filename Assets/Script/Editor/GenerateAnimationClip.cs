using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class GenerateAnimationClip
{
    // Start is called before the first frame update
    public static void Generate(List<Vector2> vec2List, string relatePath, System.Type stype, string propertyName, string originPath, float frameRate = 30f)
    {
        var clip = new AnimationClip()
        {
            name = "camAnim",
            legacy = false,
            wrapMode = WrapMode.Once,
        };
        AnimationUtility.SetGenerateMotionCurves(clip, true);

        var curve = new AnimationCurve();
        foreach (Vector2 v in vec2List)
        {
            curve.AddKey(new Keyframe(v.x / frameRate, v.y));
        }
        clip.SetCurve(relatePath, stype, propertyName, curve);

        string filePath = originPath;

        AssetDatabase.CreateAsset(clip, filePath);
        AssetDatabase.SaveAssets();

        Debug.Log(filePath);
    }
}
