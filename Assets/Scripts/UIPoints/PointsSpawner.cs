using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PointsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedText;
 
    public void SpawnPoints(int points)
    {
        spawnedText.GetComponent<Text>().text = points.ToString();
        Instantiate(spawnedText, gameObject.transform);
    }

    public void Spawn1UP()
    {
        GetComponent<Text>().text = "1UP";
    }
}
