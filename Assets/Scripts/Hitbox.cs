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
    public bool isActive;
    public AttackType type; // unused for now
    public float projectileSpeed;
    public float playerSpawnOffset;
    float timeElapsed;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Character") && isActive)
        {
            Debug.Log("Hit " + collision.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && isActive)
        {
            Debug.Log("Hit " + other.gameObject.name);
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
            mesh.enabled = false;
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
        transform.position = c.transform.position + c.facing;
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
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        CheckProjectileLifetime();
    }

}
