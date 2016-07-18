using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreatePrefabs : MonoBehaviour {

    public GameObject CubePrefab;
    public GameObject LabelPrefab;
    public GameObject TopLabelPrefab;
    //class static object to share
    public static Vector3 v3;
    public static Vector3 v3Lable;
    public static int counter;
    private List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
    private float stepLength = 100f;

    public Color[] ColorList = {Color.blue,Color.red,Color.yellow,Color.green,Color.white};

    void Start () {
        Console.log("控制台信息将在此处显示");
        counter = 0;
        var url = "http://dev.epo.oa.com/hololensdata/api/DataProvider/GetDataExample";
        //var api = new WebApiInvoke(Callback);        
        //var dic = new Dictionary<string, object>();
        //api.PostAsyn(url, dic);
        StartCoroutine(POST(url));
    }

    //public void Callback(int code, string data)
    //{
    //    Console.log("请求返回数据,data:" + data);
    //    if (code == 200)
    //    {
    //        dataList = new JsonConvert().GetItemFromJson<List<Dictionary<string, object>>>(data);
    //        //set cube original position
    //        v3 = new Vector3(-5f, 5f, 6f);
    //        //set label original position
    //        v3Lable = new Vector3(-5f, -0.45f, 6f);
    //        //延时循环调用
    //        InvokeRepeating("createCube", 1, 0.75f);
    //    }
    //}
    IEnumerator POST(string url)
    {
        Console.log("开始请求数据,url: " + url);
        WWWForm form = new WWWForm();
        WWW www = new WWW(url, form);
        yield return www;

        if (www.error != null)
        {
            //POST请求失败
            Debug.Log("error is :" + www.error);
            Console.log("error is :" + www.error);
            SceneManager.LoadScene("Origami");

        }
        else
        {
            //POST请求成功
            Debug.Log("request ok : " + www.text);
            Console.log("request ok : " + www.text);
            dataList = new JsonConvert().GetItemFromJson<List<Dictionary<string, object>>>(www.text);
            //set cube original position
            v3 = new Vector3(-5f, 5f, 6f);
            //set label original position
            v3Lable = new Vector3(-5f, -0.45f, 6f);
            //延时循环调用
            InvokeRepeating("createCube", 1, 0.75f);
        }
    }
    private void createCube()
    {
        counter++;
        if (v3 != null)
        {
            Dictionary<string, object> data = dataList[counter-1];
            //移动cube的中心位置
            v3.x += 2f;
            //移动下方label的中心位置
            v3Lable.x += 2f;
            //初始化cube
            GameObject newCube = Instantiate(CubePrefab, v3, CubePrefab.transform.rotation) as GameObject;
            //设置tag方便GO的查找
            newCube.tag = counter.ToString();
            //将newCube的父级对象设为Plane面板
            //newCube.transform.parent = GameObject.Find("Plane").transform;
            //取随机数设置高度
            Vector3 v3Scale = newCube.transform.localScale;
            //从接口返回数据中给柱的高度赋值
            v3Scale.y = (float)System.Math.Round(int.Parse(data["Amount"].ToString()) / stepLength, 2); 
            newCube.transform.localScale = v3Scale;
            //随机设置颜色
            Renderer render = newCube.GetComponent<Renderer> ();
            float rdmValue = (float)System.Math.Round(Random.value, 2);
            render.material.color = ColorList[(int)(rdmValue * 100) % 5];

            //重新计算topLabel位置
            Vector3 topv3 = new Vector3(v3.x, newCube.transform.localScale.y + 1f,v3.z);
            //初始化topLabel
            GameObject topLabel = Instantiate(TopLabelPrefab, topv3, Quaternion.identity) as GameObject;
            topLabel.tag = "topLabel" + counter.ToString();
            //将topLabel的父级对象设为newCube面板
            //topLabel.transform.parent = newCube.transform;

            //初始化Label    
            GameObject newLabel = Instantiate(LabelPrefab, v3Lable, Quaternion.identity) as GameObject;
            newLabel.tag = "Label" + counter.ToString();
            //将Label的父级对象设为newCube面板
            //newLabel.transform.parent = newCube.transform;
            //设置Label的显示文字 
            TextMesh textMesh = newLabel.GetComponent<TextMesh>();
            textMesh.text = data["Name"].ToString();
        }
        else
        {
            Debug.Log("no tag prefab CubeDemo exists!");
        }
        if(counter >= dataList.Count)
        {
            CancelInvoke("createCube");
            Debug.Log("CancelInvoke createCube");
        }
    }
    // Update is called once per frame
    //void Update()
    //{

    //}
}
