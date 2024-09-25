using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Action TimeElapsedAction;
    [SerializeField] private Text _timerText; // Reference to the Text component
    [SerializeField] private float _elapsedTime; // Time elapsed in seconds
    private bool _isElapsed;
    void Update()
    {
        if (_isElapsed) return;
        _elapsedTime -= Time.deltaTime;

        // Convert the elapsed time to minutes and seconds
        int minutes = Mathf.FloorToInt(_elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60F);

        // Format the time string and update the Text component
        if(_elapsedTime >= 0)
        {
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            _isElapsed = true;
            TimeElapsedAction?.Invoke();
        }
    }

    public void AddTime(int seconds)
    {
        _elapsedTime += seconds;
        if(_elapsedTime > 0)
        {
            _isElapsed = false;
        }
    }

    public void SetTime(int seconds)
    {
        _elapsedTime = seconds;
        if (_elapsedTime > 0)
        {
            _isElapsed = false;
        }
    }
}
