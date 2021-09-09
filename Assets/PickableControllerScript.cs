using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableControllerScript : MonoBehaviour
{

    public List<GameObject> pickable;
    public GameObject pickablePrefab;
    public GameObject player;
    public GameObject PickUpCanvas;
    public List<Transform> spawns;
    public GameObject menager;
    SceneManagerBehaviour menagerScript;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in gameObject.transform)
        {
            spawns.Add(child);
        }
        var pickup = Instantiate(pickablePrefab, spawns[Random.Range(0,19)].position, transform.rotation) ;
        pickable.Add(pickup);
        var pickup2 = Instantiate(pickablePrefab, spawns[Random.Range(0, 19)].position, transform.rotation);
        pickable.Add(pickup2);
        var pickup3 = Instantiate(pickablePrefab, spawns[Random.Range(0, 19)].position, transform.rotation);
        pickable.Add(pickup3);
        var pickup4 = Instantiate(pickablePrefab, spawns[Random.Range(0, 19)].position, transform.rotation);
        pickable.Add(pickup4);
        var pickup5 = Instantiate(pickablePrefab, spawns[Random.Range(0, 19)].position, transform.rotation);
        pickable.Add(pickup5);
        player = GameObject.FindGameObjectWithTag("Player");

        PickUpCanvas = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        menager = GameObject.Find("Managers");
        menagerScript = menager.GetComponent<SceneManagerBehaviour>();
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
                    menagerScript.SendMessage("AddItem", 5);
                    Destroy(pick);
                    PickUpCanvas.SetActive(false);
                }
                break;
            }
            else
                PickUpCanvas.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        foreach (GameObject pick in pickable)
            Destroy(pick);
    }
}
