//Create a new Dropdown GameObject by going to the Hierarchy and clicking Create>UI>Dropdown. Attach this script to the Dropdown GameObject.
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;

public class RobotDropdown : MonoBehaviour
{
    Dropdown m_Dropdown;
    public string currentRobot;
    public bool hasChanged = false;

    void Start()
    {
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
        //Initialise the Text to say the first value of the Dropdown
        currentRobot = m_Dropdown.options[m_Dropdown.value].text; // needed for initialization in joyTwistPub and ImageSub
    }

    //Update current robot field when value of Dropdown changes
    void DropdownValueChanged(Dropdown change)
    {
        m_Dropdown = change.GetComponent<Dropdown>();
        currentRobot = m_Dropdown.options[m_Dropdown.value].text;
        hasChanged = true;
    }
}