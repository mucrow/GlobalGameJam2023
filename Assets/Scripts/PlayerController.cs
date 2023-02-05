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
  EnvironmentController _env;

  bool facingRight = true;

  Vector2 _velocity = Vector2.zero;

  [SerializeField]
  GameObject _downSlashAnimation;
  [SerializeField]
  GameObject _forwardSlashAnimation;
  [SerializeField]
  GameObject _idleAnimation;
  [SerializeField]
  GameObject _runAnimation;

  void Update() {
    // Left Arrow, Right Arrow, the A key, or the D key. works for gamepads too
    var runInput = Input.GetAxis("Horizontal");
    var inputHorizontal = Input.GetAxisRaw("Horizontal");
    var isDownPressed = Input.GetAxis("Vertical"); 

    // spacebar
    var isJumpPressed = Input.GetButton("Jump");

    // this is left mouse button. i call the action "dig" ("Fire1" is the name of the left mouse
    // button input in Unity default input mapping)
    var isDigPressed = Input.GetButtonDown("Fire1");
    // right mouse button to "build"
    var isBuildPressed = Input.GetButtonDown("Fire2");
    var onGround = _onGroundTrigger.IsEntered();


    // 0.1 is the analog stick deadzone for running
    if (Math.Abs(runInput) > 0.1) {
      // run if there's an input
      _velocity.x = runInput * RunSpeed;
      if (inputHorizontal > 0 && !facingRight) {
        Flip();
        SetAnimationToRun();
      }
      if (inputHorizontal < 0 && facingRight) { 
        Flip();
        SetAnimationToRun();
      }
    } 
    else {
      // stop if there isn't
      _velocity.x = 0;
      SetAnimationToIdle();
    }

    // if the player is on the ground...
    if (onGround) {
      // and the jump button is pressed, and they are not rising...
      if (isJumpPressed && _velocity.y <= 0) {
        // make them jump
        _velocity.y = JumpForce;
      }
      else {
        // set velocity to 0 if the player is on the ground but not trying to jump
        _velocity.y = 0;
      }
    }
    else {
      // if the player is not on the ground, apply gravity.
      // this is scaled with deltaTime because the player should accelerate downward
      _velocity.y -= 9.87f * Time.deltaTime;
    }

    // apply the velocity to the rigidbody
    // TODO check if we actually need the _velocity variable (maybe we can use _rigidbody2D.velocity directly)
    _rigidbody2D.velocity = _velocity;

    // if the player pressed the build button...
    if (isBuildPressed) {
      _env.buildTile(transform);
    }
    
   // if the player pressed the dig button...
    if (isDigPressed) {
      _env.digTile(transform, facingRight, isDownPressed);
    }

  }

  void Flip() {
    Vector3 currentScale = gameObject.transform.localScale;
    currentScale.x *= -1;
    gameObject.transform.localScale = currentScale;
    
    facingRight = !facingRight;
  }

  void SetAnimationToIdle() {
    _downSlashAnimation.SetActive(false);
    _forwardSlashAnimation.SetActive(false);
    _runAnimation.SetActive(false);
    _idleAnimation.SetActive(true);
  }

  void SetAnimationToRun() {
    _downSlashAnimation.SetActive(false);
    _forwardSlashAnimation.SetActive(false);
    _idleAnimation.SetActive(false);
    _runAnimation.SetActive(true);
  }

  void SetAnimationToDownSlash() {
    _forwardSlashAnimation.SetActive(false);
    _runAnimation.SetActive(false);
    _idleAnimation.SetActive(false);
    _downSlashAnimation.SetActive(true);
  }

  void SetAnimationToForwardSlash() {
    _downSlashAnimation.SetActive(false);
    _runAnimation.SetActive(false);
    _idleAnimation.SetActive(false);
    _forwardSlashAnimation.SetActive(true);
  }
}
