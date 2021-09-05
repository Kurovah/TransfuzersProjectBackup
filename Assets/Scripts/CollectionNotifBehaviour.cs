using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CollectionNotifBehaviour : MonoBehaviour
{
    public Text entryText;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0,1,1);
        transform.DOScaleX(1, 0.5f).SetEase(Ease.OutElastic,1.2f);
        StartCoroutine(LifeTick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LifeTick()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    public void SetText(string ItemText)
    {
        entryText.text = $"[{ItemText.ToUpper()}] acquired";
    }
    
}
