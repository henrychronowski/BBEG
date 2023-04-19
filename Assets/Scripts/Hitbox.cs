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
    private MeshRenderer mesh;
    private Rigidbody rgd;
    private Attack attack;
    private Collider collider;
    public bool isActive;
    public AttackType type; // unused for now
    public float projectileSpeed;
    float timeElapsed;
    public float radius;

    List<Character> charactersHit;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Character") && isActive)
        {
            Debug.Log("Hit " + collision.gameObject.name);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Character") && isActive)
    //    {
    //        if(PlayerCharacterManager.HitboxAllegianceCheck(owner.gameObject, other.gameObject))
    //        {
    //            Debug.Log("Character " + owner.name + " Hit " + other.gameObject.name);
    //            other.gameObject.GetComponent<Character>().Hit(attack);
    //        }
    //    }
    //}

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
            
            Debug.Log("Character " + owner.name + " Hit " + hit.gameObject.name);
            hit.gameObject.GetComponent<Character>().Hit(attack);
            charactersHit.Add(hit.gameObject.GetComponent<Character>());

        }
    }

    public void StartupPhase()
    {
        
        // Melee
        if (type == AttackType.LightMelee || type == AttackType.HeavyMelee)
        {

        }
        else // Ranged
        {
            //rgd.velocity = owner.facing * projectileSpeed;
        }
    }

    public void ActivePhase()
    {
        mesh.enabled = true;
        isActive = true;
        collider.enabled = true;
        // Melee
        if (type == AttackType.LightMelee || type == AttackType.HeavyMelee)
        {

        }
        else // Ranged
        {
            rgd.velocity = owner.facing * projectileSpeed;
        }
    }
    public void CooldownPhase()
    {
        // Melee
        if (type == AttackType.LightMelee || type == AttackType.HeavyMelee)
        {
            isActive = false;
            mesh.enabled = false;
            collider.enabled = false;
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
        transform.position = c.transform.position + (c.facing * atk.spawnOffset);
    }

    void CheckProjectileLifetime()
    {
        if (type == AttackType.LightRanged || type == AttackType.HeavyRanged)
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
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<SphereCollider>();
        charactersHit = new List<Character>();
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
