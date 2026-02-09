using UnityEngine;

public class cone_move_and_watch : MonoBehaviour
{
    // This is the camera that will be followed by this object.
    [SerializeField] private GameObject followedCamera;

    // this object is the current target. This object tracks for how long we've been watching the object
    public GameObject Target;
    [SerializeField] private float timeWatchingTarget;

    private Collider coneCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeWatchingTarget = 0;
        coneCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if this object collides with the target.
        if (coneCollider.bounds.Intersects(Target.GetComponent<Collider>().bounds))
        {
            timeWatchingTarget += Time.deltaTime;
            return;
        }

        // Sujet à changement: si on ne regarde pas l'objet visé, la jauge baisse plutôt que de se remettre à 0
        timeWatchingTarget -= Time.deltaTime;
        if (timeWatchingTarget < 0) { timeWatchingTarget = 0; }
        return;
    }

    private void FixedUpdate()
    {
        // suivre les mouvements de la caméra
        transform.position = followedCamera.transform.position;
        transform.rotation = followedCamera.transform.rotation * Quaternion.Euler(0, -90, 0);
    }

    // when the target needs to change, this method is called
    public void SwitchTarget(GameObject target)
    {
        timeWatchingTarget = 0;
        Target = target;
    }
}
