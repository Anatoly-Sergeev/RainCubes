using System;
using UnityEngine;
using UnityEngine.Pool;

public class Pooler : MonoBehaviour
{
    [SerializeField] private Cube _prefabCube;
    [SerializeField] private int _capacity;

    private ObjectPool<Cube> _pool;

    public event Action<Cube> TakedFromPool;
    public event Action<Cube> ReturnedToPool;

    public int CountActiveItems => _pool.CountActive;
    public int SizePool => _capacity;

    private void Start()
    {
        _pool = new(CreatePoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolItem, false, _capacity, _capacity);
    }

    public Cube GetItem()
    {
        return _pool.Get();
    }

    public void ReleaseItem(Cube cube)
    {
        _pool.Release(cube);
    }

    private Cube CreatePoolItem()
    {
        return Instantiate(_prefabCube);
    }

    private void OnTakeFromPool(Cube cube)
    {
        TakedFromPool?.Invoke(cube);
    }

    private void OnReturnedToPool(Cube cube)
    {
        ReturnedToPool?.Invoke(cube);
    }

    private void OnDestroyPoolItem(Cube cube)
    {
        Destroy(cube.gameObject);
    }
}
