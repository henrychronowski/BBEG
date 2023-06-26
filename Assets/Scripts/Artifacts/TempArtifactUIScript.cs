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

    
    void AddCount(ArtifactBase artifact)
    {
        switch(artifact.behavior)
        {
            case (ArtifactBehaviorType.HealUponNewMinion):
                {
                    SquimbCount++;
                    break;
                }
            case (ArtifactBehaviorType.LowHPAttackBoost):
                {
                    MarCount++;
                    break;
                }
            case (ArtifactBehaviorType.None):
                {
                    WaltCount++;
                    break;
                }
        }
    }

    void SubCount(ArtifactBase artifact)
    {
        switch (artifact.behavior)
        {
            case (ArtifactBehaviorType.HealUponNewMinion):
                {
                    SquimbCount--;
                    break;
                }
            case (ArtifactBehaviorType.LowHPAttackBoost):
                {
                    MarCount--;
                    break;
                }
            case (ArtifactBehaviorType.None):
                {
                    WaltCount--;
                    break;
                }
        }
    }
}
