using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Udp3dsMax : MonoBehaviour
{
    public float Fov;
    [SerializeField]
    private float u3dFov;
    public Vector2 size;
    private Camera camera = null;
    public Camera TargetCamera {
        get { 
            if (camera == null)
                camera = this.GetComponent<Camera>();
            return camera;
        }
    }

    private void Start()
    {
    }

    private void LateUpdate()
    {
        UpdateFov(Fov);
    }

    void UpdateFov(float f)
    {
        size = new Vector2(Screen.width, Screen.height);
        u3dFov = FOVCalculator.CalculateFov(f, size);
        TargetCamera.fieldOfView = u3dFov;
    }


}
