using UnityEngine;
using UnityEngine.UI;

public class RobotSelectionDropdown : MonoBehaviour
{
    public JoystickTwistPublisher joyTwistPublisher;
    Dropdown m_Dropdown;

    void Start()
    {
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
    }

    //Output the new value of the Dropdown into joyStickPublisher
    void DropdownValueChanged(Dropdown change)
    {
        joyTwistPublisher.topicName = change.value + "/cmd_vel";
    }
}