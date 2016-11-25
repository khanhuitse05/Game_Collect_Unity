using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemChoose : MonoBehaviour {

    public static ItemChoose current;
    public Sprite[] spriteType;
    public Color colorStar;
    public Color colorNoStar;
    public Color colorNormal;
    public Color colorChoose;
    public Color colorDefault;
    public Color colorWarning;
    public Text txt;
    public Image[] circle;
    public Image framer;
    public Image iconStar;
    public Image mask;
    public DataCustomize data;
    bool isUnLock;
    bool isSelect;
    public void Init(int _id) {
        data = DataManager.Instance.GetDataCustomize(_id);
        isUnLock = GamePreferences.Instance.customize.getInfo(_id);
        UnSelect();
        txt.text = data.value.ToString();
        circle[0].sprite = Utils.loadResourcesSprite(GamePath.imageHero + _id + "0");
        circle[1].sprite = Utils.loadResourcesSprite(GamePath.imageHero + _id + "1");
        iconStar.sprite = spriteType[data.type];
    }
    public void ReLoad()
    {
        isUnLock = GamePreferences.Instance.customize.getInfo(data.id);
        OnUnLock(isUnLock);
        if (!isUnLock)
        {
            changeColorStar();
        }
    }
    public void UnSelect()
    {
        isSelect = false;
        framer.color = colorNormal;
    }
    public void Select()
    {
        if (current != null)
        {
            current.UnSelect();
        }
        current = this;
        isSelect = true;
        framer.color = colorChoose;
        GamePreferences.Instance.customize.index = data.id;
        GamePreferences.Instance.SaveCustomize();
    }
    void OnUnLock(bool _b)
    {
        mask.gameObject.SetActive(!_b);
    }
    public void changeColorStar()
    {
        int _value = data.type == 0 ? GamePreferences.Instance.setting.coin : GamePreferences.Instance.setting.getHighScore(data.type - 1);
        txt.color = data.value <= _value ? colorStar : colorNoStar;
    }
    public void OnClickData()
    {
        if (isSelect)
        {
            GameStatesManager.Instance.stateMachine.PushState(GSHome.Instance);
        }
        else if (isUnLock)
        {
            Select();
        }
        else
        {
            int _value = data.type == 0 ? GamePreferences.Instance.setting.coin : GamePreferences.Instance.setting.getHighScore(data.type - 1);
            if (data.value <= _value)
            {
                if (data.type == 0)
                {
                    GamePreferences.Instance.setting.updateCoin(0 - data.value);
                    GamePreferences.Instance.SaveSetting();
                    GSShop.Instance.ReloadItem();
                }
                GamePreferences.Instance.customize.Unlock(data.id);
                GamePreferences.Instance.SaveCustomize();
                OnUnLock(true);
                Select();
            }
            else
            {
                mask.color = colorWarning;
                StartCoroutine(SetDefaultColor(0.3f));
            }
        }
        
    }
    IEnumerator SetDefaultColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        mask.color = colorDefault;
    }
}
