using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class choose_target_items : MonoBehaviour
{
    // Ce script est attaché à un objet de la scène, et a pour but de trouver des objets avec lesquels le joueur doit interagir, en fonction de la difficulté choisie.


    private GameObject player;  // on attache ce sript à lobjet joueur, pou pouvoir calculer des distances entre le joueur et les objets de la scène.

    [Tooltip("L'objet en cours avec lequel on doit interagir.")]
    public GameObject targetItem;

    [Tooltip("Le numéro de difficulté visé. 1-3: regarder. 4-6: pointer. 7-9: prendre")] 
    [SerializeField] private int targetDifficulty;

    // on stocke les objets avec lesquels le joueur a déjà interagi, pour éviter de les faire réapparaître.
    private List<GameObject> past_interacted = new List<GameObject>();


    // les valeurs suivantes sont sujettes à changement pour équilibrage.
    // Elles permettentent de convertir les distances en numéro de difficulté.
    [Tooltip("A partir de cette distance, on ne peut pas attrapper un objet.")]
    public float grab_distance_cap = 1.5f; // la distance maximale à laquelle le joueur peut prendre un objet.

    [Tooltip("En dessous de cette taille, un objet est difficile à remarquer, et la difficulté augmente.")]
    public float size_difficulty_cap = 0.3f ; // la taille maximale d'un objet pour qu'il soit considéré comme difficile à voir ou à pointer.

    [Tooltip("Au delà de cette distance, un objet est difficile à voir, et la difficulté augmente.")]
    public float distance_difficulty_cap = 5f; // la distance maximale à laquelle un objet peut être pour être considéré comme difficile à voir ou à pointer.


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
            // Si la difficulté d'interaction avec cet objet correspond à la difficulté visée, on l'ajoute à la liste des objets ciblables.
            if (determine_difficulty(targetDifficulty, item) == targetDifficulty && !past_interacted.Contains(item))
            {
                targetableItems.Add(item);
                past_interacted.Add(item); // on ajoute cet objet à la liste des objets déjà utilisés, pour éviter de le faire réapparaître plus tard.

                // Si la liste past_interacted devient longue, on supprime le début de la liste pour éviter de ne plus trouver d'objets avec lesquels interagir.
                if (past_interacted.Count > 3) past_interacted.RemoveAt(0);
            }
        }

        // Si on a trouvé au moins un objet qui correspond à la difficulté visée, on en choisit un au hasard parmi ceux-là.
        if (targetableItems.Count > 0)
        {
            int randomIndex = Random.Range(0, targetableItems.Count);
            targetItem = targetableItems[randomIndex];
            return;
        }
         else
        {
            Debug.Log("Aucun objet ne correspond à la difficulté visée.");
            Debug.Log("On change la difficulté visée pour trouver un objet.");

            // On doit changer la difficulté, mais pas changer de mode de jeu.
            if (targetDifficulty % 3 == 1) targetDifficulty += 2;
            else targetDifficulty--;

            // puis on relance la fonction de recherche.
            // attention: risque de récursivité infinie en cas d'absence d'objet à observer.
            find_target_item();
        }
    }


    /// <summary>
    /// Cette fonction permet de déterminer la difficulté d'interaction avec un objet, en fonction du mode de jeu choisi, de la distance entre le joueur et l'objet et de la taille de l'objet.
    /// </summary>
    /// <param name="gamemode"> Un entier qui peut prendre les valeurs 1, 4 ou 7 selon le mode de jeu lancé </param>
    /// <param name="target"> L'objet dont on va déterminer la difficulté d'interaction. </param>
    /// <returns> Retourne le numéro de difficulté attribué à un seul GameObject, qui dépend de sa taille et de sa position par rapport au joueur. </returns>
    private int determine_difficulty(int gamemode, GameObject target)
    {
        // On commence avec la difficulté de base, qui dépend du mode de jeu choisi. 1 pour regarder, 4 pour pointer, 7 pour attrapper.
        int difficulty = gamemode;

        // on ajoute ensuite des points de difficulté en fonction de la distance entre le joueur et l'objet, et de la taille de l'objet. Plus l'objet est loin ou petit, plus il est difficile à interagir avec.
        if ( Vector3.Distance(player.transform.position, target.transform.position ) > distance_difficulty_cap && gamemode != 7) difficulty += 1;

        if (target.transform.localScale.magnitude < size_difficulty_cap) {
            difficulty += 1; 
        }

        // Comme un objet à attrapper ne peut pas être loin, on prévoit la possibilité qu'il soit très petit et donc difficile à récupérer.
        if (gamemode == 7)
        {
            if (target.transform.localScale.magnitude < size_difficulty_cap / 2) {
                difficulty += 1;
            }
        }

        return difficulty;
    }
}


// TODO: Lier le set_target_difficulty avec un manager qui stocke la difficulté choisie par le.a soignant.e dans l'écran précédent, pour que le script puisse trouver des objets adaptés à la difficulté choisie.
// TODO: Dans les autres codes, trouver où appeler la fonction find_target_item pour que le jeu puisse trouver un objet cible au début de chaque round, et après chaque interaction réussie.