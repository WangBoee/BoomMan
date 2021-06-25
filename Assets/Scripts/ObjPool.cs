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
    //让物体枚举类型和预制体进行对应
    public List<TypePrefab> typePrefabs = new List<TypePrefab>();
    public static ObjPool instace; //对象池的单实例

    void Awake()
    {
        instace = this;
    }
    //从对象池中获取对象
    public GameObject GetObj(ObjectType type)
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
                //若对象池中不存在物体则实例化对象
                GameObject prefab = GetPrefabType(type);
                if (prefab != null)
                {
                    temp = Instantiate(prefab, transform);
                }
            }
        }
        temp.SetActive(true);
        return temp;
    }

    //将对象添加回对象池
    public void AddObj(ObjectType type, GameObject obj)
    {
        //判断对象池中存在此类型对象及实例
        if (dic.ContainsKey(type) && dic[type].Contains(obj))
        {
            dic[type].Add(obj);
        }
        obj.SetActive(false);
    }

    //通过物体类型获取对应预制体
    public GameObject GetPrefabType(ObjectType type)
    {
        foreach (var item in typePrefabs)
        {
            //在预制体类型中找到了该类型的预制体
            if (item.type == type)
            {
                return item.prefab;
            }
        }
        return null;
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

//序列化
//使物体枚举类型和预制体相对应
[System.Serializable]
public class TypePrefab
{
    public GameObject prefab;
    public ObjectType type;

}