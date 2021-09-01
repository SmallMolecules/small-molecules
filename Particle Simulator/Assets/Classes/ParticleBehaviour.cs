using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{

    public Rigidbody Rigidbody;

    

    public float charge;

    public int mass;

    void Awake() {
      
    }

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "particle")
      {
        
        // Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
      }
    }
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.useGravity = false; 

        var cubeRenderer = GetComponent<Renderer>();
        
        if (Random.Range(0f,1f) < 0.5f) {
          charge = 1f;
          cubeRenderer.material.SetColor("_Color", Color.red);
        }
        else {
          charge = -1f;
          cubeRenderer.material.SetColor("_Color", Color.blue);
        }

        // Rigidbody.AddForce(400, 400, 400);

        mass = 1;

    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
