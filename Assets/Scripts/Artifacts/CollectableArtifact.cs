using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableArtifact : MonoBehaviour
{
    [SerializeField] ArtifactBase artifact;
    [SerializeField] RawImage image;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            EventManager.instance.AddArtifact(artifact);
            Destroy(gameObject);
        }
    }

    public void AssignArtifact(ArtifactBase a, Rarity r = Rarity.Common)
    {
        artifact = a;
        artifact.SetRarity(r);
        image.texture = artifact.uiSprite.texture;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(artifact != null)
            image.texture = artifact.uiSprite.texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
