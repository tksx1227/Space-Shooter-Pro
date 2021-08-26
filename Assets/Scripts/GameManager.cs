using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  [SerializeField]
  private bool _isGameOver = false;

  private void Update() {
    if (Input.GetKeyDown(KeyCode.R) && _isGameOver) {
      // Current Game Scene
      SceneManager.LoadScene(1);
    }
  }

  public void GameOver() {
    Debug.Log("GameManager::GameOver() Called");
    _isGameOver = true;
  }
}
