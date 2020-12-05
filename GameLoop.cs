using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Laser laser;
    [SerializeField] private List<GameObject> Items = new List<GameObject>();

    private void Start()
    {
        laser = FindObjectOfType<Laser>();
        InstantiateNewItem();
    }

    public void InstantiateNewItem()
    {
        Instantiate(Items[Random.Range(0, Items.Count)], new Vector3(0, -0.1f, Camera.main.transform.position.z + 0.55f), Quaternion.Euler(0, 90, 0));

        laser.SetFields();
    }
}
