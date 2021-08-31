using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSystem: MonoBehaviour
{
    // Start is called before the first frame update

    // public Field[] fields = new Field[3];

    public Field field;

    private float dt = 1F;

    public GameObject spawner;

    List<Particle> particles = new List<Particle>();

    void Start()
    {
        field = new Field();
        for (int i = 0; i < 10; i++) {
            float x = Random.Range(-8,8);
            float z = Random.Range(-8,8);
            float y = Random.Range(2,18);

            particles.Add(new Particle(Instantiate(spawner, new Vector3(x,y,z), Quaternion.identity)));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Particle A in particles) {
            foreach (Particle B in particles) {
                A.applyForce(B.particle, dt);
            }
        }

        
    }
}
