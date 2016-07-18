using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour {

    private static Queue<String> msgQueue = new Queue<String>();
	
    public static void log(string msg)
    {
        string name = SceneManager.GetActiveScene().name;
        msg = string.Format("{0}-Info: {1}", name, msg);
        push(msg);
        GameObject consoleInfo = GameObject.Find("ConsoleInfo");
        if (consoleInfo)
        {
            consoleInfo.SendMessageUpwards("setTextInConsole", getAllMsg());
        }
    }
    public static void error(string msg)
    {
        string name = SceneManager.GetActiveScene().name;
        msg = string.Format("{0}-Error: {1}", name, msg);
        push(msg);
        GameObject consoleInfo = GameObject.Find("ConsoleInfo");
        if (consoleInfo)
        {
            consoleInfo.SendMessageUpwards("setTextInConsole", getAllMsg());
        }
    }

    private static void push(string msg)
    {
        if(msgQueue.Count < 4)
        {
            msgQueue.Enqueue(msg);
        }
        else
        {
            msgQueue.Dequeue();
            msgQueue.Enqueue(msg);
        }
    }

    private static string getAllMsg()
    {
        string returnValue = string.Empty;
        if (msgQueue.Count > 0)
        {
            returnValue = string.Join("\n", msgQueue.ToArray());
        }
        return returnValue;
    }
}
