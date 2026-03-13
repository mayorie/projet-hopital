using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class choose_target_items : MonoBehaviour
{
    // Ce script est attaché à un objet de la scène, et a pour but de trouver des objets avec lesquels le joueur doit interagir, en fonction de la difficulté choisie.


    private GameObject player;  // on attache ce sript à lobjet joueur, pou pouvoir calculer des distances entre le joueur et les objets de la scène.

    [Tooltip("L'objet en cours avec lequel on doit interagir.")]
    [SerializeField] private GameObject targetItem;

    [Tooltip("Le numéro de difficulté visé. 1-3: regarder. 4-6: pointer. 7-9: prendre")] 
    [SerializeField] private int targetDifficulty;


    // les valeurs suivantes sont sujettes à changement pour équilibrage.
    // Elles permettentent de convertir les distances en numéro de difficulté.

    public float grab_distance_cap = 1.5f; // la distance maximale à laquelle le joueur peut prendre un objet.


    public void set_target_diffiulty(int difficulty)
    {
        // pour l'instant, j'uilise cette fonction pour passer moi-même des valeurs de difficulté.
        // Quand les codes sont mis en commun, il faudra utiliser une valeur contenue dans un manager qui indique la difficulté choisie dans l'écran précédent par le.a soignant.e.
        targetDifficulty = difficulty;
    }

    public void find_target_item()
    {
        // Trouver un objet dans la scène avec le tag "interactable", et qui correspond à la difficulté choisie.
        GameObject[] interactableItems = GameObject.FindGameObjectsWithTag("interactable");

        List< GameObject > targetableItems = new List<GameObject>(); // on va stocker les objets qui sont à la bonne distance du joueur dans cette liste.

        foreach (GameObject item in interactableItems)
        {
            float distance = Vector3.Distance(player.transform.position, item.transform.position);

            if (targetDifficulty <= 3)  // Si on doit juste regarder l'objet
            {
                // On pourra ajouter d'autres conditions de sélection plus tard, pour affiner la précision des difficultés.
                if (distance <= grab_distance_cap * 2)  // on peut regarder un objet à une distance plus grande que celle à laquelle on peut le prendre.
                { 
                    // on ajoute l'objet à la liste des objets cibles.
                    targetableItems.Add(item);
                }
            }

            else if (targetDifficulty <= 6)  // Si on doit pointer l'objet
                {
                // On pourra ajouter d'autres conditions de sélection plus tard, pour affiner la précision des difficultés.
                if (distance <= grab_distance_cap * 1.5f)  // on peut pointer un objet à une distance plus grande que celle à laquelle on peut le prendre, mais plus petite que celle à laquelle on peut le regarder.
                { 
                    // on ajoute l'objet à la liste des objets cibles.
                    targetableItems.Add(item);
                }
            }

            else if (targetDifficulty <= 9)  // Si on doit attrapper l'objet
            {
                // On pourra ajouter d'autres conditions de sélection plus tard, pour affiner la précision des difficultés.
                if (distance <= grab_distance_cap)
                { 
                    // on ajoute l'objet à la liste des objets cibles.
                    targetableItems.Add(item);
                } 
            }


        }
    }
}
