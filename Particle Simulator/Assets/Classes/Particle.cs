using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle 
{
    public GameObject particle;

    private float k = 300f;

    // public Vector3 velocity;

    public Particle(GameObject p) {
        particle = p;
        // velocity = new Vector3(0f,0f,0f);
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
