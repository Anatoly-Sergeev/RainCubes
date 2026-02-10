using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;
    [SerializeField] private Vector3 _spawnZonePosition;
    [SerializeField] private Vector3 _spawnZoneScale;

    private Bounds _spawnZone;
    private Color _default = Color.white;

    private void OnEnable()
    {
        _pool.TakedFromPool += OnTakeCube;
        _pool.ReturnedToPool += OnReturnedCube;
    }

    private void OnDisable()
    {
        _pool.TakedFromPool -= OnTakeCube;
        _pool.ReturnedToPool -= OnReturnedCube;
    }

    private void Start()
    {
        _spawnZone = new(_spawnZonePosition, _spawnZoneScale);
    }

    public Cube SpawnCube()
    {
        return _pool.GetItem();
    }

    public bool CanSpawn()
    {
        return _pool.CountActiveItems < _pool.SizePool;
    }

    private void OnTakeCube(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.transform.SetPositionAndRotation(GetSpawnPosition(), Random.rotation);

        if (cube.TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = _default;
        }

        cube.SelfDestroying += _pool.ReleaseItem;
    }

    private void OnReturnedCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.SelfDestroying -= _pool.ReleaseItem;
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(_spawnZone.min.x, _spawnZone.max.x),
                           Random.Range(_spawnZone.min.y, _spawnZone.max.y),
                           Random.Range(_spawnZone.min.z, _spawnZone.max.z));
    }
}
