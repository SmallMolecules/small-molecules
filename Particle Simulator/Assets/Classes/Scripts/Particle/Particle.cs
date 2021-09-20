using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle 
{ 
    private GameObject particle;
    
    // attributes
    public float mass;
    public float radius;
    public int charge;

    private Vector3 velocity;

    // Variable particle - default particle is handled by ParticleSystem
    public Particle(GameObject p, float setMass, float setRad, int setCharge) {
        particle = p;
        velocity = new Vector3(0.0f, 0.0f, 0.0f);

        var cubeRenderer = particle.GetComponent<Renderer>();
        
        // assign charge randomly and give colour
        if (setCharge < 0) {
          cubeRenderer.material.SetColor("_Color", Color.red);
        }
        else if (setCharge > 0){
          cubeRenderer.material.SetColor("_Color", Color.blue);
        }
        else {
            cubeRenderer.material.SetColor("_Color", Color.gray);
        }

        var myCollider = particle.GetComponent<SphereCollider>();

        myCollider.radius = setRad;
        mass = setMass;
        charge = setCharge;

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

    public GameObject getGameObject() {
        return particle;
    }

    

}
