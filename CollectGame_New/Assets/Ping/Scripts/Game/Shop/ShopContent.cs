using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopContent : MonoBehaviour
{

    public Text txtStar;
    public ItemChoose[] listItem;
    public ScrollRect scrollIndex;
    public RectTransform contentIndex;

    int _currentID;
    public int currentID {
        get { return _currentID; }
        set
        {
            _currentID = value;
            currentData = DataManager.Instance.GetDataCustomize(_currentID);
        }
    }
    DataCustomize currentData;

    public void Init()
    {
        dragDistance = Screen.height * 7 / 100;
        for (int i = 0; i < listItem.Length; i++)
        {
            listItem[i].Init(GamePreferences.Instance.customize.getInfo(i));
        }
        currentID = GamePreferences.Instance.customize.index;
        InitButton();
        ChangeIndex();
    }
    void OnEnable()
    {
        txtStar.text = GamePreferences.Instance.setting.coin.ToString();
    }
    public void SetDifficulty()
    {
        int _diff = GamePreferences.Instance.setting.currentDifficulty;
        for (int i = 0; i < listItem.Length; i++)
        {
            listItem[i].SetDifficulty(_diff);
        }
    }

    public GameObject buttonLeft;
    public GameObject buttonRight;
    public GameObject buttonUnlock;
    public GameObject buttonPlay;
    public Sprite[] spriteType;
    public Color colorStar;
    public Color colorNoStar;
    public Color colorDefault;
    public Color colorWarning;
    public Text txtBtn;
    public Image iconStarBtn;
    public Image maskBtn;
    public void InitButton()
    {
        if (currentID == 0)
        {
            buttonLeft.SetActive(false);
            buttonRight.SetActive(true);
        }
        else if (currentID == (listItem.Length - 1))
        {
            buttonLeft.SetActive(true);
            buttonRight.SetActive(false);
        }
        else
        {
            buttonLeft.SetActive(true);
            buttonRight.SetActive(true);
        }
        bool isUnLock = GamePreferences.Instance.customize.getInfo(currentID);
        if (isUnLock)
        {
            buttonPlay.SetActive(true);
            buttonUnlock.SetActive(false);
        }
        else
        {
            buttonPlay.SetActive(false);
            buttonUnlock.SetActive(true);
            txtBtn.text = currentData.value.ToString();
            iconStarBtn.sprite = spriteType[currentData.type];
            int _value = currentData.type == 0 ? GamePreferences.Instance.setting.coin : GamePreferences.Instance.setting.getHighScore(currentData.type - 1);
            txtBtn.color = currentData.value <= _value ? colorStar : colorNoStar;
        }
    }
    public int GetIDPlay()
    {
        GamePreferences.Instance.customize.index = currentID;
        GamePreferences.Instance.SaveCustomize();
        return currentID;
    }
    public void OnClickButtonUnlock()
    {
        int _value = currentData.type == 0 ? GamePreferences.Instance.setting.coin : GamePreferences.Instance.setting.getHighScore(currentData.type - 1);
        if (currentData.value <= _value)
        {
            if (currentData.type == 0)
            {
                GamePreferences.Instance.setting.updateCoin(0 - currentData.value);
                GamePreferences.Instance.SaveSetting();
                txtStar.text = GamePreferences.Instance.setting.coin.ToString();
            }
            GamePreferences.Instance.customize.Unlock(currentData.id);
            GamePreferences.Instance.SaveCustomize();
            listItem[currentID].Init(true);
            buttonPlay.SetActive(true);
            buttonUnlock.SetActive(false);
        }
        else
        {
            maskBtn.color = colorWarning;
            StartCoroutine(SetDefaultColor(0.3f));
        }
    }
    IEnumerator SetDefaultColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        maskBtn.color = colorDefault;
    }


    public void OnClickLeft()
    {
        if (currentID > 0)
        {
            currentID--;
            InitButton();
            StartCoroutine(ChangeIndexCoroutine());
        }
    }
    public void OnClickRight()
    {
        if (currentID < (listItem.Length - 1))
        {
            currentID++;
            InitButton();
            StartCoroutine(ChangeIndexCoroutine());
        }
    }
    IEnumerator ChangeIndexCoroutine()
    {
        RectTransform rect = listItem[currentID].gameObject.GetComponent<RectTransform>();
        Vector2 _currentIndex = contentIndex.anchoredPosition;
        Vector2 _targetIndex = (Vector2)scrollIndex.transform.InverseTransformPoint(contentIndex.position)
            - (Vector2)scrollIndex.transform.InverseTransformPoint(rect.position);

        float frame = 20;
        Vector2 _offsetIndex = (_targetIndex - _currentIndex) / frame;
        Canvas.ForceUpdateCanvases();
        while (true)
        {
            _currentIndex += _offsetIndex;
            contentIndex.anchoredPosition = _currentIndex;
            frame--;
            if (frame < 0)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        contentIndex.anchoredPosition = _targetIndex;
    }
    public void ChangeIndex()
    {
        RectTransform target = listItem[currentID].gameObject.GetComponent<RectTransform>();
        Canvas.ForceUpdateCanvases();
        contentIndex.anchoredPosition = (Vector2)scrollIndex.transform.InverseTransformPoint(contentIndex.position)
            - (Vector2)scrollIndex.transform.InverseTransformPoint(target.position);
    }
    #region Touch Phase
    Vector3 fpos;
    Vector3 lpos;
    float dragDistance;
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fpos = touch.position;
                lpos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lpos = touch.position;
                touchPhase();
            }
        }
    }
    void touchPhase()
    {
        if (Mathf.Abs(lpos.x - fpos.x) > dragDistance || Mathf.Abs(lpos.y - fpos.y) > dragDistance)
        {
            if (Mathf.Abs(lpos.x - fpos.x) > Mathf.Abs(lpos.y - fpos.y))
            {
                if ((lpos.x > fpos.x))
                {
                    // Left
                    OnClickLeft();
                }
                else
                {
                    // Right
                    OnClickRight();
                }
            }
            else
            {
                if (lpos.y > fpos.y)
                {
                    // Up
                }
                else
                {
                    // Down
                }
            }
        }
    }
    #endregion
}