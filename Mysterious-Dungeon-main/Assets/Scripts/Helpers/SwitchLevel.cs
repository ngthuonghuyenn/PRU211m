using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    public int sceneBuildIndex;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enter new map");
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        } 
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
