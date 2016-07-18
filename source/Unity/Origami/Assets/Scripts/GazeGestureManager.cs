using UnityEngine;
using UnityEngine.VR.WSA.Input;
public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager Instance { get; private set; }

    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;
	// Use this for initialization
	void Start () {
        Console.log("控制台信息将在此处显示");

        Instance = this;

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if(FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();
        Console.log("请点击球状物体");
    }
	
	// Update is called once per frame
	void Update () {
        GameObject oldFocusObject = FocusedObject;
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if(Physics.Raycast(headPosition,gazeDirection,out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            FocusedObject = null;
        }

        if(FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
	}
}
