using System;
using UnityEngine;
using UnityEngine.UI; // Unity UI ���
using UnityStandardAssets.Utility;

public class FPSCounter : MonoBehaviour
{
    const float fpsMeasurePeriod = 0.5f;
    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    private int m_CurrentFps;
    const string display = "{0} FPS";
    public Text fpsText; // Unity UI Text

    private void Start()
    {
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    }

    private void Update()
    {
        m_FpsAccumulator++;

        if (Time.realtimeSinceStartup > m_FpsNextPeriod)
        {
            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
            m_FpsAccumulator = 0;
            m_FpsNextPeriod += fpsMeasurePeriod;

            if (fpsText != null)
            {
                fpsText.text = string.Format(display, m_CurrentFps);
            }
        }
    }
}
