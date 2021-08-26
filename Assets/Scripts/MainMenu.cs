using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
  public void LoadGame() {
    // Main Game Scene
    SceneManager.LoadScene(1);
  }
}
