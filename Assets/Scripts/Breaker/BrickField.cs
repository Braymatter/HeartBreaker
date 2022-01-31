using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class BrickField : MonoBehaviour
{
    private List<GameObject> FieldData;

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
    public List<GameObject> SpawnBrickFieldFromData(String data)
    {
        var rows = data.Split(Environment.NewLine);
        var bricks = new List<GameObject>();


        for (var y = 0; y < rows.Length; y++)
        {
            var columns = rows[y].Split(",");
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
                        bricks.Add(newBrickPrefab);

                        BreakerBrick brick = newBrickPrefab.GetComponent<BreakerBrick>();
                        brick.parent = this;
                        brick.FieldPosition = new Vector2(y, x);

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
            for(var i = FieldData.Count - 1; i >= 0; i--)
            {
                var go = FieldData[i];
                if (go != null)
                {
                    Destroy(go);
                    FieldData.Remove(go);
                }
            }
        }
    }

    public void DeleteBrick(BreakerBrick brick)
    {
        Destroy(brick.gameObject);
        var removed = FieldData.Remove(brick.gameObject);
        if (!removed)
        {
            Debug.Log("Could Not Remove Brick from BrickField FieldData");
        }
        brick = null;
    }
    
    public bool IsEmpty()
    {
        var isEmpty = true;

        if (FieldData != null)
        {
            if (FieldData.Any(brick => brick != null))
            {
                isEmpty = false;
            }
        }
        return isEmpty;
    }
    // Start is called before the first frame update
    [Button("Reload")]
    public void SpawnField()
    {
        ClearField();
        FieldData = SpawnBrickFieldFromData(LoadFieldDataFromFile(levelName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
