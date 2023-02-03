using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController: MonoBehaviour {
  public float JumpForce;
  [FormerlySerializedAs("RunForce")]
  public float RunSpeed;

  [SerializeField]
  Rigidbody2D _rigidbody2D;
  [SerializeField]
  TriggerState _onGroundTrigger;

  Vector2 _velocity = Vector2.zero;

  void Update() {
    var runInput = Input.GetAxis("Horizontal");
    var isJumpPressed = Input.GetButton("Jump");
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
  }
}
