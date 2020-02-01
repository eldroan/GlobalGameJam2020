using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateTitle : MonoBehaviour
{
    // Start is called before the first frame update
    public Image target;
    public Sprite[] sprites;
    public float timeBetweenFrames;
    private float _currentTime = 0f;
    private int _currentSpriteIndex = 0;
    private int currentRandomIndex;


    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (timeBetweenFrames < _currentTime)
        {
            _currentTime = 0f;
            currentRandomIndex = Random.Range(0, 100) % sprites.Length;
            _currentSpriteIndex = _currentSpriteIndex == currentRandomIndex ? (currentRandomIndex + 1) % sprites.Length : currentRandomIndex;
            target.sprite = sprites[_currentSpriteIndex];
        }
    }
}
