using UnityEngine;
using UnityEngine.XR;

public class VR_PerformanceOptimizer : MonoBehaviour
{
    [Header("XR Resolution Scale (0.5 - 1.0)")]
    [Range(0.5f, 1.0f)]
    public float xrResolutionScale = 0.8f;

    [Header("Anti-Aliasing (0, 2, 4)")]
    public int antiAliasingLevel = 2;

    [Header("Shadow Settings")]
    public int shadowResolution = 512;
    public ShadowProjection shadowProjection = ShadowProjection.CloseFit;
    public ShadowQuality shadowQuality = ShadowQuality.All;

    void Start()
    {
        ApplyXRSettings();
        ApplyQualitySettings();
    }

    void ApplyXRSettings()
    {
        // Baisser la résolution VR (XR)
        XRSettings.renderViewportScale = xrResolutionScale;

        Debug.Log("[VR Optimizer] XR Resolution scale set to: " + xrResolutionScale);
    }

    void ApplyQualitySettings()
    {
        // Anti-aliasing global
        QualitySettings.antiAliasing = antiAliasingLevel;

        // Réduction des ombres
        QualitySettings.shadowResolution = (ShadowResolution)shadowResolution;
        QualitySettings.shadowProjection = shadowProjection;
        QualitySettings.shadows = shadowQuality;

        Debug.Log("[VR Optimizer] Quality settings applied.");
    }
}