using UnityEngine;
using UnityEngine.UI;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

/// <summary>
/// Based on RosSubscriberExample code from Unity ROS tutorials
/// https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/subscriber.md
/// Listens for compressed image msg from ESP-32 camera to emulate a video feed
/// </summary>


public class ImageSubscriber : MonoBehaviour
{
    [HideInInspector]
    public Texture2D VideoTexture2D; 
    public Image imageVideo; // Image object where we place the "video" on
    private int espCamWidth = 320;
    private int espCamHeight = 240;
    public RobotDropdown robotDropdown;
    public ROSConnection ros; // needed, add prefab, else object reference is empty
    public string topicName;
    private string newTopicName;
    void Start()
    {
        VideoTexture2D = new Texture2D(espCamWidth, espCamHeight);
        topicName = "/bot_1/esp32_img/compressed";
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<CompressedImageMsg>(topicName, ImageChange);
    }
    
    void Update()
    {
        if (robotDropdown.hasChanged)
        {
            newTopicName = robotDropdown.currentRobot + "/esp32_img/compressed";
            ros.Unsubscribe(topicName);
            ros.Subscribe<CompressedImageMsg>(newTopicName, ImageChange);
            topicName = newTopicName;
            robotDropdown.hasChanged = false;
        }
    }

    /* Image object on Unity screen. Place sprite on image, update constantly */
    void ImageChange(CompressedImageMsg imageMessage)
    {
        VideoTexture2D.LoadImage(imageMessage.data); // load compressed image onto 2D Texture
        Sprite newImage = Sprite.Create(VideoTexture2D, new Rect(0,0,espCamWidth, espCamHeight), Vector2.zero);
        imageVideo.sprite = newImage;
    }
}
