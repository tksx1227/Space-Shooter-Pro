using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField]
  private float _speed = 3.5f;
  private float _speedMultiplier = 2;
  [SerializeField]
  private GameObject _laserPrefab;
  [SerializeField]
  private GameObject _tripleShotPrefab;
  [SerializeField]
  private float _fireRate = 0.15f;
  private float _nextFire = -1.0f;
  [SerializeField]
  private int _lives = 3;
  private SpawnManager _spawnManager;

  private bool _isTripleShotActive = false;
  private bool _isShieldsActive = false;

  [SerializeField]
  private GameObject _shieldsVisualizer;

  [SerializeField]
  private int _score = 0;

  private UIManager _uiManager;

  void Start() {
    _shieldsVisualizer.SetActive(false);
    transform.position = new Vector3(0, 0, 0);

    _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    if (_spawnManager == null) {
      Debug.LogError("The Spawn Manager is NULL.");
    }

    if (_uiManager == null) {
      Debug.LogError("The UI Manager is NULL.");
    }
  }

  void Update() {
    CalculateMovement();

    if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire) {
      FireLaser();
    }
  }

  void CalculateMovement() {
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
    transform.Translate(direction * _speed * Time.deltaTime);

    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

    if (transform.position.x > 11.3f) {
      transform.position = new Vector3(-11f, transform.position.y, 0);
    } else if (transform.position.x < -11.3f) {
      transform.position = new Vector3(11f, transform.position.y, 0);
    }
  }

  void FireLaser() {
    _nextFire = Time.time + _fireRate;

    if (_isTripleShotActive) {
      Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
    } else {
      Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
    }
  }

  public void Damage() {
    if (_isShieldsActive) {
      _isShieldsActive = false;
      _shieldsVisualizer.SetActive(false);
      return;
    }

    _lives -= 1;

    _uiManager.UpdateLives(_lives);

    if (_lives < 1) {
      _spawnManager.onPlayerDeath();
      Destroy(this.gameObject);
    }
  }

  public void TripleShotActive() {
    _isTripleShotActive = true;
    StartCoroutine(TripleShotPowerDownRoutine());
  }

  IEnumerator TripleShotPowerDownRoutine() {
    yield return new WaitForSeconds(5.0f);
    _isTripleShotActive = false;
  }

  public void SpeedBoostActive() {
    _speed *= _speedMultiplier;
    StartCoroutine(SpeedBoostPowerDownRoutine());
  }

  IEnumerator SpeedBoostPowerDownRoutine() {
    yield return new WaitForSeconds(5.0f);
    _speed /= _speedMultiplier;
  }

  public void ShieldsActive() {
    _shieldsVisualizer.SetActive(true);
    _isShieldsActive = true;
  }

  public void AddScore(int points) {
    _score += points;
    _uiManager.UpdateScore(_score);
  }
}
