using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle 
{
    public float charge;
    public int mass;

    private Vector3 velocity;
    private GameObject particle;

    public Particle(GameObject p) {
        particle = p;
        velocity = new Vector3(0.0f,0.0f,0.0f);

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
        mass = 1;
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
