using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoost : MonoBehaviour
{
    [SerializeField] private float _gamePlaySpeed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnValidate()
    {
        if (Application.isEditor)
        {
           Time.timeScale = _gamePlaySpeed;
        }
    }

    private void OnDestroy()
    {
       Time.timeScale = 1f;
    }
}
