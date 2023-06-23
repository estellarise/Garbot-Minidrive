using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

/// <summary>
/// Based on RosPublisherExample code from Unity ROS tutorials
/// Publishes twist msg based on joystick toggle
/// </summary>
public class JoystickTwistPublisher: MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "cmd_vel";
    public Joystick joystickLeft;
    public Joystick joystickRight;
    
    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<TwistMsg>(topicName);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > publishMessageFrequency)
        {
            TwistMsg joystickTwist = new TwistMsg(
                new Vector3Msg(joystickLeft.Vertical, 0, 0), // x corresponds to joystick's vertical displacement
                new Vector3Msg(0, 0, joystickRight.Horizontal) // angZ corresponds to R joystick's horizontal displacement
            );

            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, joystickTwist);

            timeElapsed = 0;
        }
    }
}
