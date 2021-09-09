using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItemBehaviour : MonoBehaviour
{
    public int itemIndex;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer.sprite = FindObjectOfType<SceneManagerBehaviour>().itemList.items[itemIndex].itemSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControllerScript>())
        {
            FindObjectOfType<SceneManagerBehaviour>().AddItem(1);
            Destroy(gameObject);
        }
    }

    public void UpdateSprite()
    {
        spriteRenderer.sprite = FindObjectOfType<SceneManagerBehaviour>().itemList.items[itemIndex].itemSprite;
    }
}