// Created by Lee Fischer
// 5/3/25

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVSaveAdapter : ISaveSystem
{
    // Variables
    // This creates the file path for saving data.
    private readonly string filePath = Path.Combine(Application.persistentDataPath, "PlayerData.csv");

    // Methods
    public void SavePlayerData(PlayerData data)
    {
        try
        {
            string csv = data.position.x.ToString() + ", " + data.position.y.ToString() + "\n";

            File.WriteAllText(filePath, csv);
            Debug.Log("CSV Saved! " + csv.TrimEnd());
        }
        catch(IOException e)
        {
            Debug.Log("Error! CSV Save Failed!.." + e.ToString());
        }
    }
    public PlayerData LoadPlayerData()
    {
        PlayerData data = new PlayerData { position = Vector2.zero };

        try
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                if(lines.Length > 0)
                {
                    string[] values = lines[lines.Length - 1].Split(',');

                    if(values.Length == 2 && float.TryParse(values[0], out float x) && float.TryParse(values[1],out float y))
                    {
                        data.position = new Vector2(x, y);

                        Debug.Log("CSV Loaded!");
                    }
                    else
                    {
                        Debug.Log("Invalid CSV Detected!...");
                    }
                }
            }
        }
        catch(IOException e)
        {
            Debug.Log("Error Loading!..." + e.ToString());
        }
        return data;
    }
}
