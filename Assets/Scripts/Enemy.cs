using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject
                .Find("Player")
                .GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move enemy 4m / sec
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // if enemy at the bottom of screen then respawn at the top with a new random pos
        if (transform.position.y < -5.6f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Destroy(gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            Destroy(gameObject);
        }
    }
}
