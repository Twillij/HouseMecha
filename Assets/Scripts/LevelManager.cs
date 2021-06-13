using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerController player;
    public Vector3 scaleRequirement;

    public List<GameObject> itemPrefabs = new List<GameObject>();

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        foreach (GameObject itemPrefab in itemPrefabs)
        {
            Instantiate(itemPrefab);
        }
    }

    private bool CheckLevelRequirement()
    {
        Vector3 playerScale = player.transform.localScale;

        if (playerScale.x >= scaleRequirement.x &&
            playerScale.y >= scaleRequirement.y &&
            playerScale.z >= scaleRequirement.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
