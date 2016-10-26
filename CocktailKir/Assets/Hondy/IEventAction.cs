using UnityEngine;
using System.Collections;

public class IEventAction
{

    Vector3 scale;

    public delegate void call();

}



public class Test : MonoBehaviour
{
    public delegate void TestCallback(string s, float f);
    public float totalTime = 0.0f;

    void Start()
    {
    }
    void Update()
    {
        totalTime += Time.deltaTime;
    }

    public void Hoge(TestCallback Callback, string s)
    {
        Callback(s, totalTime);
    }
}
 
// Main.cs

public class Main : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Test test = new Test();
        // 定義済みのメソッドを呼ぶ場合
        test.Hoge(Hoge, "Hoge Method");
        // 匿名関数も使える
        test.Hoge((a, b) => { Debug.Log(a + ":" + b.ToString()); }, "Lambda");
    }

    void Hoge(string s, float f)
    {
        Debug.Log(s + ':' + f.ToString());
    }
}