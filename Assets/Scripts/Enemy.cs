using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  [SerializeField]
  private float _speed = 4.0f;

  private Player _player;

  private Animator _anim;

  void Start() {
    _player = GameObject.Find("Player").GetComponent<Player>();

    if (_player == null) {
      Debug.LogError("Player is NULL.");
    }

    _anim = GetComponent<Animator>();

    if (_anim == null) {
      Debug.LogError("The Animator is NULL.");
    }
  }

  void Update() {
    transform.Translate(Vector3.down * _speed * Time.deltaTime);

    if (transform.position.y <= -5.5f) {
      float randomX = Random.Range(-9.5f, 9.5f);
      transform.position = new Vector3(randomX, 7.5f, 0);
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "Player") {
      Player player = other.transform.GetComponent<Player>();

      if (player != null) {
        player.Damage();
      }

      _anim.SetTrigger("OnEnemyDeath");
      _speed = 0;
      gameObject.GetComponent<BoxCollider2D>().enabled = false;
      Destroy(gameObject, 2.8f);
    }

    if (other.tag == "Laser") {
      Destroy(other.gameObject);

      if (_player != null) {
        _player.AddScore(10);
      }

      _anim.SetTrigger("OnEnemyDeath");
      _speed = 0;
      gameObject.GetComponent<BoxCollider2D>().enabled = false;
      Destroy(gameObject, 2.8f);
    }
  }
}
