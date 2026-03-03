using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Nb d'écrans détectés : " + Display.displays.Length);

        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate(); // Active Display 2
        }
    }
}