using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BrickField : MonoBehaviour
{
    private List<List<GameObject>> FieldData;

    public String levelName;
    
    public Vector2 brickSize = Vector2.one;

    public String LoadFieldDataFromFile(String path)
    {
        TextAsset fileText = Resources.Load<TextAsset>("BrickBreakerLevels/" + path);
        if (fileText == null)
        {
            Debug.LogError("Could not load level: " + path);
        }

        return fileText.text;
    }
    
    //String is a csv 
    public List<List<GameObject>> SpawnBrickFieldFromData(String data)
    {
        var rows = data.Split(Environment.NewLine);
        var bricks = new List<List<GameObject>>();


        for (var y = 0; y < rows.Length; y++)
        {
            var columns = rows[y].Split(",");
            bricks.Add(new List<GameObject>(columns.Length));
            for (var x = 0; x < columns.Length; x++)
            {
                //Generate the brick from the data here
                if (columns[x] != "0")
                {
                    String path = null;
                    switch (columns[x])
                    {
                        case "1":
                            path = "BreakerBrick";
                            break;
                        case "2":
                            path = "ArmoredBrick";
                            break;
                        case "3":
                            path = "ExplosionBrick";
                            break;
                        case "4":
                            path = "ShrinkerBrick";
                            break;
                        default:
                            break;
                    }

                    if (path != null)
                    {
                        var newBrickPrefab = (GameObject) Instantiate(Resources.Load(path), transform);
                        newBrickPrefab.transform.Translate(new Vector3(brickSize.x * x, brickSize.y * -y, 0 ));
                        bricks[y].Add(newBrickPrefab);
                    }
                }
            }
        }

        return bricks;
    }
    
    [Button("Clear Field")]
    public void ClearField()
    {
        if (FieldData != null)
        {
            foreach (var row in FieldData)
            {
                foreach (var go in row)
                {
                    if (go != null)
                    {
                        Destroy(go);
                    }
                }
            }
        }
    }
    
    // Start is called before the first frame update
    [Button("Reload")]
    void Start()
    {
        ClearField();
        FieldData = SpawnBrickFieldFromData(LoadFieldDataFromFile(levelName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
