using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpawner : MonoBehaviour
{
    public InputField[] inputs;

    public ParticleSystem system;
    // Start is called before the first frame update
    void Start()
    {
        system = GameObject.Find("System").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void AttemptCreate() {
        bool abort = false;
        float[] val = new float[3];
        for (int i=0; i < 3; i++) {
            
            if(!float.TryParse(inputs[i].text, out val[i])) {
                inputs[i].image.color = Color.red;
                abort = true;
            }
        }
        if (!abort) {
            system.AddNewParticle(val[0], val[1], val[2]);
        }
    }

    public void ClearColour() {
        for (int i=0; i < 3; i++) {
            inputs[i].image.color = Color.white;
            
        }
    }


}
