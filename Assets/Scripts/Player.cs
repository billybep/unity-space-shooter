using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 laserOffset;

    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _speedMultiplier = 2;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShootPrefab;
    [SerializeField] private GameObject _shieldVisualizer;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private bool _isTripleShootActive = false;
    [SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] private bool _isShieldActive = false;
    [SerializeField] private int _score = 0;

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

        _uiManager = GameObject
            .Find("Canvas")
            .GetComponent<UIManager>();

        if (_uiManager == null)
        { 
            Debug.LogError("UIManager is NULL.");
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

        transform.Translate(direction * _speed * Time.deltaTime);

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


        if (_isTripleShootActive)
        {
            Instantiate(_tripleShootPrefab, transform.position + laserOffset, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives -= 1;

        if (_lives < 1)
        {
            Destroy(gameObject);

            _spawnManager.OnPlayerDeath();
            Debug.Log("GAME OVER");
        }
    }

    public void TripleShootActive()
    {
        _isTripleShootActive = true;
        StartCoroutine(TripleShootPowerDownRoutine());
    }

    IEnumerator TripleShootPowerDownRoutine()
    {
        while (_isTripleShootActive)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShootActive = false;
        }
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void SheildActive()
    { 
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
