using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocheInput : MonoBehaviour
{
    CocheControl cocheControl;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
            cocheControl = GetComponent<CocheControl>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<PhotonView>().IsMine)
        {
            Vector2 inputVector = Vector2.zero;

            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            cocheControl.SetInputVector(inputVector);
        }

    }
}
