using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hero : MonoBehaviour {

    public float heightJump;
    public int lane;
    protected int laneMax;
    public float offsetLane = 0.25f;
    protected float laneY(int _index)
    {
        return GSGamePlay.Instance.lanes[_index].position.y + offsetLane; ;
    }

    protected float timeJump;
    protected float VelJump;

    public float gravityScale = 1;
    public float speed;
    protected Vector2 velocity;
    protected  float gravity { get { return Physics2D.gravity.y * gravityScale; } }

    Control control;   

    public void Init(Control _control)
    {
        control = _control;
        transform.position = new Vector3(0, laneY(0), 0);
        timeJump = Mathf.Sqrt(2 * heightJump / -gravity);
        VelJump = -gravity * timeJump;
        lane = 0;
        isJump = false;
        laneMax = GSGamePlay.Instance.lanes.Length - 1;
        velocity = new Vector2(speed, 0);        
    }
	
	void Update () {
        if (GSGamePlay.Instance.isPlay)
        {
            UpdateMove();
        }
    }
    public bool isJump;
    protected  void UpdateMove()
    {
        // Right
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
    protected  void JumpFinish()
    {
        isJump = false;
        velocity.y = 0;
        transform.position = new Vector3(transform.position.x, laneY(lane), transform.position.z);
    }
    public void JumeUp()
    {
        isDown = false;
        isUp = true;
    }
    public void JumeDown()
    {
        isUp = false;
        isDown = true;
    }
    public void Turning()
    {
        velocity.x *= -1;
    }
    public void TurnLeft()
    {
        velocity.x = -speed;
    }
    public void TurnRight()
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
                default:
                    break;
            }
        }
    }
    protected  void OnTriggerLane(Collider2D _col)
    {
        Lane _lane = _col.gameObject.GetComponent<Lane>();
        if (lane == _lane.index && velocity.y < 0)
        {
            JumpFinish();
        }
    }
    void OnTriggerCreep(Collider2D _col)
    {
        GSGamePlay.Instance.OnOverGame();
    }
    void OnTriggerFood(Collider2D _col)
    {
        Food _unit = _col.gameObject.GetComponent<Food>();
        GSGamePlay.Instance.CollectFood(_unit);
    }
}
