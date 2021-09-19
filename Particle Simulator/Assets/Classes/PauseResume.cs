using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PauseResume : MonoBehaviour
{
 
    public GameObject PauseScreen;
    public GameObject PauseButton;

    private ParticleSystem system;
 
    // Start is called before the first frame update
    void Start()
    {
        system = GameObject.Find("System").GetComponent<ParticleSystem>();
    }
 
    // Update is called once per frame
    void Update()
    {

    }
 
    public void PauseGame()
    {
        system.togglePause();
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
    }
 
    public void ResumeGame()
    {
        system.togglePause();
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void TimeScale(float dt) {
        system.updateDT(dt);
    }
}