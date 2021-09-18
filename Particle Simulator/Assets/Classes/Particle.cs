using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle 
{
<<<<<<< Updated upstream
    public GameObject particle;
=======
    public float charge;
    public float mass;
    public float radius;
>>>>>>> Stashed changes

    private float k = 300f;

    // public Vector3 velocity;

    public Particle(GameObject p, float setRad, float setMass) {
        particle = p;
<<<<<<< Updated upstream
        // velocity = new Vector3(0f,0f,0f);
=======
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

        var myCollider = particle.GetComponent<SphereCollider>();

        myCollider.radius = setRad;
        mass = setMass;
>>>>>>> Stashed changes
    }

    public void applyForce(GameObject B, float dt) {
        Vector3 F = coloumb(B);
        particle.GetComponent<Rigidbody>().AddForce(F*dt);
    }

    
    public Vector3 coloumb(GameObject B) {

        int q1 = B.GetComponent<ParticleBehaviour>().charge;
        int q2 = particle.GetComponent<ParticleBehaviour>().charge;

        float r = Vector3.Distance(particle.transform.position, B.transform.position);

        if (r < 1) {
            r = 1;
        }


        float con = k*q1*q2/r;

        return con*Vector3.Normalize(particle.transform.position - B.transform.position);


    }
}
