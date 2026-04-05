using System;
using UnityEngine;

public class IsPoseCorrect : MonoBehaviour
{
    public bool isPoseCorrect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Console.WriteLine("Is the pose correct? " + isPoseCorrect);
    }

    public void posecorrect()
            {
        isPoseCorrect = true;
        // Code pour vérifier si la pose est correcte
    }

    public void poseincorrect()
    {
        isPoseCorrect = false;
        // Code pour vérifier si la pose est incorrecte
    }

}
