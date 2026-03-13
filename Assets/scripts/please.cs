using UnityEngine;

public class please : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
