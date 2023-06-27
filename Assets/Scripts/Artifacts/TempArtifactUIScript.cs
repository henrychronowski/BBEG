using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempArtifactUIScript : MonoBehaviour
{
    //This will only be used for testing purposes!
    //Will delete this once we have better artifact UI
    int SquimbCount = 0;
    int MarCount = 0;
    int WaltCount = 0;

    public Text SquimbText;
    public Text MarText;
    public Text WaltText;

    public static TempArtifactUIScript instance;

    public void AddCount(ArtifactBase artifact)
    {
        switch(artifact.behavior)
        {
            case (ArtifactBehaviorType.HealUponNewMinion):
                {
                    SquimbCount++;
                    SquimbText.text = SquimbCount.ToString();
                    break;
                }
            case (ArtifactBehaviorType.LowHPAttackBoost):
                {
                    MarCount++;
                    MarText.text = MarCount.ToString();
                    break;
                }
            case (ArtifactBehaviorType.None):
                {
                    WaltCount++;
                    WaltText.text = WaltCount.ToString();
                    break;
                }
        }
    }

    public void SubCount(ArtifactBase artifact)
    {
        switch (artifact.behavior)
        {
            case (ArtifactBehaviorType.HealUponNewMinion):
                {
                    SquimbCount--;
                    SquimbText.text = SquimbCount.ToString();
                    break;
                }
            case (ArtifactBehaviorType.LowHPAttackBoost):
                {
                    MarCount--;
                    MarText.text = MarCount.ToString();
                    break;
                }
            case (ArtifactBehaviorType.None):
                {
                    WaltCount--;
                    WaltText.text = WaltCount.ToString();
                    break;
                }
        }
    }

    private void Start()
    {
        SquimbText.text = SquimbCount.ToString();
        MarText.text = MarCount.ToString();
        WaltText.text = WaltCount.ToString();
    }
}
