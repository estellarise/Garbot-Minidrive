using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

/// <summary>
/// Based on RosPublisherExample code from Unity ROS tutorials
/// https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/publisher.md
/// 
/// Publishes Twist message based on joystick toggle
/// </summary>
public class JoystickTwistPublisher: MonoBehaviour
{
    ROSConnection ros;
    [HideInInspector] // hide b/c inspector makes its field content fixed (unable to change)
    public string topicName = "";
    public Joystick joystickLeft;
    public Joystick joystickRight;
    public RobotDropdown dropdown;
    public int numberOfRobots = 3; // change according to how many actual robots there are
    
    // Publish every N seconds
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    void Start() 
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        // register (fixed) number X robots beforehand 
        for (int i = 1; i < numberOfRobots + 1; i++)
        {
            topicName = "/bot_" + i.ToString() + "/cmd_vel";
            ros.RegisterPublisher<TwistMsg>(topicName);
            print("Registered: "+ topicName);
        }
    }

    private void Update()
    {
        if (!dropdown.currentRobot.Equals("")) // if current robot in dropdown is not blank
        {
            Publish();
        }
    }
    private void Publish()
    {
        topicName = "/" + dropdown.currentRobot + "/cmd_vel";
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


