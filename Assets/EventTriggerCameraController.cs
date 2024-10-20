using Cinemachine;
using UnityEngine;

public class EventTriggerCameraController : MonoBehaviour
{
    public GameObject eventTriggerCamera;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetEventTriggeredCamera(GameObject eventTriggerCamera)
    {
        this.eventTriggerCamera = eventTriggerCamera;
    }
    public void SetTriggerCamPriority(int priority)
    {
        eventTriggerCamera.GetComponent<CinemachineVirtualCamera>().Priority = priority;
    }
}
