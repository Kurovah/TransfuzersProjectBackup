using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHUD : MonoBehaviour
{
    [SerializeField] private Button button;

    public static TowerHUD Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); }
    }

    public bool GetButtonStatus()
    {
        return button.IsActive();
    }
}
