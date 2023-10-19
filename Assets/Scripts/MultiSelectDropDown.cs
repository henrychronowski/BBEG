using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSelectDropDown : MonoBehaviour
{
    [System.Flags]
    public enum MyOptions
    {
        Option1 = 1,
        Option2 = 2,
        Option3 = 4,
        Option4 = 8
    }

    public MyOptions selectedOptions;

    // Use this method to convert the selected options to a readable string
    public string GetSelectedOptionsString()
    {
        return selectedOptions.ToString();
    }
}
