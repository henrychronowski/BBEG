using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempArtifactUIScript : ArtifactBase
{
    //This will only be used for testing purposes!
    //Will delete this once we have better artifact UI
    int SquimbCount = 0;
    int MarCount = 0;
    int WaltCount = 0;

    public Sprite SquimbSprite;

    
    void AddCount(ArtifactBase artifact)
    {
        switch(artifact.uiSprite)
        {
            //case (SquimbSprite):
                //{
                //    SquimbCount++;
                //    break;
                //}
        }
    }

    void SubCount(ArtifactBase artifact)
    {

    }
}
