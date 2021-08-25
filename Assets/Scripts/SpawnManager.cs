using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
  [SerializeField]
  private GameObject _enemyPrefab;
  [SerializeField]
  private GameObject _enemyContainer;
  [SerializeField]
  private GameObject[] _powerups;

  private bool _stopSpawning = false;

  void Start() {
    StartCoroutine(SpawnEnemyRoutine());
    StartCoroutine(SpawnPowerupRoutine());
  }

  IEnumerator SpawnEnemyRoutine() {
    while (!_stopSpawning) {
      Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0);
      GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
      newEnemy.transform.parent = _enemyContainer.transform;
      yield return new WaitForSeconds(5.0f);
    }
  }

  IEnumerator SpawnPowerupRoutine() {
    while (!_stopSpawning) {
      Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0);
      int randomPowerUp = Random.Range(0, 3);
      GameObject newPowerupItem = Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
      yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));
    }
  }

  public void onPlayerDeath() {
    _stopSpawning = true;
  }
}
