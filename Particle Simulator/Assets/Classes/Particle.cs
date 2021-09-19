using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle 
{
    private GameObject particle;
    
    public float mass;
    public float radius;
    public float charge;

    private Vector3 velocity;

    // Default particle
    public Particle(GameObject p) {
        particle = p;

        var cubeRenderer = particle.GetComponent<Renderer>();
        
        // assign charge randomly and give colour
        if (Random.Range(0f,1f) < 0.5f) {
          charge = 1f;
          cubeRenderer.material.SetColor("_Color", Color.red);
        }
        else {
          charge = -1f;
          cubeRenderer.material.SetColor("_Color", Color.blue);
        }

        mass = 100f;
        radius = 10f;

        particle.transform.localScale = new Vector3(radius/2, radius/2, radius/2);
    }

    // Variable particle
    public Particle(GameObject p, float setRad, float setMass) {
        particle = p;

        var cubeRenderer = particle.GetComponent<Renderer>();
        
        // assign charge randomly and give colour
        if (Random.Range(0f,1f) < 0.5f) {
          charge = 1f;
          cubeRenderer.material.SetColor("_Color", Color.red);
        }
        else {
          charge = -1f;
          cubeRenderer.material.SetColor("_Color", Color.blue);
        }

        var myCollider = particle.GetComponent<SphereCollider>();

        myCollider.radius = setRad;
        mass = setMass;

        particle.transform.localScale = new Vector3(setRad/2, setRad/2, setRad/2);

    }

    // getter method to get position (and protect var particle)
    public Vector3 getPos() {
        return particle.transform.position;
    }

    // add force contribution to vecolicty
    public void addFoce(Vector3 F) {
        velocity += F/mass; //NOTE: mass is currently 1
    }

    // update position of gameobject according to velocity
    public void step(float dt) {
        particle.transform.Translate(velocity*dt);
    }

    

}
