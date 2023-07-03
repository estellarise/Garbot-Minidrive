using UnityEngine;
using UnityEngine.UI;

public class RobotSelectionDropdown : MonoBehaviour
{
    [SerializeField]
    Text textBox;

    public JoystickTwistPublisher joyTwistPublisher;

    public string valueText = "";
    Dropdown m_Dropdown;

    void Start()
    {
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });

        //Initialise the Text to say the first value of the Dropdown
        //m_Text.text = "First Value : " + m_Dropdown.value;
        //joyTwistPublisher.topicName = m_Dropdown.value + "/cmd_vel";
    }

    //Ouput the new value of the Dropdown into joyStickPublisher
    void DropdownValueChanged(Dropdown change)
    {
        joyTwistPublisher.topicName = change.value + "/cmd_vel";
    }
    
 /*
    public void ChangeLocationDropDown()
    {
        Debug.Log("DROP DOWN CHANGED");
        Dropdown m_Dropdown;
        m_Dropdown = GetComponent();
        Debug.Log(m_Dropdown.options[m_Dropdown.value].text);


        //store in variable
        valueText = m_Dropdown.options[m_Dropdown.value].text;
        //set textbox value
        textBox.text = valueText;
    }
    */
}