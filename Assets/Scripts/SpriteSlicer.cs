using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSlicer : MonoBehaviour
{
    [SerializeField]
    Image image;
    public Vector4 border;
    public void NineSliceSprite()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }
        Debug.Log(image.sprite.border);

        Sprite s = image.sprite;
        Rect rect = new Rect(0, 0, s.texture.width, s.texture.height);
        Sprite newSprite = Sprite.Create(s.texture, rect, new Vector2(0.5f, 0.5f), Mathf.Max(s.texture.width, s.texture.height), 1, SpriteMeshType.FullRect, border);
        newSprite.texture.wrapMode = TextureWrapMode.Repeat;
        image.sprite = newSprite;
        Debug.Log(image.sprite.border);
    }
}
