using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class for managing the properites of particle objects

    This class manages particle properties such as position, velocity, radius, mass,
    charge and GameObject is is binds to.
    @author Isaac Bergl
    @date September 2021
    \see Simulator Scales
    */
public class Particle 
{ 
    /**The GameObject that this object is dictating*/
    public GameObject particle;
    
    /**Mass of the particle. Min value of 1.0*/
    public float mass;
    /**Radius of the particle. Min value of 1.0*/
    public float radius;
    /**Charge of the particle. Min value of 1*/
    public int charge;

    /**Vector velocity of the particle. Dictates the position.*/
    private Vector3 velocity;
    /**Vector position of the particle. Dictates the GameObject position */
    private Vector3 position;

    /**Reference to the Scales of the parent Simulator*/
    public Scales scales;

    /**
    Constructor for a particle. Sets the GameObject to be the one given in as a parameter, 
    as well as passing a reference to the Scales, s, and the physical properties.
    @param p (GameObject)
    @param S (Scales)
    @param setMass (float)
    @param setRadius (float)
    @param setCharge (int)
    */
    public Particle(GameObject p, Scales S, float setMass, float setRadius, int setCharge) {
        particle = p;
        position = p.transform.position;
        velocity = new Vector3(0.0f, 0.0f, 0.0f);

        scales = S;
        
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

        myCollider.radius = setRadius;
        mass = setMass;
        charge = setCharge;
        radius = setRadius;

        particle.transform.localScale = new Vector3(setRadius*2, setRadius*2, setRadius*2);
    }

    /**
    Gets the position of the particle
    @returns position (Vector3)
    */
    public Vector3 getPos() {
        return position;
    }

    /**
    Gets the velocity of the particle
    @returns velocity (Vector3)
    */
    public Vector3 getVel() {
        return velocity;
    }

    /**
    Adds a force to the particle 
    @rparam Unity unit scaled force F (Vector3)
    */
    public void addForce(Vector3 F) {
                
        velocity += F/mass; //NOTE: mass is currently 1
    }

    /**
    Steps the particle position and moves the GameObject accordin to
    its velocity.
    @param timestep  (float)
    */
    public void step(float dt) {

        position += velocity*dt;

        particle.transform.Translate(velocity*dt);
    }

    public void checkBoxCollision() {
        bool collideRight = Physics.Raycast(particle.transform.position, Vector3.right, radius);
        bool collideLeft = Physics.Raycast(particle.transform.position, Vector3.left, radius);
        bool collideUp = Physics.Raycast(particle.transform.position, Vector3.up, radius);
        bool collideDown = Physics.Raycast(particle.transform.position, Vector3.down, radius);
        bool collideForward = Physics.Raycast(particle.transform.position, Vector3.forward, radius);
        bool collideBack = Physics.Raycast(particle.transform.position, Vector3.back, radius);

        float vx = velocity.x;
        float vy = velocity.y;
        float vz = velocity.z;

        if (collideRight || collideLeft) {
            velocity.x = -vx;

        } else if (collideUp || collideDown) {
            velocity.y = -vy;

        } else if (collideForward || collideBack) {
            velocity.z = -vz;
        }
    }


}
