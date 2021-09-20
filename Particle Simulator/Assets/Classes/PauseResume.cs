using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseResume : MonoBehaviour
{
    //components of the pause screen
    public GameObject PauseScreen;
    public GameObject PauseButton;
    public GameObject AddParticle;

    private ParticleSystem system;

    public Text text;

    public Slider slider;
 
    // Start is called before the first frame update
    void Start()
    {
        // show/hide menu items
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
        AddParticle.SetActive(false);
        // get system object
        system = GameObject.Find("System").GetComponent<ParticleSystem>();
        
        // set value of slider to dt of system object
        slider.value = system.dt;
        Debug.Log(slider.value);
    }
 
    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString("0.000");
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
        AddParticle.SetActive(false);
    }

    // called by the add particle button - shows new menu
    public void AddNewParticle() {
        if (AddParticle.activeSelf) {
            AddParticle.SetActive(false);
        }
        else {
            AddParticle.SetActive(true);
        }

    }

    //updates the timescale as per the slider
    public void TimeScale(float dt) {
        system.updateDT(dt);
    }
}