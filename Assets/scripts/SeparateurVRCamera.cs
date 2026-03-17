using UnityEngine;

public class SeparateurVRCamera : MonoBehaviour
{
    void OnEnable()
    {
        // Empêche cette caméra d'être utilisée pour le rendu VR
        Camera cam = GetComponent<Camera>();
        if (cam != null)
            cam.stereoTargetEye = StereoTargetEyeMask.None;
    }
}