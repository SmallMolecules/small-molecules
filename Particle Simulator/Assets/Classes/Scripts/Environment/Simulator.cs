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

    /**Specifies if destroy mode has been activated*/
    public bool destroy = false;

    /**The actual box environment object for the current simulation*/
    public GameObject box;

    /**Reference to the simulation manager
    /see SimulationManager*/
    private SimulationManager manager;

    /**The GameObject to spawn (Particle Object)*/
    public GameObject particleSpawner;

    /**The GameObject Environment to spawn (Box)*/
    public GameObject boxEnvironment;

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
    // private System.Random rand;
    /**The seed used to generate this simulator's random object*/
    public int seed;

    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    */

    /**Ratio of wall thickness to length of the inside of the sides is 1:40
    Ratio of unity scale to unity length is 1:10
    The values are obtained from the box prefab. Changing the constant will not change 
    the box prefab thickness
    */
    public float BOX_THICKNESS_SCALE = 0.025f;
    public float BOX_LENGTH_SCALE = 10;
    public float boxLength;
    public float wallThickness;

    private System.Random rand = new System.Random(9);
    void Start()
    {
        manager = transform.parent.gameObject.GetComponent<SimulationManager>();
        box = Instantiate(boxEnvironment, new Vector3(0, 0, 0), Quaternion.identity);
        box.transform.parent = this.transform;
        UpdateBoxSize(box.transform.localScale.x);


        for (int i = 0; i < manager.NUM_PARTICLES; i++)
        {

            float radius = Random.Range(1, 2);
            float mass = Random.Range(1, 2);
            int charge = (int)Random.Range(0, 3) - 1;

            AddNewParticle(generateRandomCoords(radius), mass, radius, charge);
        }

        // dynamicFields.Add(new Coloumb(this));
        dynamicFields.Add(new Coloumb(this));
    }

    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    */
    void Update()
    {
        if (paused || manager.paused)
        {
            if (destroy) HandleDestroyParticle();
            return;
        }

        for (int a = 0; a < particles.Count; a++)
        {
            UpdateVelocity(a);
        }

        UpdatePositions();
    }
    /**Updates the velocity of the particle with an index of "a" in the list
    @param a - the index of the particle to update (int)*/
    private void UpdateVelocity(int a)
    {
        foreach (StaticField F in staticFields)
        {
            F.ApplyForce(particles[a]);
        }

        foreach (DynamicField F in dynamicFields)
        {
            for (int b = a + 1; b < particles.Count; b++)
            {
                F.ApplyForce(particles[a], particles[b]);
            }
        }
    }

    /**Updates the positions of all the particles in the list according to thier velocity*/
    private void UpdatePositions()
    {
        foreach (Particle A in particles)
        {
            checkOutOfBounds(A);
            A.CheckBoxCollision();
            A.Step();
        }
    }

    /**Adds a new particle at a given position with the specified parameters
    @param pos (Vector3)
    @param mass (float)
    @param radius (float)
    @param charge (int)*/
    public void AddNewParticle(Vector3 pos, float mass = 1, float radius = 0.5f, int charge = 0)
    {
        GameObject sphere = Instantiate(particleSpawner, pos, Quaternion.identity);
        sphere.transform.parent = this.transform;
        particles.Add(new Particle(sphere, scales, mass, radius, charge));
    }

    /**Adds a particle at a random position with default physical properties
    /see AddNewParticle
    */
    public void AddNewParticleRandom()
    {
        AddNewParticle(generateRandomCoords());
    }

    /**Called by the UI elements to change the time scale
    @param coeff - the coefficient of the time scale (float)
    @param exp - the exponent of the time scale (int)*/
    public void UpdateTime(float coeff, int exp)
    {
        scales.SetTime(coeff, exp);
    }

    /**Called by the UI elements to change the length scale
    @param coeff - the coefficient of the length scale (float)
    @param exp - the exponent of the length scale (int)*/
    public void UpdateLength(float coeff, int index)
    {
        scales.SetLength(coeff, index);
    }

    /**Removes a particle, A, from the simulation
    @param A - the particle to remove (Particle)*/
    private void RemoveParticle(Particle A)
    {
        Destroy(A.particle);
        particles.Remove(A);
    }

    /**Toggles the pause state of the simulation*/
    public void TogglePause()
    {
        paused = !paused;
    }

    /**Checks if a particle if clicked to destroy
    and calls RemoveParticle()*/
    private void HandleDestroyParticle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << 6;
            hits = Physics.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, layerMask);
            bool deleted = false;
            for (int a = 0; a < particles.Count; a++)
            {
                for (int h = 0; h < hits.Length; h++)
                {
                    if (hits[h].transform.position == particles[a].particle.transform.position)
                    {
                        if (!deleted)
                        {
                            RemoveParticle(particles[a]);
                            deleted = true;
                        }
                    }
                }
            }
        }
    }

    /**Toggles the destroy particle option of the simulation
    @param set - a bool to set destroy to true or false
    */
    public void ToggleDestroy(bool set)
    {
        destroy = set;
    }

    /**Called by the UI elements to change the size of the box
    @param coeff - the coefficient of the size scale (float)
    */
    public void UpdateBoxSize(float coeff)
    {
        box.transform.localScale = new Vector3(coeff, coeff, coeff);
        boxLength = box.transform.localScale.x * BOX_LENGTH_SCALE;
        wallThickness = boxLength * BOX_THICKNESS_SCALE;

    }

    /**Generates a random coordinate that is inside the bounds of the box
    @param radius - the size of the particle to be added
    */
    private Vector3 generateRandomCoords(float radius = 1f)
    {
        float halfLength = boxLength / 2 - radius;
        float fullLength = boxLength - radius;
        float minimum = wallThickness + radius;

        float x = rand.Next((int)Mathf.Ceil(-halfLength), (int)Mathf.Floor(halfLength));
        float y = rand.Next((int)Mathf.Ceil(minimum), (int)Mathf.Floor(fullLength));
        float z = rand.Next((int)Mathf.Ceil(minimum), (int)Mathf.Floor(fullLength));

        return new Vector3(x, y, z);
    }

    /**Checks if the particle is outside the bounds 
    of the boxand puts it back in
    @param p - the particle being checked
    */
    public void checkOutOfBounds(Particle p)
    {
        Vector3 pos = p.particle.transform.position;
        float radius = p.radius;

        float halfLength = boxLength / 2 - radius;
        float fullLength = boxLength + wallThickness - radius;
        float minimum = wallThickness + radius;

        float x = pos.x;

        if (pos.x < -halfLength)
        {
            x = -halfLength;
        }
        if (pos.x > halfLength)
        {
            x = halfLength;
        };

        float y = pos.y;
        if (pos.y < minimum)
        {
            y = minimum;
        }

        if (pos.y > fullLength)
        {
            y = fullLength;
        }

        float z = pos.z;
        if (pos.z < minimum)
        {
            z = minimum;
        }

        if (pos.z > fullLength)
        {
            z = fullLength;
        }

        // For some reason this block of code doesn't work even though it should be the same thing as above
        // x = Mathf.Max(pos.x, -halfLength);
        // x = Mathf.Min(pos.x, halfLength);

        // y = Mathf.Max(pos.y, minimum);
        // y = Mathf.Min(pos.y, fullLength);

        // z = Mathf.Max(pos.z, minimum);
        // z = Mathf.Min(pos.z, fullLength);

        p.particle.transform.position = new Vector3(x, y, z);

    }


}