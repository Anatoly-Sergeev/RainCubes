using System.Collections;
using UnityEngine;

public class RainCubes : MonoBehaviour
{
    private const float MinDelay = 2.0f;
    private const float MaxDelay = 5.0f;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Painter _painter;

    private WaitForSeconds _wait;

    private void Update()
    {
        if (_spawner.CanSpawn())
        {
            Cube cube = _spawner.SpawnCube();
            cube.FirstPlatformCollided += OnPlatformCollision;
        }
    }

    private void OnPlatformCollision(Cube cube)
    {
        if (cube.TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = _painter.GetRandomColor();
        }

        StartCoroutine(DieAfterDelay(cube));
    }

    private IEnumerator DieAfterDelay(Cube cube)
    {
        _wait = new(GetRandomDelay());
        yield return _wait;

        cube.FirstPlatformCollided -= OnPlatformCollision;
        cube.Die();
    }

    private float GetRandomDelay()
    {
        return Random.Range(MinDelay, MaxDelay);
    }
}
