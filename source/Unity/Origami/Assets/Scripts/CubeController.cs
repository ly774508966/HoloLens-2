using UnityEngine;

public class CubeController : MonoBehaviour {


    private float stepLength = 100f;
    void OnSelect()
    {
        Console.log("已触发'OnSelect'事件");
        Vector3 v;
        v.x = this.gameObject.transform.localScale.x;
        v.y = this.gameObject.transform.localScale.y + 0.2F;
        v.z = this.gameObject.transform.localScale.z;
        this.gameObject.transform.localScale = v;


        //v.x = this.gameObject.transform.position.x;
        //v.y = this.gameObject.transform.position.y + 0.1F;
        //v.z = this.gameObject.transform.position.z;
        //this.gameObject.transform.position = v;

        GameObject topLabel = GameObject.FindWithTag("topLabel" + this.gameObject.tag);
        Vector3 vTopLabel = topLabel.transform.position;
        vTopLabel.y += 0.2F;
        topLabel.transform.position = vTopLabel;
        TextMesh topTextMesh = topLabel.GetComponent<TextMesh>();
        topTextMesh.text = (this.transform.localScale.y * 100).ToString();
    }

    void Update()
    {
        transform.localRotation *= Quaternion.Euler(0, 30 * Time.deltaTime, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Console.log(string.Format("已碰撞物体:{0}", collision.gameObject.name));
        //设置topLabel的显示文字
        GameObject topLabel = GameObject.FindWithTag("topLabel" + this.gameObject.tag);
        TextMesh topTextMesh = topLabel.GetComponent<TextMesh>();
        topTextMesh.text = (this.transform.localScale.y * stepLength).ToString();
    }
}
