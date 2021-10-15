using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

/** @brief Class for managing the properites of a simulator instance

    This class manages an overall simulation - the internal workings of what
    is considered a "box". This class holds the particles, the fields and the scales.
    @author Isaac Bergl
    @author Dhruv Jobanputra
    @date September 2021
    \see Simulator Scales
    */
public class Simulator : MonoBehaviour
{
    /**Set of scales used by the simulator. This object is referenced by other 
    objects such as particles and fields.
    /see Scales Scale*/
    public Scales scales = new Scales();

    /**Dictates if the simulation is paused. True if this simulation is paused, false otherwise*/
    public bool paused;

    /**Reference to the simulation manager
    /see SimulationManager*/
    private SimulationManager manager;

    /**The GameObject to spawn (Particle Object)*/
    public GameObject particle_spawner;

    /**The GameObject Environment to spawn (Box)*/
    public GameObject box_environment;

    /**List of the particles*/
    List<Particle> particles = new List<Particle>();
    /**List of the dynamic fields
    /see DynamicField*/
    List<DynamicField> dynamicFields = new List<DynamicField>();
    /**List of the static fields
    /see StaticField*/
    List<StaticField> staticFields = new List<StaticField>();

    /**System.Random object for random number generation. Each time program starts, a
    random seed is generated and used to construct this object*/
    private System.Random rand = new System.Random();
    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    */
    void Start()
    {
        // set reference to parent script
        manager = transform.parent.gameObject.GetComponent<SimulationManager>();
        GameObject box = Instantiate(box_environment, new Vector3(0, 0, 0), Quaternion.identity);
        box.transform.parent = this.transform;


        //creates random particles
        for (int i = 0; i < manager.NUM_PARTICLES; i++)
        {
            float x = rand.Next(-10, 10);
            float z = rand.Next(-10, 10);
            float y = rand.Next(-10, 10);

            float radius = Random.Range(1, 2);
            // float radius = 1f;
            // float mass = Random.Range(0, 10);
            float mass = 1f;
            int charge = (int)Random.Range(0, 3)-1;
            while (charge == 0) charge = (int)Random.Range(0, 3)-1;
            
            AddNewParticle(new Vector3(x,y,z), mass, radius, charge);
        }

        // add dynamic feilds
        dynamicFields.Add(new Coloumb(this));
        // dynamicFields.Add(new LennardJones());

        // add static fields
        // staticFields.Add(new Wind());      
    }

    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    */
    void Update()
    {
        if (paused || manager.paused) return;
        // List<Thread> threads = new List<Thread>();

        // for all particles
        for (int a = 0; a < particles.Count; a++)
        {
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
    /**Updates the velocity of the particle with an index of "a" in the list
    @param a - the index of the particle to update (int)*/
    private void updateVelocity(int a)
    {
        // Static Field Contributions
        foreach (StaticField F in staticFields)
        {
            F.applyForce(particles[a], scales);
        }

        // Dynamic Field Contributions
        foreach (DynamicField F in dynamicFields)
        {
            // only apply to particles after index as F.applyForce applies
            // force to both particles to save computation time
            for (int b = a + 1; b < particles.Count; b++)
            {
                // F.applyForce(particles[a], particles[b], scales);
                F.applyForce(particles[a], particles[b]);
            }
        }
    }

    /**Updates the positions of all the particles in the list according to thier velocity*/
    private void updatePositions()
    {
        foreach (Particle A in particles)
        {
            A.step(scales.time.VAL);
            A.checkBoxCollision();
            // A.boundaryCheck();
        }
    }

    /**Adds a new particle at a given position with the specified parameters
    @param pos (Vector3)
    @param mass (float)
    @param radius (float)
    @param charge (int)*/
    public void AddNewParticle(Vector3 pos, float mass = 1, float radius = 0.5f, int charge = 0)
    {
        GameObject sphere = Instantiate(particle_spawner, pos, Quaternion.identity);
        sphere.transform.parent = this.transform;
        particles.Add(new Particle(sphere, scales, mass, radius, charge));
    }

    /**Adds a particle at a random position with default physical properties
    /see AddNewParticle
    */
    public void AddNewParticleRandom()
    {
        float z = rand.Next(-10, 10);
        float x = rand.Next(-10, 10);
        float y = rand.Next(-10, 10);
        AddNewParticle(new Vector3(x, y, z));
    }

    /**Removes a particle, A, from the simulation
    @param A - the particle to remove (Particle)*/
    private void RemoveParticle(Particle A)
    {
        Destroy(A.particle);
        particles.Remove(A);
    }

    /**Toggles the pause state of the simulation*/
    public void togglePause()
    {
        paused = !paused;
    }

    /**Called by the UI elements to change the time scale
    @param coeff - the coefficient of the time scale (float)
    @param exp- the exponent of the time scale (int)*/
    public void updateTime(float coeff, int exp)
    {
        Debug.Log("Called");

        scales.setTime(coeff, exp);
        foreach (DynamicField d in dynamicFields)
        {
            d.updateConstants();
        }
        foreach (StaticField s in staticFields)
        {
            s.updateConstants();
        }
    }
    /**Called by the UI elements to change the length scale
    @param coeff - the coefficient of the length scale (float)
    @param exp- the exponent of the length scale (int)*/
    public void updateLength(float coeff, int index)
    {
        scales.setLength(coeff, index);
        foreach (DynamicField d in dynamicFields)
        {
            d.updateConstants();
        }
        foreach (StaticField s in staticFields)
        {
            s.updateConstants();
        }
    }
}
