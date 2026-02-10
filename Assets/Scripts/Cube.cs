using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private string _platformTag;

    private bool _isFirstPlatformCollision;

    public event Action<Cube> SelfDestroying;
    public event Action<Cube> FirstPlatformCollided;

    private void OnEnable()
    {
        _isFirstPlatformCollision = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isFirstPlatformCollision && collision.gameObject.CompareTag(_platformTag))
        {
            _isFirstPlatformCollision = false;
            FirstPlatformCollided?.Invoke(this);
        }
    }

    public void Die()
    {
        SelfDestroying?.Invoke(this);
    }
}
