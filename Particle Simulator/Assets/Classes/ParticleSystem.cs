using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleSystem: MonoBehaviour
{
    // Fields
    // [SerializeField]
    public float dt = 0.05F;

    [SerializeField][Range(0,20)]
    private int NUM_PARTICLES = 10;

    public GameObject spawner;

    public bool paused = false;

    // lists of particles, dynamic fields and static fields
    List<Particle> particles = new List<Particle>();
    List<DynamicField> dynamicFields = new List<DynamicField>();
    List<StaticField> staticFields = new List<StaticField>();

    public System.Random ran = new System.Random();

    // Called once initially
    void Start()
    { 
        //creates random particles
        for (int i = 0; i < NUM_PARTICLES; i++) {
            float x = Random.Range(-10, 10);
            float z = Random.Range(-10, 10);
            float y = Random.Range(-10, 10);

            particles.Add(new Particle(Instantiate(spawner, new Vector3(x,y,z), Quaternion.identity)));
        }

        dynamicFields.Add(new Coloumb());
        // staticFields.Add(new Wind());
        
    }

    // Called once per frame
    void Update()
    {
        if (paused) return;
        if(Input.GetMouseButtonUp(1)) {
            
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                if (particles.Count > 0) {
                int del = ran.Next(0, particles.Count-1);
                Destroy(particles[del].getGameObject());
                particles.RemoveAt(del);
                }
            }
            else {
                float x = Random.Range(-10, 10);
                float z = Random.Range(-10, 10);
                float y = Random.Range(-10, 10);
                AddNewParticle(x,y,z);
            }

        }

        
        // Static Field Contributions
        foreach (StaticField F in staticFields) {
            foreach (Particle A in particles) {
                A.addFoce(F.force(A));
            }
        }

        // Dynamic Field Contributions
        foreach (DynamicField F in dynamicFields) {
            foreach (Particle A in particles) {
                foreach (Particle B in particles) {
                    A.addFoce(F.force(A, B));
                }
            }
        }
        // Update Positions
        foreach (Particle A in particles) {
            A.step(dt);
        }       
    }

    // called by the slider to update dt
    public void updateDT(float dT) {
        dt = dT;
    }

    // called by pause menu
    public void togglePause() {
        paused = !paused;
    }

    // adds new particle at given location
    //  TODO: include mass, size, charge, colour, etc
    public void AddNewParticle(float x, float y, float z) {
        particles.Add(new Particle(Instantiate(spawner, new Vector3(x,y,z), Quaternion.identity)));
    }
}
