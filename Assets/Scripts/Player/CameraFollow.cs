using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;

    void Start()
    {
        // Ana Kamera Ayarlarý
        mainCamera.depth = 0;
        mainCamera.clearFlags = CameraClearFlags.Skybox;

        // Ýkinci Kamera Ayarlarý
        secondaryCamera.depth = 1;
        secondaryCamera.clearFlags = CameraClearFlags.Depth;

        // Ýkinci Kameranýn Görüntü Alaný
        secondaryCamera.rect = new Rect(0.75f, 0.0f, 0.25f, 0.25f);
    }
}
