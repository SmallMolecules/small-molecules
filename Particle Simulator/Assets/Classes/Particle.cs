using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    // private int charge = 1;

    GameObject body;

    void Awake() {

    }

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "particle")
      {
        
        Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
      }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
