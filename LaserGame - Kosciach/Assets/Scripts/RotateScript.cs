using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    [SerializeField] Transform _centerOfGravity;
    [SerializeField] Rigidbody2D _rigidbody2d;
    [SerializeField] float _speed;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector3 lookDistance = _centerOfGravity.position - transform.position;
        float lookAngle = Mathf.Atan2(lookDistance.y, lookDistance.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        _rigidbody2d.velocity = transform.right * _speed;
    }
}
