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

            if (i % 2 == 0) {
                AddNewParticle(new Vector3(x,y,z));
            } else {
                // float radius = Random.Range(10, 20);
                float radius = 10;
                // float mass = Random.Range(0, 10);
                float mass = 1;
                int charge = (int)Random.Range(0, 3)-1;
                
                AddNewParticle(new Vector3(x,y,z), mass, radius, charge);
            }
        }

        dynamicFields.Add(new Coloumb());
        // staticFields.Add(new Wind());
        
    }

    // Called once per frame
    void Update()
    {     
        if (paused) return;
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

    // adds new particle at given location with default parameters
    //  TODO: include mass, size, charge, colour, etc
    public void AddNewParticle(Vector3 pos, float mass = 1, float radius = 10, int charge = 0) 
    {
        particles.Add(new Particle(Instantiate(spawner, pos, Quaternion.identity), mass, radius, charge));
    }

    public void AddNewParticleRandom() {
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);
        AddNewParticle(new Vector3(x,y,z));
    }
}
