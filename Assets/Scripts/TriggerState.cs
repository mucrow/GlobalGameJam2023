using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerState: MonoBehaviour {
  bool _isEntered = false;

  public bool IsEntered() {
    return _isEntered;
  }

  void OnTriggerEnter2D(Collider2D other) {
    _isEntered = true;
  }

  void OnTriggerExit2D(Collider2D other) {
    _isEntered = false;
  }
}
