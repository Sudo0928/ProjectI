using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    public void SpawnItem()
    {
        Vector3 pos = Vector3.zero;
        pos.x += Random.Range(-3.0f, 3f);
        pos.y += Random.Range(-3f, 3f);

        var go = Instantiate<GameObject>(itemPrefab);
        go.transform.position = pos;
        var item = DataManager.gachaSystem.GetItem(4);
        go.GetComponent<Item>().SetItem(item);
	} 
}
