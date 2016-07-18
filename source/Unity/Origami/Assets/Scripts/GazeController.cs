using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR.WSA.Input;

public class GazeController : MonoBehaviour {
    public static GazeController Instance { get; private set; }

    public GameObject FocusedObject { get; private set; }
    GestureRecognizer recognizer;
    // Use this for initialization
    void Start()
    {
        Instance = this;

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();        
    }

    void OnSelect()
    {
         SceneManager.LoadScene("Origami");
    }

        // Update is called once per frame
        void Update () {
        GameObject oldFocusObject = FocusedObject;
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            FocusedObject = null;
        }

        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();//cancel pending gestures
            recognizer.StartCapturingGestures();
        }
    }
}
