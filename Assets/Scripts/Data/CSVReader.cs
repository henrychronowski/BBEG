using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static RedScarf.EasyCSV.CsvHelper;

public class ArtifactData
{
    public string Name;
    public string Description;
    public int healthBuff;
}

public class CSVReader : MonoBehaviour
{
    //private void ReadCsvFile()
    //{
    //    // Path to your CSV file
    //    string filePath = "CSV/test.csv";

    //    // Create a reader for the CSV file
    //    using (var reader = new StreamReader(filePath))
    //    using (var csv = new CSVReader())
    //    {
            
    //        // Read the CSV records into a list
    //        var records = csv.GetRecords<ArtifactData>();
            

    //        // Loop through the records and extract the variables
    //        foreach (var record in records)
    //        {
    //            string variableName = record.VariableName;

    //            // Use the variableName as needed in your script
    //            // For example, you can assign it to a variable, display it in the console, etc.
    //            Debug.Log("Variable Name: " + variableName);
    //        }
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
