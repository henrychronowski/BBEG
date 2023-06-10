using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackPhase
{
    Startup,
    Active,
    Cooldown
}

public class Hitbox : MonoBehaviour
{
    private Character owner;
    [SerializeField] private MeshRenderer mesh;
    private Rigidbody rgd;
    private Attack attack;
    private Collider collider;
    public bool isActive;
    public AttackType type; // unused for now
    public float projectileSpeed;
    float timeElapsed;
    public float radius;
    public int piercingPower;

    List<Character> charactersHit;

    // Debug purposes
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Character") && isActive)
        {
            Debug.Log("Hit " + collision.gameObject.name);
        }
    }

    // Only works with spherical hitboxes right now
    void CollisionCheck()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider hit in hits)
        {
            if (hit.gameObject.layer != LayerMask.NameToLayer("Character"))
            {
                continue;
            }

            if (!PlayerCharacterManager.HitboxAllegianceCheck(owner.gameObject, hit.gameObject))
            {
                continue;
            }
            
            if (charactersHit.Contains(hit.GetComponent<Character>()))
            {
                continue;
            }

            Character recipient = hit.gameObject.GetComponent<Character>();
            Debug.Log("Character " + owner.name + " Hit " + hit.gameObject.name);
            charactersHit.Add(recipient);
            HitData data = new HitData(owner, recipient, attack, attack.damage);
            EventManager.instance.AttackConnected(data);
            if (charactersHit.Count > piercingPower)
            {
                Destroy(gameObject);
            }

        }
    }



    public void StartupPhase()
    {
        
        // Melee
        if (type == AttackType.Melee)
        {

        }
        else // Ranged
        {
            //rgd.velocity = owner.transform.forward * projectileSpeed;
        }
    }

    public void ActivePhase()
    {
        mesh.enabled = true;
        isActive = true;
        collider.enabled = true;
        // Melee
        if (type == AttackType.Melee)
        {

        }
        else // Ranged
        {
            rgd.velocity = owner.transform.forward * projectileSpeed;
        }
    }
    public void CooldownPhase()
    {
        // Melee
        if (type == AttackType.Melee)
        {
            isActive = false;
            if(mesh)
            {
                mesh.enabled = false;
                collider.enabled = false;
            }
            if(this != null)
                Destroy(gameObject);
        }
        else // Ranged
        {

            //rgd.velocity = owner.facing * projectileSpeed;
        }
    }

    public void Init(Character c, Attack atk)
    {
        owner = c;
        attack = atk;
        transform.position = c.transform.position + (c.transform.forward * atk.spawnOffset);
    }

    void CheckProjectileLifetime()
    {
        if (type == AttackType.Ranged)
        {
            if (attack.projectileLifetime <= timeElapsed)
            {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
        charactersHit = new List<Character>();
        mesh.transform.localScale = Vector3.one * (radius * 2);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        CheckProjectileLifetime();
        if(isActive)
        {
            CollisionCheck();
        }
    }

    private void OnDrawGizmos()
    {
        if (isActive)
            Gizmos.DrawSphere(transform.position, radius);
    }
}
