using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;
using UnityEngine.UI;

/// <summary>
/// Based on RosPublisherExample code from Unity ROS tutorials
/// Publishes twist msg based on joystick toggle
/// </summary>


public class ImageSubscriber : MonoBehaviour
{
    public Texture2D VideoTexture2D; 
    public Image imageVideo;
    void Start()
    {
        VideoTexture2D = new Texture2D(2, 2);
        ROSConnection.GetOrCreateInstance().Subscribe<CompressedImageMsg>("esp32_img/compressed", ImageChange); 
            // moving to update freezes connection, subscriber inherently a loop, no need to loop it
    }

    void ImageChange(CompressedImageMsg imageMessage)
    {
        //imageVideo.image = ImageConversion.LoadImage(imageVideo, imageMessage.data);
        // ImageConversion.LoadImage(VideoTexture2D, imageMessage.data);
        VideoTexture2D.LoadImage(imageMessage.data);
        Sprite newImage = Sprite.Create(VideoTexture2D, new Rect(2,2,2, 2), Vector2.zero);
        imageVideo.sprite = newImage;
        //GetComponent<Renderer>().material.mainTexture = VideoTexture2D;
        //cube.GetComponent<Renderer>().material.color = new Color32((byte)colorMessage.r, (byte)colorMessage.g, (byte)colorMessage.b, (byte)colorMessage.a);
    }
}
