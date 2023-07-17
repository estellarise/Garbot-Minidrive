# Trashbot-Minidrive
This repository contains a Unity Project that serves as a User Interface to move a mini robot and see from the robot's perspective via video. It does so by 
publishing Twist messages and listening to compressed images from an ESP32.

It does not include ESP32 code setup.
<br>(Currently have not tested switching between multiple video feeds.)

## Setup
- Requirements / What this works on (you're welcome to try other setups, I only know that this one works)
  - Ubuntu 22.04
  - [ROS 2 Humble](https://docs.ros.org/en/humble/Installation/Ubuntu-Install-Debians.html): Do all instructions up to and including ```sudo apt install ros-humble-desktop```
  - [Unity 2020.3.45f1](https://stackoverflow.com/questions/73378850/how-can-i-install-unity-hub-on-ubuntu-22-04): Use first answer
  
- Components
  - Software
    - Clone this project :) {Make script to automate even further}
    - [Unity-ROS Bridge Github](https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/README.md)
    - [Unity-ROS TCP](https://github.com/Unity-Technologies/ROS-TCP-Connector/blob/main/com.unity.robotics.visualizations/Visualizations.md)
    - [Unity Joystick Pack](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631): 
      - Add to Package Manager {add screenshots on how to do this in Unity}
    - [MicroROS Humble](https://github.com/micro-ROS/micro_ros_setup/tree/humble#quick-start): Follow "Building", skip anything with "firmware"
      
  - Hardware (Incomplete List)
    - ESP32-CAM + Power Supply
    - Tiny trash can
    - Android Phone + USB Cord
    - Motor Controller
    - Mini Wheels

- Helpful Links
  - [Build setup from Unity to Android](https://www.youtube.com/watch?v=Nb62z3J4A_A)

## Run
- MicroROS
  - Follow "Building micro-ROS-Agent"
  - Replace the last command with
         ```ros2 run micro_ros_agent micro_ros_agent udp4 --port 8888```
- Unity ROS Bridge
  ```
    source /opt/ros/humble/setup.bash
    source install/setup.bash
    hostname -I
  ```
    Check IP, starts w/ 192.168.x.xxx {replace w/ env var's later?}
  ```
  ros2 run ros_tcp_endpoint default_server_endpoint --ros-args -p ROS_IP:=192.168.<x.xx> -p ROS_TCP_PORT:=11311
  ```
- Unity: open Project and press the play button
- Power on ESP32
- You should see live video feed from the camera and be able to use the joysticks to control the motors :)
  
