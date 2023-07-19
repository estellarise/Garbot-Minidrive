using System;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

namespace MiniDrive
{
    public class JoystickPointPublisher:MonoBehaviour
    {
         ROSConnection ros;
            [HideInInspector] // hide b/c inspector makes its field content fixed (unable to change)
            public string topicName;
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
                    topicName = "/person_bot_" + i.ToString() + "/cmd_vel";
                    ros.RegisterPublisher<PointMsg>(topicName);
                    print("Registered: "+ topicName);
                }
            }
        
            private void Update()
            {
                Publish();
            }
            private void Publish()
            {
                topicName = "/" + dropdown.currentRobot + "/cmd_vel";
                timeElapsed += Time.deltaTime;
                if (timeElapsed > publishMessageFrequency)
                {
                    PointMsg joystickPointMsg = new PointMsg(
                        ConvertToVelocityInt(joystickLeft.Horizontal), // turn left or right (velocity)
                        ConvertToVelocityInt(joystickRight.Vertical), // raise or lower arm (velocity)
                        0
                    );
        
                    // Finally send the message to server_endpoint.py running in ROS
                    ros.Publish(topicName, joystickPointMsg);
        
                    timeElapsed = 0;
                }
            }
            // Send to Dynamixel motor values in 0-2046 (motors handle 0 - 2047, not important to cover the full range here)
            // -2047 ~ 2047
            private float ConvertToVelocityInt(float vel)
            {
                //return (float)(Math.Ceiling(vel * 1023) + 1023);
                return (float)Math.Ceiling(vel * 2047);
            }
    }
}