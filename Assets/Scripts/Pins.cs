using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pins : MonoBehaviour
{
    private int count = 0;

    bool isKnockedDown = false;
    public Transform groundCheck;
    public float groundDis = 0.4f;
    public LayerMask groundMask;
    public LayerMask laneMask;
    
    private void Start()
    {
       
        Bowling.pinKnockedDownCount = 0;
    }
    private void Update()
    {
     
        isKnockedDown = (Physics.CheckSphere(groundCheck.position, groundDis, groundMask) || Physics.CheckSphere(groundCheck.position, groundDis, laneMask));
        if (isKnockedDown)
        {

            if (count == 0)
            {
                Bowling.pinKnockedDownCount++;
                count++;
            }

        }
    }
}
