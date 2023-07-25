# Garbot-Minidrive
This repository contains a Unity Project that serves as a User Interface to move a mini robot and see from the robot's perspective via video. It does so by 
publishing Twist messages and listening to compressed images from an ESP32.

The repo does not include ESP32 code setup.

## Setup
- Requirements / What this works on (you're welcome to try other setups, I only know that this one works)
  - Ubuntu 22.04
  - [ROS 2 Humble](https://docs.ros.org/en/humble/Installation/Ubuntu-Install-Debians.html): Do all instructions up to and including ```sudo apt install ros-humble-desktop```
  - [Unity 2020.3.45f1](https://stackoverflow.com/questions/73378850/how-can-i-install-unity-hub-on-ubuntu-22-04): Use link if installing Unity on Ubuntu, use first answer
  
- Components
  - Software
    - Clone this project into its own folder :)
    - [Unity-ROS Bridge Github](https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/README.md): Clone into the same folder, follow [Setup](https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/setup.md)
    - [Unity-ROS TCP Visualization](https://github.com/Unity-Technologies/ROS-TCP-Connector/blob/main/com.unity.robotics.visualizations/Visualizations.md): Add to Unity package manager (see Notes for instructions)
    - [Unity Joystick Pack](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631): Add to Unity Package Manager (See Notes for guidance)
    - [MicroROS Humble](https://github.com/micro-ROS/micro_ros_setup/tree/humble#quick-start): Follow "Building", skip any section headers with "firmware"
      
  - Hardware (Incomplete List)
    - ESP32-CAM + Power Supply
    - Tiny trash can
    - Android Phone + USB Cord
    - Motor Controller
    - Mini Wheels

- Helpful Links
  - [Build setup from Unity to Android](https://www.youtube.com/watch?v=Nb62z3J4A_A)
    - Build one scene at a time depending on which interface is needed. Dark blue is trash robot, cyan is person robot.

## Run
- MicroROS
  - Code from "Building micro-ROS-Agent" of the MicroROS package (see above):
  ```
  ros2 run micro_ros_setup create_agent_ws.sh
  ros2 run micro_ros_setup build_agent.sh
  source install/local_setup.sh
  ros2 run micro_ros_agent micro_ros_agent udp4 --port 8888
  ```

- Unity ROS Bridge
  ```
    source /opt/ros/humble/setup.bash
    source install/setup.bash
    hostname -I
  ```
    Replace ROS_IP below w/ the local IP of the host machine, starts w/ 192.168.x.xxx
  ```
  ros2 run ros_tcp_endpoint default_server_endpoint --ros-args -p ROS_IP:=192.168.<x.xxx> -p ROS_TCP_PORT:=11311
  ```
- Unity: open Project and press the play button
- Power on ESP32
- You should see live video feed from the camera and be able to use the joysticks to control the motors :)
  
## Configurations
- Specify number of robots in JoystickPointPublisher.cs and JoystickTwistPublisher.cs
- Naming Conventions:

  | Interface    | Image Subscriber                  | Twist/Point Publisher             |
  |--------------|-----------------------------------|-----------------------------------|
  | Minibot      | /person_bot_1/esp32_img/compressed| /person_bot_1/cmd_vel             |
  | MiniTrashBot | /bot_1/esp32_img/compressed       | /bot_1/cmd_vel                    |

## Notes
- To specify the number of robots beforehand, change number of robots in joystick [twist/point] corresponding field.
- Future Directions
  - Two devices publishing twists to the same robot crashes host. Implement a lock to ensure only one set of cameras and motors are connected to one device at a time.
  - Left-dominant mode
  - Allow user to input IP for ROS Connection (similar to Robotics Window, but don't need to re-build project every time remote host changes)
  - Script dropdown so adding robots is automated (currently have to manually add options to Unity dropdown)
  - Attach twist publisher to point publisher to make only one variable control # of robots
  - Make a button that toggles b/w person mode and trash bot mode
- Overarching structure
  - use the same wifi, or else ROS and Unity cannot see each other
  - ensure no docker containers are using the port for ROS
  - can add source /opt/... to bashrc file to circumvent the need for that step.
- ROS
  - once one type of msg publishes to a topic, the topic will always expect that type of msg
  - Cmake, msgs and topics: need to be declared in both Ros AND Unity
    - suggestion to enable the above: make Github and clone once to each object
- Unity-ROS
  - [Tutorials starting point](https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/README.md)
- Unity
  - Package Manager via Git URL: Window -> Package Manager -> Follow these [official instructions](https://docs.unity3d.com/2020.3/Documentation/Manual/upm-ui-giturl.html)
  - Assets: Window -> Package Manager -> Packages: My Assets
    - add them externally first via link
    - import, should be ready for use after
  - Enable "USB Debugging Mode" after enabling developer mode to install interface
  - Input fields are "sticky", they retain input if you manually give input (whether through script or via Unity editor)
