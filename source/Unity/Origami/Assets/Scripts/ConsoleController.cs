using UnityEngine;
using UnityEngine.UI;


public class ConsoleController : MonoBehaviour {

    private Text textComponent;
	// Use this for initialization
	void Start () {
        initTextComponent();
    }

    void setTextInConsole(string msg)
    {
        initTextComponent();
        if (!string.IsNullOrEmpty(msg))
        {
            textComponent.text = msg.ToString();
        }
    }

    private void initTextComponent()
    {
        if(textComponent == null)
        {
            textComponent = this.GetComponent<Text>();
        }
    }
}
