using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPreviewUI : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject slotPrefab;
    public void OpenUI(ItemInfo item)
    {
        var go = Instantiate<GameObject>(slotPrefab);
        go.transform.SetParent(parent.transform);



	}
}
