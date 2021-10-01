using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class ParticleSystem: MonoBehaviour
{
    // Fields
    // [SerializeField]
    public Scales scales;

    [SerializeField][Range(0,20)]
    private int NUM_PARTICLES = 10;

    public GameObject spawner;

    public bool paused = false;

    // lists of particles, dynamic fields and static fields
    List<Particle> particles = new List<Particle>();
    List<DynamicField> dynamicFields = new List<DynamicField>();
    List<StaticField> staticFields = new List<StaticField>();

    private System.Random rnd = new System.Random(9);

    // Called once initially
    void Start()
    { 
        scales = new Scales();
        //creates random particles
        for (int i = 0; i < NUM_PARTICLES; i++) {
            float x = rnd.Next(-10, 10);
            float z = rnd.Next(-10, 10);
            float y = rnd.Next(-10, 10);

            if (rnd.Next() % 3==0) {
                AddNewParticle(new Vector3(x,y,z));
            } else {
                // float radius = Random.Range(10, 20);
                float radius = 1f;
                // float mass = Random.Range(0, 10);
                float mass = 1f;
                int charge = (int)Random.Range(0, 3)-1;
                
                AddNewParticle(new Vector3(x,y,z), mass, radius, charge);
            }
        }

        dynamicFields.Add(new Coloumb());
        // dynamicFields.Add(new LennardJones());

        // staticFields.Add(new Wind());      
    }

    // Called once per frame
    void Update()
    {
        if (paused) return;
        // Static Field Contributions
        // List<Thread> threads = new List<Thread>();
        // foreach (Particle A in particles) {
        for (int a = 0; a < particles.Count-1; a++) {
            // Thread updatethread = new Thread(() => updateVelocity(A));
            // threads.Add(updatethread);
            // updatethread.Start();
            updateVelocity(a);
        }  
        // foreach (Thread t in threads) {
        //     t.Join();
        // }

        updatePositions();
    }

    private void updateVelocity(int a) {
        foreach (StaticField F in staticFields) {
            // A.addFoce(F.force(A, scales));
        }

        // Dynamic Field Contributions
        foreach (DynamicField F in dynamicFields) {
            for (int b = a+1; b < particles.Count; b++) {
                F.applyForce(particles[a], particles[b], scales);
            }

        }
    }

    private void updatePositions() {
        foreach (Particle A in particles) {
            A.step(scales.getTime());
        }
    }
    
    // called by pause menu
    public void togglePause() {
        paused = !paused;
    }

    // adds new particle at given location with default parameters
    //  TODO: include mass, size, charge, colour, etc
    public void AddNewParticle(Vector3 pos, float mass = 1, float radius = 1f, int charge = 0) 
    {
        particles.Add(new Particle(Instantiate(spawner, pos, Quaternion.identity), mass, radius, charge));
    }

    public void AddNewParticleRandom() {
        float x = rnd.Next(-10, 10);
        float z = rnd.Next(-10, 10);
        float y = rnd.Next(-10, 10);
        AddNewParticle(new Vector3(x,y,z));
    }
}
