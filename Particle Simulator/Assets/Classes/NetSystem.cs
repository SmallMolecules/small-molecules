using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSystem: MonoBehaviour
{
    // Start is called before the first frame update

    // public Field[] fields = new Field[3];

    public Field field;
    public List<GameObject> particles = new List<GameObject>();

    private float dt = 0.001F;


    void Start()
    {
        field = new Field();
        foreach (GameObject g in particles) {
            float x = Random.Range(-10,10);

            float y = Random.Range(1,20);
            float z = Random.Range(-10,10);
            Instantiate(g, new Vector3(x,y,z), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject A in particles) {
            foreach (GameObject B in particles) {
                
            }
        }
        
    }
}
