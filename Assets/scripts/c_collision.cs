using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_collision : MonoBehaviour
{
    public float collisionprogress = 0;
    private bool isColliding = false;
    public float timeobjectiveinsec = 1.5f;

    [SerializeField] FloatingHealthBar HealthBar;
    [SerializeField] GameObject gameObjectName;
    // Déclaration de l'événement
    public event Action OnCollisionProgressMax;

    private void Awake()
    {
        HealthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    void Start()
    {
        Debug.Log("Script c_collision démarré sur " + gameObject.name);
        StartCoroutine(CollisionProgressRoutine());
        HealthBar.UpdateHealthBar(collisionprogress, timeobjectiveinsec);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == gameObjectName.name) // Assigner le nom de l'objet à celui défini dans l'inspecteur
        {
            Debug.Log(gameObject.name + " a détecté une collision avec " + other.gameObject.name);
            isColliding = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == gameObjectName.name) // Assigner le nom de l'objet à celui défini dans l'inspecteur
        {
            Debug.Log(gameObject.name + " est en collision continue avec " + other.gameObject.name);
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == gameObjectName.name) // Assigner le nom de l'objet à celui défini dans l'inspecteur
        {
            Debug.Log(gameObject.name + " a quitté la collision avec " + other.gameObject.name);
            isColliding = false;
        }
    }

    private IEnumerator CollisionProgressRoutine()
    {
        while (true)
        {
            if (isColliding && collisionprogress < timeobjectiveinsec)
            {
                collisionprogress += 0.1f;
                Debug.Log("Progression de la collision : " + collisionprogress + "%");
                HealthBar.UpdateHealthBar(collisionprogress, timeobjectiveinsec);
                if (collisionprogress == timeobjectiveinsec)
                {
                    Debug.Log("Signal : collisionprogress atteint " + timeobjectiveinsec + " !");
                    OnCollisionProgressMax?.Invoke();
                }
                yield return new WaitForSeconds(0.1f);
            }
            else if (!isColliding && collisionprogress > 0)
            {
                yield return new WaitForSeconds(1f);
                while (!isColliding && collisionprogress > 0)
                {
                    collisionprogress -= 0.1f;
                    Debug.Log("Régression de la collision : " + collisionprogress + "%");
                    yield return new WaitForSeconds(0.1f);
                    HealthBar.UpdateHealthBar(collisionprogress, timeobjectiveinsec);
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}
