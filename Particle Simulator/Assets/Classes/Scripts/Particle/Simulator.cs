using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

// class for a simulator 
public class Simulator  : MonoBehaviour
{
    // class of local scales
    public Scales scales = new Scales();

    public bool paused;

    // reference to parent
    private SimulationManager manager;

    // reference to object to spawn
    public GameObject particle_spawner;

    // lists of particles, dynamic fields and static fields
    List<Particle> particles = new List<Particle>();
    List<DynamicField> dynamicFields = new List<DynamicField>();
    List<StaticField> staticFields = new List<StaticField>();

    // seeded random number generator 
    private System.Random rnd = new System.Random(9);

    // Called once initially
    void Start()
    { 
        // set reference to parent script
        manager = transform.parent.gameObject.GetComponent<SimulationManager>();
       
        //creates random particles
        for (int i = 0; i < manager.NUM_PARTICLES; i++) {
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

        // add dynamic feilds
        dynamicFields.Add(new Coloumb());
        // dynamicFields.Add(new LennardJones());

        // add static fields
        // staticFields.Add(new Wind());      
    }

    // Called once per frame - commented out is the multithreaded implementation
    void Update()
    {
        if (paused || manager.paused) return;    
        // List<Thread> threads = new List<Thread>();

        // for all particles
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

    // applies forces
    private void updateVelocity(int a) {
        // Static Field Contributions
        foreach (StaticField F in staticFields) {
            F.applyForce(particles[a], scales);
        }

        // Dynamic Field Contributions
        foreach (DynamicField F in dynamicFields) {
            // only apply to particles after index as F.applyForce applies
            // force to both particles to save computation time
            for (int b = a+1; b < particles.Count; b++) {
                F.applyForce(particles[a], particles[b], scales);
            }
        }
    }

    // updates positions of particles by dt
    private void updatePositions() {
        foreach (Particle A in particles) {
            A.step(scales.time.VAL);
        }
    }
    
    // adds new particle at given location with default parameters
    public void AddNewParticle(Vector3 pos, float mass = 1, float radius = 0.5f, int charge = 0) 
    {
        GameObject sphere = Instantiate(particle_spawner, pos, Quaternion.identity);
        sphere.transform.parent = this.transform;
        particles.Add(new Particle(sphere, scales, mass, radius, charge));
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

    // pauses/unpauses the game
    public void togglePause() {
        paused = !paused;
    }
    


}
