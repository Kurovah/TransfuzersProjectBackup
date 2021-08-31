using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerIcon : MonoBehaviour
{
    public int index;
    public Text amountText;
    public Image towerImage;
    public Button Button;

    private void Update()
    {
        Button.interactable = SceneManagerBehaviour.GetTurretAmount(index) > 0;
        amountText.text = SceneManagerBehaviour.GetTurretAmount(index).ToString();
    }
}
