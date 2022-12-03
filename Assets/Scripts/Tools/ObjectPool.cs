using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    //[SerializeField] private Projectile projectilePrefab;
    //[SerializeField] private int objectCount = 20;
    //private List<Projectile> projectiles;

    [SerializeField] private List<ObjectIns> objects;

    [System.Serializable]
    public class ObjectIns
    {
        public string id;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int objectCount;
        private List<GameObject> createdObjects;

        public void Create()
        {
            createdObjects = new List<GameObject>();
            for (int i = 0; i < objectCount; i++)
            {
                var ins = InstantiateNew();
                createdObjects.Add(ins);
            }
        }

        private GameObject InstantiateNew()
        {
            var ins = Instantiate(prefab);
            ins.SetActive(false);
            return ins;
        }

        public GameObject Get()
        {
            foreach (var obj in createdObjects)
            {
                if (obj.activeInHierarchy == false)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            var ins = InstantiateNew();
            createdObjects.Add(ins);
            return ins;
        }

        public T Get<T>() where T:Component
        {
            foreach (var obj in createdObjects)
            {
                if (obj.activeInHierarchy == false && obj.TryGetComponent(out T t))
                {
                    obj.SetActive(true);
                    return t;
                }
            }

            var ins = InstantiateNew();
            createdObjects.Add(ins);
            return ins.GetComponent<T>();
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        foreach(var data in objects)
        {
            data.Create();
        }
    }

    public GameObject Get(string id)
    {
        return objects.Find(x => x.id == id).Get();
    }

    public T Get<T>(string id) where T : Component
    {
        return objects.Find(x => x.id == id).Get<T>();
    }
}
