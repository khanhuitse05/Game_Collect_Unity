using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GSShop : GSTemplate
{
    static GSShop _instance;
    public static GSShop Instance { get { return _instance; } }

    public Text txtStar;
    public Text txtCount;
    public GameObject pfbottom;
    public GameObject pfItem;
    public Transform root;
    List<ItemChoose> listItem;
    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
    protected override void init()
    {
        listItem = new List<ItemChoose>();
        for (int i = 0; i < DataManager.Instance.dataCustomize.Count; i++)
        {
            AddItem(i);
        }
        AddBottom();
        listItem[GamePreferences.Instance.customize.index].Select();
    }
    public override void onEnter()
    {
        base.onEnter();
        ReloadItem();
    }
    public void ReloadItem()
    {
        int _count = 0;
        for (int i = 0; i < listItem.Count; i++)
        {
            listItem[i].ReLoad();
            if (GamePreferences.Instance.customize.getInfo(i))
            {
                _count++;
            }
        }
        txtCount.text = "" + _count + " / " + listItem.Count;
        txtStar.text = GamePreferences.Instance.setting.coin.ToString();
    }
    public override void onResume()
    {
        base.onResume();
    }
    public override void onSuspend()
    {
        base.onSuspend();
    }
    public override void onExit()
    {
        base.onExit();
    }
    protected override void onBackKey()
    {
        OnBack();
    }
    void AddItem(int _id)
    {
        GameObject _obj = Utils.Spawn(pfItem, root);
        ItemChoose _item = _obj.GetComponent<ItemChoose>();
        _item.Init(_id);
        listItem.Add(_item);
    }
    void AddBottom()
    {
        Utils.Spawn(pfbottom, root);
    }
    public void OnBack()
    {
        GameStatesManager.Instance.stateMachine.PopState();
    }
}