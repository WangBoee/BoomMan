using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//枚举对象类型
public enum ObjectType
{
    SuperWall,
    Wall,
    Prop,
    Bomb,
    Enemy,
    BombEffect
}
public class ObjPool : MonoBehaviour
{
    //物体类型和对应的对象池关系字典
    private Dictionary<ObjectType, List<GameObject>> dic = new Dictionary<ObjectType, List<GameObject>>();
    
    //从对象池中获取对象
    public void GetObj(ObjectType type)
    {
        GameObject temp = null;
        //判断字典中有无该类型对象
        if (!dic.ContainsKey(type))
        {
            dic.Add(type, new List<GameObject>());
        }
        else
        {
            //判断该类型对象池中有无物体
            if (dic[type].Count > 0)
            {
                //获取最后一个对象
                int index = dic[type].Count - 1;
                temp = dic[type][index];
                dic[type].RemoveAt(index); //从对象池中移除该对象
            }
            else
            {
                //TODO
            }
        }
    }

    //将对象添加回对象池
    public void AddObj(ObjectType type)
    {
        //TODO
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}