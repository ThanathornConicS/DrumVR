using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField]
    GameObject drumSet;

    void Start()
    {
        drumSet = GameObject.Find("Drum Set");
    }

    public void SpawnPrefab()
    {
        var drum = Instantiate(prefab, this.transform.position, Quaternion.identity);
        drum.transform.parent = drumSet.transform;

    }
}
