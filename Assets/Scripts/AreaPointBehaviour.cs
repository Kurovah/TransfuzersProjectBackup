using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AreaPointBehaviour : MonoBehaviour
{
    public Text displayText;
    public string areaDisplayName;
    public Scene sceneToGo;
    // Start is called before the first frame update
    void Start()
    {
        displayText.text = areaDisplayName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToScene()
    {
        SceneManager.LoadScene(sceneToGo.name);
    }
}
