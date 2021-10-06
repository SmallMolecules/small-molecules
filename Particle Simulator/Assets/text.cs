using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        Text text = canvasGameObject.AddComponent<Text>();
        text.text = textString;

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        text.material = ArialFont.material;

        return text;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = textString;
    }
}
