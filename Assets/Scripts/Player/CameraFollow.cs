using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;

    void Start()
    {
        // Ana Kamera Ayarlar�
        mainCamera.depth = 0;
        mainCamera.clearFlags = CameraClearFlags.Skybox;

        // �kinci Kamera Ayarlar�
        secondaryCamera.depth = 1;
        secondaryCamera.clearFlags = CameraClearFlags.Depth;

        // �kinci Kameran�n G�r�nt� Alan�
        secondaryCamera.rect = new Rect(0.75f, 0.0f, 0.25f, 0.25f);
    }
}
