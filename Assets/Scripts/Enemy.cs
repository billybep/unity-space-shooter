using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
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

    private void OnTriggerEnter(Collider other)
    {
        // if collied with player
        if (other.tag == "Player")
        {
            Debug.Log("Player");
            Destroy(gameObject);
        }

        // if collied with laser
        if (other.tag == "Laser")
        {
            Debug.Log("Lazzer");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
