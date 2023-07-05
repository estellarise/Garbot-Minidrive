using UnityEngine;
using UnityEngine.UI;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

/// <summary>
/// Based on RosPublisherExample code from Unity ROS tutorials
/// Publishes twist msg based on joystick toggle
/// </summary>


public class ImageSubscriber : MonoBehaviour
{
    // [HideInInspector]
    public Texture2D VideoTexture2D; 
    public Image imageVideo;
    private int espCamWidth = 320;
    private int espCamHeight = 240;
    public TestDropdown Dropdown;
    //private ROSConnection ros = new ROSConnection();
    public ROSConnection ros;
    public string topicName;
    private string newTopicName;
    void Start()
    {
        VideoTexture2D = new Texture2D(espCamWidth, espCamHeight);
        topicName = "/bot_1" + "/esp32_img/compressed";
        ROSConnection.GetOrCreateInstance().Subscribe<CompressedImageMsg>(topicName, ImageChange);
        // moving to update freezes connection, subscriber inherently a loop, no need to loop it
        
    }
    
    void Update()
    {
        if (Dropdown.hasChanged)
        {
            //print("hello");
            newTopicName = Dropdown.currentRobot + "/esp32_img/compressed";
            //print(newTopicName);
            //print(topicName);
            ros.Unsubscribe(topicName);
            ROSConnection.GetOrCreateInstance().Subscribe<CompressedImageMsg>(newTopicName, ImageChange);
            topicName = newTopicName;
            Dropdown.hasChanged = false;
        }
    }

    void ImageChange(CompressedImageMsg imageMessage)
    {
        VideoTexture2D.LoadImage(imageMessage.data);
        Sprite newImage = Sprite.Create(VideoTexture2D, new Rect(0,0,espCamWidth, espCamHeight), Vector2.zero);
        imageVideo.sprite = newImage;
    }
}
