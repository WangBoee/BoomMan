using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//枚举道具类型
//使道具类型与图片一一对应
public enum PropType
{
    HP,
    Speed,
    Bomb,
    Range,
    Time
}
public class Prop : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite wallSprite;
    public PropTypeSprite[] propTypeSprite; //存储不同道具图片
    private PropType propType;
    private bool isBombed = false; //道具只能被炸一次

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite = wallSprite;
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameController.Instance.GetEnemyCounts() == 0)
        //{
        //    DestroyProp();
        //}
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //被炸弹特效碰到显示道具图片
        if (col.CompareTag(Tag.BombEffect) && !isBombed)
        {
            this.gameObject.tag = Tag.Untagged;
            this.gameObject.layer = 0;
            //开启道具触发器
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            int index = Random.Range(0, propTypeSprite.Length);
            spriteRenderer.sprite = propTypeSprite[index].sprite;
            propType = propTypeSprite[index].propType;
            isBombed = true;
            StartCoroutine(PropAnim());
        }
        //玩家人物触碰到道具
        if (col.CompareTag(Tag.Player))
        {
            PlayerController playerController = col.GetComponent<PlayerController>();
            //Debug.LogError("玩家触发");
            switch (propType)
            {
                case PropType.HP:
                    playerController.HP++;
                    break;
                case PropType.Speed:
                    playerController.Speed += 0.05f;
                    break;
                case PropType.Bomb:
                    playerController.bombCount++;
                    break;
                case PropType.Range:
                    playerController.boomRange++;
                    break;
                case PropType.Time:
                    GameController.Instance.time += 15;
                    break;
                default:
                    break;
            }
            //重置
            ResetProp();
            //回收到对象池
            ObjPool.Instance.AddObj(ObjectType.Prop, gameObject);
        }
    }
    //重置道具数据
    public void ResetProp()
    {
        isBombed = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        this.gameObject.tag = Tag.Wall;
        this.gameObject.layer = 8;
        GetComponent<SpriteRenderer>().sprite = wallSprite;
    }
    //动画
    IEnumerator PropAnim()
    {
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
    }
}

//序列化
//不同道具图片(精灵)对应不同的Type值
[System.Serializable]
public class PropTypeSprite
{
    public PropType propType;
    public Sprite sprite;
}