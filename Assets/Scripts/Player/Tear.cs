using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    private SpriteRenderer mainSprite;

    private Rigidbody2D _rigidbody2D;

    private float speed = 50f;

    private Vector2 moveDirection = Vector2.zero;

    public void Init(Vector2 moveDirection, float speed = 5)
    {
        this.moveDirection = moveDirection;
        this.speed = speed;
    }

    private void Awake()
    {
        mainSprite = GetComponentInChildren<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(Remove());
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = moveDirection.normalized * speed * Time.fixedDeltaTime;
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
