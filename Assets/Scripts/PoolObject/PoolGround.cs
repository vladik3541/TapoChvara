using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolGround : MonoBehaviour
{
    private GameObject _prefab; // Префаб, який буде використовуватися пулом
    private int initialPoolSize = 10; // Початковий розмір пулу

    private List<GameObject> pool; // Список вільних об'єктів   

    // Створення початкового пулу об'єктів
    public void CreateInitialPool(GameObject prefab)
    {
        pool = new List<GameObject>();
        _prefab = prefab;
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObjectInPool();
        }
    }

    // Створення нового об'єкта в пулі
    GameObject CreateNewObjectInPool()
    {
        GameObject obj = Instantiate(_prefab);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    // Отримання вільного об'єкту з пулу
    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Якщо всі об'єкти в пулі зайняті, створити новий
        return CreateNewObjectInPool();
    }

    // Повернути об'єкт в пул
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void RecreatePool(GameObject newPrefab)
    {
        // Знищити всі існуючі об'єкти в пулі
        foreach (GameObject obj in pool)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // Очистити список
        pool.Clear();

        // Оновити префаб
        _prefab = newPrefab;

        // Створити нові об'єкти
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObjectInPool();
        }
    }
}
