using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroTutorial : MonoBehaviour
{

    const float heightJump = 1f;
    const float gravityScale = 2;
    public GameObject body;
    public int lane { get; set; }
    int laneMax;
    public float offsetLane = 0.25f;
    float laneY(int _index)
    {
        return GSGamePlay.Instance.lanes[_index].position.y + offsetLane; ;
    }

    float timeJump;
    float VelJump;

    public float speed;
    public Vector2 velocity;
    float gravity { get { return Physics2D.gravity.y * gravityScale; } }


    public void Init(int _lane)
    {
        lane = _lane;
        transform.position = new Vector3(0, laneY(lane), 0);
        timeJump = Mathf.Sqrt(2 * heightJump / -gravity);
        VelJump = -gravity * timeJump;
        isJump = false;
        laneMax = GSGamePlay.Instance.lanes.Length - 1;
        TurnLeft();
    }

    void Update()
    {
        UpdateMove();
    }
    [HideInInspector]
    public bool isJump;
    protected void UpdateMove()
    {
        // Right
        transform.Translate(velocity * Time.deltaTime);
        if (transform.position.x > Pos.botRight.x)
        {
            transform.position = new Vector2(Pos.botRight.x, transform.position.y);
            TurnLeft();
            GSTutorial.Instance.HeroOverFarm();
        }
        if (transform.position.x < Pos.topLeft.x)
        {
            transform.position = new Vector2(Pos.topLeft.x, transform.position.y);
            TurnRight();
            GSTutorial.Instance.HeroOverFarm();
        }
        //
        if (isUp && !isJump)
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
        if (isDown && !isJump)
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
    bool isDown;
    bool isUp;
    void JumpFinish()
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
        SetDirection();
    }
    public void TurnLeft()
    {
        velocity.x = -speed;
        SetDirection();
    }
    public void TurnRight()
    {
        velocity.x = speed;
        SetDirection();
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        Unit _unit = _col.gameObject.GetComponent<Unit>();
        if (_unit != null)
        {
            switch (_unit.getTypeUnit())
            {
                case UnitType.Lane:
                    OnTriggerLane(_col);
                    break;
                default:
                    break;
            }
        }
    }
    protected void OnTriggerLane(Collider2D _col)
    {
        Lane _lane = _col.gameObject.GetComponent<Lane>();
        if (velocity.y < 0)
        {
            if (lane == _lane.index)
            {
                JumpFinish();
            }
        }
    }
    void SetDirection()
    {
        if (velocity.x > 0)
        {
            body.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            body.transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
