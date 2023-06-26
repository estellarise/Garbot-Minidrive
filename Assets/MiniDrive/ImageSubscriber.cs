using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

/// <summary>
/// Based on RosPublisherExample code from Unity ROS tutorials
/// Publishes twist msg based on joystick toggle
/// </summary>
public class ImagePublisher: MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "video";

    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<CompressedImageMsg>(topicName);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > publishMessageFrequency)
        {
            CompressedImageMsg videoMsg = new CompressedImageMsg();

            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, videoMsg);

            timeElapsed = 0;
        }
    }
}
