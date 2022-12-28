using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionerScript : MonoBehaviour
{
    [Header("----Reference--------------")]
    [SerializeField] Camera _camera;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Transform _transitionEnd;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] Transform _hider;

    [Header("----Values--------------")]
    [SerializeField] float _transitionerSpeed;


    private Vector3 _transitionerPosition;
    private Vector2 _halfCameraSize;
    private string _newScene;


    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transitionEnd = transform.parent.GetChild(1);
        _hider = transform.parent.GetChild(2);
    }

    private void Start()
    {
        _halfCameraSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);//Sets screen size (half)

        transform.localScale = new Vector3(0.5f, _halfCameraSize.y*2, 0f);//Sets scale
        transform.position = new Vector3(_halfCameraSize.x + transform.localScale.x / 2, 0f, 0f);//Sets position

        _hider.localScale = transform.localScale;//Sets hider scale
        _hider.position = new Vector3(_halfCameraSize.x, 0f, 0f);

        _transitionEnd.position = new Vector3(-_halfCameraSize.x - transform.localScale.x * 2, 0f, 0f);//Sets end position
    }
    private void Update()
    {
        Debug.DrawLine(_hider.position, transform.position, Color.green);
        float distance = _hider.position.x - transform.position.x;//Calculate distance between two;
        _hider.localScale = new Vector3(distance, _hider.localScale.y, 0f);//Set x scale as distance;
    }

    public void StartTransition(string newScene)
    {
        _newScene = newScene;
        _rigidbody2D.velocity = -transform.right * _transitionerSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EndTransition"))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
