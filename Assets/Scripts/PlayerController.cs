using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class PlayerController: MonoBehaviour {
  [SerializeField]
  float JumpForce;
  [SerializeField]
  float RunSpeed;

  [SerializeField]
  Rigidbody2D _rigidbody2D;
  [SerializeField]
  TriggerState _onGroundTrigger;
  [SerializeField]
  Tilemap _tilemap;

  Vector2 _velocity = Vector2.zero;

  void Update() {
    var runInput = Input.GetAxis("Horizontal");
    var isJumpPressed = Input.GetButton("Jump");
    var isBuildPressed = Input.GetButtonDown("Fire1");
    var onGround = _onGroundTrigger.IsEntered();

    if (Math.Abs(runInput) > 0.1) {
      _velocity.x = runInput * RunSpeed;
    }
    else {
      _velocity.x = 0;
    }

    if (onGround) {
      if (isJumpPressed && _velocity.y <= 0) {
        _velocity.y = JumpForce;
      }
      else {
        _velocity.y = 0;
      }
    }
    else {
      _velocity.y -= 9.87f * Time.deltaTime;
    }

    _rigidbody2D.velocity = _velocity;

    if (isBuildPressed) {
      var tileData = FindATile();
      // var pos = FindAnEmptyTile();
      var pos = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y - 1), 0);
      _tilemap.SetTile(pos, tileData);
    }
  }

  TileBase FindATile() {
    for (int y = -10; y < 10; ++y) {
      for (int x = -10; x < 10; ++x) {
        var tileData = _tilemap.GetTile(new Vector3Int(x, y, 0));
        if (tileData != null) {
          Debug.Log(tileData);
          return tileData;
        }
      }
    }
    return null;
  }

  Vector3Int FindAnEmptyTile() {
    for (int y = -10; y < 10; ++y) {
      for (int x = -10; x < 10; ++x) {
        var pos = new Vector3Int(x, y, 0);
        var tileData = _tilemap.GetTile(pos);
        if (tileData == null) {
          return pos;
        }
      }
    }
    return new Vector3Int(0, 0, 0);
  }
}
