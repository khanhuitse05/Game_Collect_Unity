using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hero : MonoBehaviour {

    const float heightJump = 1f;
    const float gravityScale = 2;
    public GameObject body;
    public SpriteRenderer[] undyingSprite;
    public GameObject shieldSprite;
    public int lane;
    protected int laneMax;
    public float offsetLane = 0.25f;
    protected float laneY(int _index)
    {
        return GSGamePlay.Instance.lanes[_index].position.y + offsetLane; ;
    }

    protected float timeJump;
    protected float VelJump;

    public float speed;
    protected Vector2 velocity;
    protected  float gravity { get { return Physics2D.gravity.y * gravityScale; } }

    Control control;   

    public void Init(Control _control, int _lane = 0)
    {
        control = _control;
        lane = _lane;
        transform.position = new Vector3(0, laneY(lane), 0);
        timeJump = Mathf.Sqrt(2 * heightJump / -gravity);
        VelJump = -gravity * timeJump;
        isJump = false;
        laneMax = GSGamePlay.Instance.lanes.Length - 1;
        //velocity = new Vector2(speed, 0);
        TurnShield(0);
        TurnUndying(GSGamePlay.Instance.timeShield);
    }
	
	void Update () {
        if (GSGamePlay.Instance.isPlay)
        {
            UpdateMove();
            UpdateShield();
        }
        if (GSGamePlay.Instance.isOver)
        {
            UpdateDie();
        }
    }
    [HideInInspector]
    public bool isJump;
    protected virtual void MoveEffect(){ }
    protected  void UpdateMove()
    {
        // Right
        MoveEffect();
        transform.Translate(velocity * Time.deltaTime);
        if (transform.position.x > Pos.botRight.x)
        {
            transform.position = new Vector2(Pos.botRight.x, transform.position.y);
            TurnLeft();
        }
        if (transform.position.x < Pos.topLeft.x)
        {
            transform.position = new Vector2(Pos.topLeft.x, transform.position.y);
            TurnRight();
        }
        //
        if (isUp && !control.isJump)
        {
            GameConstants.PlaySoundJump();
            isUp = false;
            isJump = true;
            velocity.y = VelJump;
            if (lane != laneMax)
            {
                lane++;
            }
        }
        // Down
        if (isDown && !control.isJump)
        {
            isDown = false;
            if (lane != 0)
            {
                isJump = true;
                lane--;
            }
        }
        if (isJump)
        {
            velocity.y = velocity.y + gravity * Time.deltaTime;
        }
    }
    protected bool isDown;
    protected bool isUp;
    protected virtual void JumpFinish()
    {
        isJump = false;
        velocity.y = 0;
        transform.position = new Vector3(transform.position.x, laneY(lane), transform.position.z);
    }
    public virtual void JumeUp()
    {
        isDown = false;
        isUp = true;
    }
    public virtual void JumeDown()
    {
        isUp = false;
        isDown = true;
    }
    public virtual void Turning()
    {
        velocity.x *= -1;
    }
    public virtual void TurnLeft()
    {
        velocity.x = -speed;
    }
    public virtual void TurnRight()
    {
        velocity.x = speed;
    }
    
    void OnTriggerEnter2D(Collider2D _col)
    {
        Unit _unit = _col.gameObject.GetComponent<Unit>();
        if (_unit != null)
        {
            switch (_unit.getTypeUnit())
            {
                case UnitType.None:
                    break;
                case UnitType.Creep:
                    OnTriggerCreep(_col);
                    break;
                case UnitType.Food:
                    OnTriggerFood(_col);
                    break;
                case UnitType.Lane:
                    OnTriggerLane(_col);
                    break;
                case UnitType.Item:
                    OnTriggerItem(_col);
                    break;
                default:
                    break;
            }
        }
    }
    protected  void OnTriggerLane(Collider2D _col)
    {
        if (GSGamePlay.Instance.isPlay)
        {
            Lane _lane = _col.gameObject.GetComponent<Lane>();
            if (_lane.index >= 0 && !GSGamePlay.Instance.isOver)
            {
                if (velocity.y < 0)
                {
                    if (lane == _lane.index)
                    {
                        JumpFinish();
                    }
                    if ((lane - 1) == _lane.index)
                    {
                        lane -= 1;
                        JumpFinish();
                    }
                }
            }
            else
            {
                GSGamePlay.Instance.OnOverGame(0);
            }
        }
    }
    void OnTriggerCreep(Collider2D _col)
    {
        if (timeUndying > 0)
        {
            return;
        }
        if (timeShield > 0)
        {
            TurnShield(0);
            TurnUndying(1);
            return;
        }
        velocity = new Vector2(0, 0);
        if (GSGamePlay.Instance.isPlay)
        {
            ObjectValue _creep = _col.gameObject.GetComponent<ObjectValue>();
            GameConstants.PlaySoundDead();
            Utils.Spawn(GSGamePlay.Instance.explodeDie).transform.position = transform.position;
            GSGamePlay.Instance.OnOverGame(1, _creep.Getvalue());
        }
    }
    void OnTriggerFood(Collider2D _col)
    {
        GameConstants.PlaySoundCollect();
        Food _unit = _col.gameObject.GetComponent<Food>();
        GSGamePlay.Instance.CollectFood(_unit);
    }
    protected void OnTriggerItem(Collider2D _col)
    {
        GameConstants.PlaySoundCollect();
        Item _item = _col.gameObject.GetComponent<Item>();
        switch (_item.type)
        {
            case ItemType.None:
                break;
            case ItemType.Shield:
                TurnShield(_item.value);
                break;
            default:
                break;
        }
        ObjectPoolManager.Unspawn(_item.gameObject);

    }
    void UpdateDie()
    {
        body.transform.Rotate(Vector3.forward * Time.fixedDeltaTime * 100);
        velocity.y = velocity.y + gravity * Time.fixedDeltaTime;
        transform.Translate(velocity * Time.fixedDeltaTime);
    }
    float timeUndying;
    float offsetUndying = 0.2f;
    Color colorUndying = new Color(1, 1, 1, 0.5f);
    IEnumerator UndyingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(offsetUndying / 2);
            for (int i = 0; i < undyingSprite.Length; i++)
            {
                undyingSprite[i].color = colorUndying;
            }
            yield return new WaitForSeconds(offsetUndying / 2);
            for (int i = 0; i < undyingSprite.Length; i++)
            {
                undyingSprite[i].color = Color.white;
            }
            timeUndying -= offsetUndying;
            if (timeUndying < 0)
            {
                TurnUndying(0);
                break;
            }
        }
    }
    public void TurnUndying(float _time)
    {
        if (_time > 0)
        {
            timeUndying = _time;
            StartCoroutine(UndyingRoutine());
        }
        else
        {
            timeUndying = 0;
            for (int i = 0; i < undyingSprite.Length; i++)
            {
                undyingSprite[i].color = Color.white;
            }
        }
    }
    float timeShield;
    void UpdateShield()
    {
        if (timeShield > 0)
        {
            timeShield -= Time.deltaTime;
            if (timeShield <= 0)
            {
                TurnShield(0);
                TurnUndying(1);
            }
        }
    }
    public void TurnShield(float _time)
    {
        timeShield = _time;
        shieldSprite.SetActive(_time > 0);
    }
}
