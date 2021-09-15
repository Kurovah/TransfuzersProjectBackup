using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchAreaSlider : MonoBehaviour
{
    Vector3 initialMousePos, initialObjectPos ,MouseOffset;
    public RectTransform r;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        initialMousePos = Input.mousePosition;
        initialObjectPos = r.position;
        Debug.Log("Cllicked");
    }

    private void OnMouseDrag()
    {
        MouseOffset = Input.mousePosition;
        r.position = initialObjectPos + (initialMousePos + MouseOffset);
    }
}
