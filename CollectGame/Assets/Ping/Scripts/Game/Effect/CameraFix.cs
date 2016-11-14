using UnityEngine;
using System.Collections;

public class CameraFix : MonoBehaviour
{

    public float _default = 3.6f;
    void Awake()
    {
        Camera _camera = GetComponent<Camera>();
        float currentRatio = (float)Screen.width / Screen.height;
        float newCameraSize = _default / currentRatio;
        _camera.orthographicSize = newCameraSize;
    }
}
