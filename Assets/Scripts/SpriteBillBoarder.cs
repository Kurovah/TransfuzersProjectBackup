using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillBoarder : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
