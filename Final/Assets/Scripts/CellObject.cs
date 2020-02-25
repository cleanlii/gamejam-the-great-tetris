using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Runtime.InteropServices;
using System;

public class Messagebox
{
    [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr handle, String message, String title, int type);
}

public class Pair
{
    public Pair(Vector2Int pos,GameObject go)
    {
        position = pos;
        gameo = go;
    }
    public Vector2Int position;
    public GameObject gameo;
}
public class CellObject : MonoBehaviour
{
    public List<Vector2Int> cells;
    public List<Pair> IndexSubPair;

    public Vector2Int ObjectPosition;

    CellMap cellmap;

    public bool moveAble;
    public GameObject SubPlayerPrefab;

    public Text HealthNum;
    public Text AttackNum;
    public Text DefenceNum;
    public Text BottleNum;
    public Text MoneyNum;
    public Image WeaponImg;
    public Image ShieldImg;

    public GameObject player;

    void Start()
    {
        cellmap = GameObject.FindWithTag("cellmap").GetComponent<CellMap>();
        cellmap.cellObjects.Add(this);
        IndexSubPair = new List<Pair>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

    }

    void WinGame()
    {
        SceneManager.LoadScene("Win");
    }

    public void MappingCellObject()
    {
        ObjectPosition.x = (int)gameObject.transform.position.x;
        ObjectPosition.y = (int)gameObject.transform.position.y;
        foreach (Vector2Int int2 in cells)
        {
            cellmap.CellObjectLookupTable[int2.x + ObjectPosition.x, int2.y + ObjectPosition.y] = this;
        }
    }

    public void UpdateCellObject()
    {

        if (moveAble)
        {
            List<CellObject> CollisionObject = new List<CellObject>();
            Vector3 newPos = gameObject.transform.position;
            if (Input.GetKeyDown(KeyCode.A))
            {
                CellObject obj = DetectCollisionAtMove(Vector2Int.left);
                if (obj != null)
                {
                    CollisionObject.Add(obj);
                }
                else newPos += Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                CellObject obj = DetectCollisionAtMove(Vector2Int.right);
                if (obj != null)
                {
                    CollisionObject.Add(obj);
                }
                else newPos += Vector3.right;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                CellObject obj = DetectCollisionAtMove(Vector2Int.up);
                if (obj != null)
                {
                    CollisionObject.Add(obj);
                }
                else newPos += Vector3.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                CellObject obj = DetectCollisionAtMove(Vector2Int.down);
                if (obj != null)
                {
                    CollisionObject.Add(obj);
                }
                else newPos += Vector3.down;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                gameObject.GetComponent<ATK>().Attack = 999;
                AttackNum.text = gameObject.GetComponent<ATK>().Attack.ToString();
            }
            gameObject.transform.position = newPos;
            foreach (CellObject co in CollisionObject)
            {
                AtCollision(co);
            }

            if (gameObject.GetComponent<BTT>().Bottle > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AddSubCell(GetMouseWorldPosInt() - ObjectPosition);
                    gameObject.GetComponent<BTT>().Bottle -= 1;
                    BottleNum.text = gameObject.GetComponent<BTT>().Bottle.ToString();
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {

                Vector2Int int2 = GetMouseWorldPosInt();
                Debug.Log(int2);
                if (cellmap.CellObjectLookupTable[int2.x, int2.y] == this)
                {
                    DeleteSubCell(GetMouseWorldPosInt() - ObjectPosition);
                }
            }
        }
    }
    private void OnDestroy()
    {
        cellmap.cellObjects.Remove(this);
        if (gameObject.GetComponent<CellObject>().moveAble == false)
        {
            player.GetComponent<MOY>().Money += 100;
        }
    }

    public void AtCollision(CellObject cellObject)
    {

        if (cellObject.gameObject.tag == "Surprise")
        {
            Messagebox.MessageBox(IntPtr.Zero, "走着走着，你感觉到有什么肮脏的东西正盯着你", "随机事件", 0);
            Messagebox.MessageBox(IntPtr.Zero, "古神的诅咒：生命值-50", "随机事件", 0);
            gameObject.GetComponent<HP>().Health -= 50;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
            Destroy(cellObject.gameObject);
        }

        if (cellObject.gameObject.tag == "Surprise3")
        {
            Messagebox.MessageBox(IntPtr.Zero, "你看见一个衣衫褴褛的老头向你走来，手里拿着一本圣经", "随机事件", 0);
            Messagebox.MessageBox(IntPtr.Zero, "友善的牧师：生命值+60", "随机事件", 0);
            gameObject.GetComponent<HP>().Health += 60;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
            Destroy(cellObject.gameObject);
        }

        if (cellObject.gameObject.tag == "Surprise1")
        {
            Messagebox.MessageBox(IntPtr.Zero, "你路过了一个传说中的祭坛，要献血开门吗？", "随机事件", 1);
            Messagebox.MessageBox(IntPtr.Zero, "血神祭坛：生命值-20 攻击力+30", "随机事件", 0);
            gameObject.GetComponent<HP>().Health -= 20;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
            gameObject.GetComponent<ATK>().Attack += 30;
            AttackNum.text = gameObject.GetComponent<ATK>().Attack.ToString();
            Destroy(cellObject.gameObject);
        }

        if (cellObject.gameObject.tag == "Surprise2")
        {
            Messagebox.MessageBox(IntPtr.Zero, "一把宝剑插在不明生物的排泄物里，你要拔出它吗？", "随机事件", 1);
            Messagebox.MessageBox(IntPtr.Zero, "屎中剑：攻击力+20", "随机事件", 1);
            gameObject.GetComponent<ATK>().Attack += 20;
            AttackNum.text = gameObject.GetComponent<ATK>().Attack.ToString();
            Destroy(cellObject.gameObject);
        }

        if (cellObject.gameObject.tag == "Chest")
        {
            Messagebox.MessageBox(IntPtr.Zero, "恭喜你获得 渴望盾+2", "不知名的宝箱", 0);
            gameObject.GetComponent<DFC>().Defence += 25;
            DefenceNum.text = gameObject.GetComponent<DFC>().Defence.ToString();
            
            Destroy(cellObject.gameObject);
        }

        if (cellObject.gameObject.tag == "Shop")
        {
            Messagebox.MessageBox(IntPtr.Zero, "变形药水$100 （会涨价哦）", "欢迎来到商店！", 0);
            if (gameObject.GetComponent<MOY>().Money >= 100)
            {
                Messagebox.MessageBox(IntPtr.Zero, "欢迎下次光临！", "商店老板", 0);
                gameObject.GetComponent<MOY>().Money -= 100;
                MoneyNum.text = gameObject.GetComponent<MOY>().Money.ToString();
                gameObject.GetComponent<BTT>().Bottle += 1;
                BottleNum.text = gameObject.GetComponent<BTT>().Bottle.ToString();
            }
            else
                Messagebox.MessageBox(IntPtr.Zero, "没钱快滚！", "商店老板", 0);
        }

        if (cellObject.gameObject.tag == "Drink")
        {
            Messagebox.MessageBox(IntPtr.Zero, "恭喜你获得 生命值+50", "生命药水", 0);
            gameObject.GetComponent<HP>().Health += 50;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
            Destroy(cellObject.gameObject);
        }

        if (cellObject.gameObject.tag == "ShellEnemy")
        {
            int ak = gameObject.GetComponent<ATK>().Attack;
            int akk = cellObject.GetComponent<ATK>().Attack;
            gameObject.GetComponent<HP>().Health -= akk;
            cellObject.GetComponent<HP>().Health -= ak;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
        }

        if (cellObject.gameObject.tag == "SwordEnemy")
        {
            int ak = gameObject.GetComponent<ATK>().Attack;
            int akk = cellObject.GetComponent<ATK>().Attack;
            gameObject.GetComponent<HP>().Health -= akk;
            cellObject.GetComponent<HP>().Health -= ak;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
        }

        if (cellObject.gameObject.tag == "KnightEnemy")
        {
            int ak = gameObject.GetComponent<ATK>().Attack;
            int akk = cellObject.GetComponent<ATK>().Attack;
            gameObject.GetComponent<HP>().Health -= akk;
            cellObject.GetComponent<HP>().Health -= ak;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
        }

        if (cellObject.gameObject.tag == "GongEnemy")
        {
            int ak = gameObject.GetComponent<ATK>().Attack;
            int akk = cellObject.GetComponent<ATK>().Attack;
            gameObject.GetComponent<HP>().Health -= akk;
            cellObject.GetComponent<HP>().Health -= ak;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
        }

        if (cellObject.gameObject.tag == "Devil")
        {
            int ak = gameObject.GetComponent<ATK>().Attack;
            int akk = cellObject.GetComponent<ATK>().Attack;
            gameObject.GetComponent<HP>().Health -= akk;
            cellObject.GetComponent<HP>().Health -= ak;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
        }

        if (cellObject.gameObject.tag == "ArrowEnemy")
        {
            int ak = gameObject.GetComponent<ATK>().Attack;
            int akk = cellObject.GetComponent<ATK>().Attack;
            gameObject.GetComponent<HP>().Health -= akk;
            cellObject.GetComponent<HP>().Health -= ak;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
        }

        if (cellObject.gameObject.tag == "Octupus")
        {
            int ak = gameObject.GetComponent<ATK>().Attack;
            int akk = cellObject.GetComponent<ATK>().Attack;
            gameObject.GetComponent<HP>().Health -= akk;
            cellObject.GetComponent<HP>().Health -= ak;
            HealthNum.text = gameObject.GetComponent<HP>().Health.ToString();
        }

            if (cellObject.gameObject.tag == "Flag")
        {
            WinGame();
            Destroy(cellObject.gameObject);
        }


    }

    public void AddSubCell(Vector2Int pos)
    {
        GameObject subcell = Instantiate(SubPlayerPrefab, transform);
        IndexSubPair.Add(new Pair(pos, subcell));
        subcell.transform.localScale = Vector3.one;
        Vector3 newPos = subcell.transform.position;
        newPos.x += pos.x;
        newPos.y += pos.y;
        subcell.transform.position = newPos;
        cells.Add(pos);
    }

    public void DeleteSubCell(Vector2Int pos)
    {
        cells.Remove(pos);
        for(int i = 0; i < IndexSubPair.Count; i++)
        {
            if (IndexSubPair[i].position == pos)
            {
                Destroy(IndexSubPair[i].gameo);
                IndexSubPair.RemoveAt(i);
            }
        }
    }

    public Vector2 GetMouseWorldPos()
    {
        Vector3 vec3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vec2 = new Vector2(vec3.x, vec3.y);
        return vec2;
    }

    public Vector2Int GetMouseWorldPosInt()
    {
        Vector3 vec3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int vec2 = new Vector2Int((int)(vec3.x + 0.5f), (int)(vec3.y + 0.5f));
        return vec2;
    }

    public CellObject DetectCollisionAtMove(Vector2Int move)
    {
        CellObject flag = null;
        foreach (Vector2Int int2 in cells)
        {
            if (DetectCollisionAtPos(int2 + ObjectPosition+ move) != null)
            {
                flag = DetectCollisionAtPos(int2 + ObjectPosition+ move);
            }
        }
        return flag;
    }

    //false则无碰撞，true则有，不会碰到自己，不会碰到null
    public CellObject DetectCollisionAtPos(Vector2Int position)
    {
        if (cellmap.CellObjectLookupTable[position.x, position.y] != null && cellmap.CellObjectLookupTable[position.x, position.y] != this)
            return cellmap.CellObjectLookupTable[position.x, position.y];
        else return null;
    }
}
