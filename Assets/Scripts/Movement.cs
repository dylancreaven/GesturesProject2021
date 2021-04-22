using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public float speed=2f;
    public XRNode inputSource;
    public float gravity = -9.81f;
    private float fallingSpeed;
    public float additionalHeight =0.2f;
    public LayerMask groundLayer;

    private Vector2 inputAxis;
    private Vector2 lookAroundAxis;
    private XRRig rig;
    private CharacterController player;
    void Start()
    {
        
        rig = GetComponent<XRRig>();
        player = GetComponent<CharacterController>();
       
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);




    }
     private void FixedUpdate() {
        followHeadset();
        Quaternion headYaw = Quaternion.Euler(0,rig.cameraGameObject.transform.eulerAngles.y,0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x,0,inputAxis.y);
        // cause movement to change direction to where camera is looking
        player.Move(direction*Time.fixedDeltaTime*speed);


        //gravity stuff
        bool isGrounded = CheckIfGrounded();
        if(isGrounded)
            fallingSpeed=0;
        else
        {
            fallingSpeed +=gravity * Time.fixedDeltaTime;
        }
        player.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    private bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(player.center);
        float rayLength = player.center.y+0.01f;//margin of error
        bool hasHit = Physics.SphereCast(rayStart,player.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;


    }
    
    //player game object moves with vr headset
    void followHeadset(){
        player.height = rig.cameraInRigSpaceHeight+additionalHeight; 
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        player.center = new Vector3(capsuleCenter.x,player.height/2+player.skinWidth,capsuleCenter.z);


    }
}
