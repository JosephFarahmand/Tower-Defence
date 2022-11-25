using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReplaceObjects : MonoBehaviour
{
    public GameObject newObject;
    public bool activeInHierarchy;

    [NaughtyAttributes.Button]
    private void Replace()
    {
        var source = transform.childCount;
        for (var i = 0; i < source; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeInHierarchy != activeInHierarchy) continue;

            var objectIns = PrefabUtility.InstantiatePrefab(newObject, transform) as GameObject;
            objectIns.transform.SetPositionAndRotation(child.localPosition, child.localRotation);

            child.gameObject.SetActive(false);
        }

        DestroyImmediate(this);
    }
}
