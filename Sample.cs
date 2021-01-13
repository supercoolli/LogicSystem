using UnityEngine;
using LH;//引入命名空间

public enum LogicType_1
{
    None,
}

public enum LogicType_2
{
    None,
}

/// <summary>
/// 存有逻辑名称的类
/// </summary>
public class LogicName
{
    public const string Group = "Group";
    public const string Name_1 = "Name1";
    public const string Name_2 = "Name2";
    public const string Name_3 = "Name3";
    public const string Name_4 = "Name4";
}

public class Sample : MonoBehaviour
{
    public GameObject g;

    private void Awake()
    {
        if (g == null)
            g = gameObject;
    }

    void Start()
    {
        //订阅方法

        //根据字符串订阅方法
        LogicEvent.Subscribe(LogicName.Group, LogicName.Name_1, TestPara);

        //Sub后<>中的类型要与方法参数类型保持一致
        LogicEvent.Subscribe<int>(LogicName.Group, LogicName.Name_2, TestPara);

        //最后一个参数类型为GameObject,当传入的gameobject被Destory时，Sub的方法会自动DeSub
        LogicEvent.Subscribe<int, int>(LogicName.Group, LogicName.Name_3, TestPara, g);

        //最多可支持6个任意参数类型的方法，可根据需要扩充
        LogicEvent.Subscribe<int, int, string>(LogicName.Group, LogicName.Name_4, TestPara);

        //根据任意枚订阅定方法
        LogicEvent.Subscribe(LogicType_1.None, TestEnum);
        LogicEvent.Subscribe(LogicType_2.None, TestEnum);


        //通知方法
        //可以在任意地方进行通知，使用Pub方法后会调用之前Sub的方法

        //根据字符串通知方法
        LogicEvent.Publish(LogicName.Group, LogicName.Name_1);

        LogicEvent.Publish(LogicName.Group, LogicName.Name_2, 2);

        LogicEvent.Publish(LogicName.Group, LogicName.Name_3, 3, 4);

        LogicEvent.Publish(LogicName.Group, LogicName.Name_4, 5, 6, "测试");

        //根据枚举通知方法
        LogicEvent.Publish(LogicType_1.None);
        LogicEvent.Publish(LogicType_2.None);



        //取消订阅方法

        LogicEvent.DeSubscribe(LogicName.Group, LogicName.Name_1, TestPara);
        LogicEvent.DeSubscribe<int>(LogicName.Group, LogicName.Name_2, TestPara);
        LogicEvent.DeSubscribe<int, int>(LogicName.Group, LogicName.Name_3, TestPara);
        LogicEvent.DeSubscribe<int, int, string>(LogicName.Group, LogicName.Name_4, TestPara);

        LogicEvent.DeSubscribe(LogicType_1.None, TestEnum);
        LogicEvent.DeSubscribe(LogicType_2.None, TestEnum);

    }

    private void TestEnum()
    {
        Debug.Log("通过枚举类型调用方法");
    }

    private void TestPara()
    {
        Debug.Log("通过字符串调用没有参数的方法");
    }
    private void TestPara(int a)
    {
        Debug.Log("通过字符串调用1个参数的方法    " + a);
    }

    private void TestPara(int a, int b)
    {
        int x = a + b;
        Debug.Log("通过字符串调用2个参数的方法    " + a + " + " + b + " = " + x);
    }

    private void TestPara(int a, int b, string c)
    {
        int x = a + b;
        Debug.Log("通过字符串调用3个参数的方法    " + a + " + " + b + " = " + x + "    " + c);
    }

}
