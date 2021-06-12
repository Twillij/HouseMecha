using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float scaleRequirement = 1;
    public List<GameObject> itemPrefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject itemPrefab in itemPrefabs)
        {
            Instantiate(itemPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
