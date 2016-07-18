using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereCommands : MonoBehaviour
{
    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        Console.log("已触发'OnSelect'事件");
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Console.log(string.Format("已碰撞物体:{0}", collision.gameObject.name));
        if (collision.gameObject.name.Equals("Notepad"))
        {
            SceneManager.LoadScene("cube");
        }
        //Debug.Log("the collision object name is：" + collision.gameObject.name);
    }
}