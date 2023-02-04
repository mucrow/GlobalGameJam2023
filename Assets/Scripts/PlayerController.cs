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

  bool facingRight = true;

  Vector2 _velocity = Vector2.zero;

  void Update() {
    // Left Arrow, Right Arrow, the A key, or the D key. works for gamepads too
    var runInput = Input.GetAxis("Horizontal");
    var isDownPressed = Input.GetAxis("Vertical"); 

    // spacebar
    var isJumpPressed = Input.GetButton("Jump");

    // this is left mouse button. i call the action "build" ("Fire1" is the name of the left mouse
    // button input in Unity default input mapping)
    var isBuildPressed = Input.GetButtonDown("Fire1");
    // right mouse button to dig
    var isDigPressed = Input.GetButtonDown("Fire2");
    var onGround = _onGroundTrigger.IsEntered();

    // 0.1 is the analog stick deadzone for running
    if (Math.Abs(runInput) > 0.1 && !facingRight) {
      Flip();
      // run if there's an input
      _velocity.x = runInput * RunSpeed;
    } else if (Math.Abs(runInput) > 0.1 && facingRight){
      Flip();
      // run if there's an input
      _velocity.x = runInput * RunSpeed;
    } 
    else {
      // stop if there isn't
      _velocity.x = 0;
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
      // this is debugging code. i don't know how to access tile data correctly (so the player
      // knows what tile theyre placing) so i just find a non-empty tile in the level and use that
      var tileData = FindATile();

      // use the player's position to determine the target position of the tile we're placing
      // basically it's (player.x, player.y - 1)
      var pos = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y - 1), 0);

      // modify the world

      // TODO: if condition for when tile already existing
      _tilemap.SetTile(pos, tileData);
    }
    
   // if the player pressed the dig button...
    if (isDigPressed) {
      var tileData = FindATile();
      Vector3Int pos = new Vector3Int(0, 0, 0);

      //dig below
      if (Math.Abs(isDownPressed) > 0.1) {
        Debug.Log("down");
        // use the player's position to determine the target position of the tile we're placing
        // basically it's (player.x, player.y - 1)
        pos = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y - 1), 0);
        _tilemap.SetTile(pos, null);
      } 
      //dig to the side
      else if (Math.Abs(runInput) > 0.1) {
        Debug.Log("side");

        // need to check rotation of the character (facing forward or back)
        if (facingRight) {
        pos = new Vector3Int(Mathf.FloorToInt(transform.position.x + 1), Mathf.FloorToInt(transform.position.y), 0);
        } else {
        pos = new Vector3Int(Mathf.FloorToInt(transform.position.x - 1), Mathf.FloorToInt(transform.position.y), 0);
        }
        _tilemap.SetTile(pos, null);
      }

      // modify the world
    }

  }

  void Flip() {
    Debug.Log("Flipped");
    Vector3 currentScale = gameObject.transform.localScale;
    currentScale.x *= -1;
    gameObject.transform.localScale = currentScale;
    
    facingRight = !facingRight;
  }
  
  // returns a non-empty tile in the range of (-10, -10) to (10, 10) in the world
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
}
