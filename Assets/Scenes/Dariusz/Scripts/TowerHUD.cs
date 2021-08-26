using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHUD : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject activeCamera;
    [SerializeField] private GameObject canvas;
    public static TowerHUD Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        SetHUDActive();
    }
    
    public bool GetButtonStatus()
    {
        return button.IsActive();
    }

    private void SetHUDActive()
    {
        switch (activeCamera.activeSelf == true)
        {
            case true:
                canvas.SetActive(true);
                Debug.Log("yes");
                break;
            case false:
                canvas.SetActive(false);
                break;
        }
        if(activeCamera.activeSelf == true)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(true);
        }
    }
}
