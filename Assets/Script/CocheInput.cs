using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocheInput : MonoBehaviour
{
    CocheControl cocheControl;

    // Start is called before the first frame update
    void Start()
    {

        cocheControl= GetComponent<CocheControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        
        cocheControl.SetInputVector(inputVector);

    }
}
