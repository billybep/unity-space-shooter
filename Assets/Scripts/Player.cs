using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 laserOffset;

    public float speed = 3.5f;

    [SerializeField] private GameObject _laserPrefab;
    private SpawnManager _spawnManager;

    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _lives = 3;

    private float _canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        // Take the currunt pos = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject
            .Find("Spawn_Manager")
            .GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        laserOffset = new Vector3(0, 0.8f, 0);

        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
    }

    public void Damage()
    {
        _lives -= 1;

        if (_lives < 1)
        {
            Destroy(gameObject);

            _spawnManager.OnPlayerDeath();
            Debug.Log("GAME OVER");
        }
    }
}
