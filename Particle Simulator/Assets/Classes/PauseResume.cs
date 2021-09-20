using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseResume : MonoBehaviour
{
 
    public GameObject PauseScreen;
    public GameObject PauseButton;
    public GameObject AddParticle;

    private ParticleSystem system;

    public Text text;

    public Slider slider;
 
    // Start is called before the first frame update
    void Start()
    {
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
        AddParticle.SetActive(false);
        system = GameObject.Find("System").GetComponent<ParticleSystem>();
        
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

    public void AddNewParticle() {
        if (AddParticle.activeSelf) {
            AddParticle.SetActive(false);
        }
        else {
            AddParticle.SetActive(true);
        }

    }

    public void TimeScale(float dt) {
        system.updateDT(dt);
    }
}