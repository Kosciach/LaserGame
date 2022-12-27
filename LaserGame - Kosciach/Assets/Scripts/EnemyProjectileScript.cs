using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyProjectileScript : MonoBehaviour
{
    [Header("----References-------------")]
    [SerializeField] Transform _reactor;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] ParticleSystem _enemyProjectileParticle;
    [SerializeField] GameObject _shieldProjectilePrefab;
    [SerializeField] LayerMask _shieldMask;

    [Header("----Values-------------")]
    [SerializeField] float _speed;

    private void Awake()
    {
        _reactor = FindObjectOfType<ReactorScript>().transform;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Vector3 lookDistance = _reactor.position - transform.position;
        float lookAngle = Mathf.Atan2(lookDistance.y, lookDistance.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, lookAngle));
        _rigidbody2D.AddForce(transform.up * _speed);
    }

    public void Destruction()
    {
        Instantiate(_enemyProjectileParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Reactor"))
        {
            other.GetComponent<ReactorScript>().TakeDamage();
        }
        else if (other.CompareTag("ShieldSource"))
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, other.transform.position, _shieldMask);
            Debug.DrawLine(transform.position, other.transform.position, Color.cyan);

            if (hit)
            {
                Debug.DrawRay(hit.transform.position, hit.normal, Color.green, 10.0f);
                Vector2 lookAtposition = hit.point + hit.normal * 2;
                Vector2 lookDistance = lookAtposition - hit.point;
                float lookAngle = Mathf.Atan2(lookDistance.y, lookDistance.x) * Mathf.Rad2Deg - 90f;
                Instantiate(_shieldProjectilePrefab, transform.position, Quaternion.Euler(0f, 0f, lookAngle));
                Destruction();
            }
        }
    }
}
