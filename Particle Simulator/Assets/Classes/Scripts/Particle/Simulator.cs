using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class Simulator  : MonoBehaviour
{


    [SerializeField][Range(0,20)]
    private int NUM_PARTICLES = 10;

    public Scales scales;

    public bool paused;

    private SimulationManager manager;

    // lists of particles, dynamic fields and static fields
    List<Particle> particles = new List<Particle>();
    List<DynamicField> dynamicFields = new List<DynamicField>();
    List<StaticField> staticFields = new List<StaticField>();

    private System.Random rnd = new System.Random(9);

    // Called once initially
    void Start()
    { 
        manager = GameObject.Find("Manager").GetComponent<SimulationManager>();
        scales = new Scales();
        //creates random particles
        for (int i = 0; i < NUM_PARTICLES; i++) {
            float x = rnd.Next(-10, 10);
            float z = rnd.Next(-10, 10);
            float y = rnd.Next(-10, 10);

            // float radius = Random.Range(10, 20);
            float radius = 1f;
            // float mass = Random.Range(0, 10);
            float mass = 1f;
            int charge = (int)Random.Range(0, 3)-1;
            
            AddNewParticle(new Vector3(x,y,z), mass, radius, charge);
        }

        dynamicFields.Add(new Coloumb());
        dynamicFields.Add(new LennardJones());

        // staticFields.Add(new Wind());      
    }

    // Called once per frame
    void Update()
    {
        if (paused || manager.paused) return;
        // Static Field Contributions
        // List<Thread> threads = new List<Thread>();
        // foreach (Particle A in particles) {
        for (int a = 0; a < particles.Count; a++) {
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
        // Static Field Contributions
        foreach (StaticField F in staticFields) {
            F.applyForce(particles[a], scales);
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
    
    // adds new particle at given location with default parameters
    //  TODO: include mass, size, charge, colour, etc
    public void AddNewParticle(Vector3 pos, float mass = 1, float radius = 0.5f, int charge = 0) 
    {
        GameObject sphere = Instantiate(manager.particle, pos, Quaternion.identity);
        sphere.transform.parent = this.transform;
        particles.Add(new Particle(sphere, mass, radius, charge));
    }

    public void AddNewParticleRandom() {
        float z = rnd.Next(-10, 10);
        float x = rnd.Next(-10, 10);
        float y = rnd.Next(-10, 10);
        AddNewParticle(new Vector3(x,y,z));
    }

    private void RemoveParticle(Particle A) {
        Destroy(A.particle);
        particles.Remove(A);
    }

    public void togglePause() {
        paused = !paused;
    }

    [ContextMenu("Reset Scene")]
    private void reset(Simulator s) {
        // TODO - reset to another simulator state
    }
}
