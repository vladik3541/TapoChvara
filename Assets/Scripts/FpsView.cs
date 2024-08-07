using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsView : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public int fpsWidth = 60;
    private float deltaTime;
    private void Awake()
    {
        Application.targetFrameRate = fpsWidth;
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);
    }
}
