using UnityEngine;

public class LoopingSpriteAnimator: MonoBehaviour {
  [SerializeField]
  Sprite[] _animationFrames;

  [SerializeField]
  float _timePerFrame = 0.3f;

  SpriteRenderer _renderer;

  float _timeToNextFrame;

  int _currentFrame;

  void Awake() {
    _renderer = GetComponent<SpriteRenderer>();
    _currentFrame = 0;
    _timeToNextFrame = _timePerFrame;
  }

  void Update() {
    _timeToNextFrame -= Time.deltaTime;
    if (_timeToNextFrame < 0f) {
      _currentFrame = (_currentFrame + 1) % _animationFrames.Length;
      _renderer.sprite = _animationFrames[_currentFrame];
      _timeToNextFrame += _timePerFrame;
    }
  }
}
