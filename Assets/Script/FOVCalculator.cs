using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVCalculator
{
    public enum FOV_TRANSLATE_TYPE
    {
        Horizontal,
        Vertical
    };

    public static float CalculateFov(float fov, Vector2 screenSize, FOV_TRANSLATE_TYPE ft_type = FOV_TRANSLATE_TYPE.Vertical)
    {
        float fov_result;
        float radian = 180.0f / Mathf.PI;
        float screenRatio = (screenSize.x / screenSize.y);
        switch (ft_type)
        {
            case FOV_TRANSLATE_TYPE.Vertical: //get v
                fov_result = 2.0f * Mathf.Atan(Mathf.Tan((fov / 2.0f) / radian) / screenRatio) * radian;
                break;
            case FOV_TRANSLATE_TYPE.Horizontal: //get h
                fov_result = 2.0f * Mathf.Atan(Mathf.Tan(fov / 2.0f) * screenRatio) * radian;
                break;
            default:
                fov_result = fov;
                break;
        }
        return fov_result;
    }
}
