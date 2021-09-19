using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem: MonoBehaviour
{
    // Fields
    [SerializeField]
    private float dt = 0.001F;

    [SerializeField][Range(0,20)]
    private int NUM_PARTICLES = 10;

    public GameObject spawner;

    // lists of particles, dynamic fields and static fields
    List<Particle> particles = new List<Particle>();
    List<DynamicField> dynamicFields = new List<DynamicField>();
    List<StaticField> staticFields = new List<StaticField>();

    public System.Random ran = new System.Random();

    // Called once initially
    void Start()
    { 
        for (int i = 0; i < NUM_PARTICLES; i++) {
            float x = Random.Range(-8,8);
            float z = Random.Range(-8,8);
            float y = Random.Range(2,18);

            particles.Add(new Particle(Instantiate(spawner, new Vector3(x,y,z), Quaternion.identity)));
        }

        dynamicFields.Add(new Coloumb());
        staticFields.Add(new Wind());
        
    }

    // Called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)) {
            
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                if (particles.Count > 0) {
                int del = ran.Next(0, particles.Count-1);
                Destroy(particles[del].getGameObject());
                particles.RemoveAt(del);
                }
            }
            else {
                float x = Random.Range(-8,8);
                float z = Random.Range(-8,8);
                float y = Random.Range(2,18);
                particles.Add(new Particle(Instantiate(spawner, new Vector3(x,y,z), Quaternion.identity)));
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
}
