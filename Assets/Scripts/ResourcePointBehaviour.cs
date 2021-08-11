using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePointBehaviour : MonoBehaviour
{
    public int drop;
    public GameObject fieldItemPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropItem()
    {
        float t = Random.Range(0, 360);
        Vector3 placepos = new Vector3(transform.position.x + Mathf.Sin(t) * 3, 0, transform.position.z + Mathf.Cos(t) * 3);
        GameObject i = Instantiate(fieldItemPrefab, placepos, Quaternion.identity);
        i.GetComponent<FieldItemBehaviour>().itemIndex = drop;
        i.GetComponent<FieldItemBehaviour>().UpdateSprite();
    }
}

