using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableControllerScript : MonoBehaviour
{

    public List<GameObject> pickable;
    public GameObject pickablePrefab;
    public GameObject player;
    public GameObject PickUpCanvas;
    // Start is called before the first frame update
    void Start()
    {
        var pickup = Instantiate(pickablePrefab, transform.position - new Vector3(5, 0, 5), transform.rotation);
        pickable.Add(pickup);
        var pickup2 = Instantiate(pickablePrefab, transform.position + new Vector3(5, 0, 5), transform.rotation);
        pickable.Add(pickup2);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject pick in pickable)
        {
            if ((pick.transform.position - player.transform.position).magnitude <= 2)
            {
                PickUpCanvas.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickable.Remove(pick);
                    Destroy(pick);
                    PickUpCanvas.SetActive(false);
                }
                break;
            }
            else
                PickUpCanvas.SetActive(false);
        }
    }
}
