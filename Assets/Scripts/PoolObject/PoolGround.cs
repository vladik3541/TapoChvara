using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolGround : MonoBehaviour
{
    private GameObject _prefab; // ������, ���� ���� ����������������� �����
    private int initialPoolSize = 10; // ���������� ����� ����

    private List<GameObject> pool; // ������ ������ ��'����   

    // ��������� ����������� ���� ��'����
    public void CreateInitialPool(GameObject prefab)
    {
        pool = new List<GameObject>();
        _prefab = prefab;
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObjectInPool();
        }
    }

    // ��������� ������ ��'���� � ���
    GameObject CreateNewObjectInPool()
    {
        GameObject obj = Instantiate(_prefab);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    // ��������� ������� ��'���� � ����
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

        // ���� �� ��'���� � ��� ������, �������� �����
        return CreateNewObjectInPool();
    }

    // ��������� ��'��� � ���
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void RecreatePool(GameObject newPrefab)
    {
        // ������� �� ������� ��'���� � ���
        foreach (GameObject obj in pool)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // �������� ������
        pool.Clear();

        // ������� ������
        _prefab = newPrefab;

        // �������� ��� ��'����
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObjectInPool();
        }
    }
}
