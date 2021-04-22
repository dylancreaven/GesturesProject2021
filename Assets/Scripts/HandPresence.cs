using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    private InputDevice targetDevice;
    private GameObject spawnController;
    private GameObject spawnHandModel;
    public bool showController = false;
    private Animator handAnimator;
    void Start()
    {
        InitializeHands();
    }

    void InitializeHands()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);


        foreach (var device in devices)
        {
            Debug.Log(device.name + device.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find corresponding controller model");
                spawnController = Instantiate(controllerPrefabs[0], transform);
            }
            spawnHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnHandModel.GetComponent<Animator>();
        }

    }


    void UpdateHandAnimation()
    {

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            //Debug.Log("triggerValue of "+targetDevice.name+" => "+triggerValue);
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            //Debug.Log("gripValue of "+targetDevice.name+" => "+gripValue);
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (!targetDevice.isValid)
        {
            InitializeHands();
        }
        else
        {
            if (showController)
            {
                spawnHandModel.SetActive(false);
                spawnController.SetActive(true);
            }
            else
            {
                UpdateHandAnimation();
                spawnHandModel.SetActive(true);
                spawnController.SetActive(false);
            }
        }


    }
}
