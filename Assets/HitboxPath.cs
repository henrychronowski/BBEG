using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxPath : MonoBehaviour
{
    Vector3 origin;

    [SerializeField] float horizontalOffSet;
    Hitbox thisHitbox;
    [SerializeField] bool reverseDirection;
    [SerializeField] float speed;
    [SerializeField] float progress;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        thisHitbox = GetComponent<Hitbox>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = origin + (-thisHitbox.owner.transform.forward * Mathf.Pow(progress, 2)) 
            + (-thisHitbox.owner.transform.right * horizontalOffSet * progress);
        progress += Time.deltaTime * speed;
    }
}
