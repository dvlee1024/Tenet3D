using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    Animator animator;

    public float speed = 0.5f;

    PlayerAction curAction = PlayerAction.IDLE;
    PlayerAction lastAction = PlayerAction.IDLE;

    Vector3 curTarget = Vector3.zero;

    private bool canController = false;

    void Start()
    {
		animator = GetComponent<Animator>();
	}

    bool hasTargetPlace = false;
    void Update()
    {

        if (canController)
        {
            SetupTargetPlace();
        }

        UpdatePosition();
        UpdateAnimator();
    }

    private void UpdatePosition()
    {
        lastAction = curAction;
        if (hasTargetPlace)
        {
            if (DistanceFromTarget() > 0.1f)
            {
                UpdateDirection();
                Vector3 v = Vector3.MoveTowards(transform.position, curTarget, speed * Time.deltaTime);
                transform.position = v;
                curAction = PlayerAction.RUN;
            }
            else
            {
                hasTargetPlace = false;
                transform.position = curTarget;
                curAction = PlayerAction.IDLE;
            }
        }
    }

    private void SetupTargetPlace()
    {
        Vector2 touchPoint = Input.mousePosition;
        bool bTouch = false;

#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    bTouch = true;
                    touchPoint = Input.GetTouch(0).position;
                }
            }else {
                
            }
#else
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                bTouch = true;
                touchPoint = Input.mousePosition;
            }
        }
#endif

        if (bTouch)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPoint);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100, LayerMask.GetMask("Ground")))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                Vector3 hitPoint = hitInfo.point;
                curTarget = hitPoint;
                hasTargetPlace = true;
            }
        }
    }

    private float DistanceFromTarget()
    {
        return Vector3.Distance(transform.position, curTarget);
    }

    private void UpdateDirection()
    {
        transform.LookAt(curTarget);
    }


    public void setCanController(bool can)
    {
        canController = can;
    }

    public void Reset()
    {
        transform.position = Vector3.zero;
        animator.SetTrigger("idle");
        hasTargetPlace = false;
        canController = false;
        curAction = PlayerAction.IDLE;
        lastAction = PlayerAction.IDLE;
    }

    public Frame getFrame()
    {
        Frame frame = new Frame();
        frame.postion = gameObject.transform.position;
        frame.rotation = gameObject.transform.rotation;
        frame.action = curAction;
        return frame;
    }

    public void setFrame(Frame frame)
    {
        transform.position = frame.postion;
        transform.rotation = frame.rotation;
        lastAction = curAction;
        curAction = frame.action;

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        if (lastAction != curAction)
        {
            switch (curAction)
            {
                case PlayerAction.IDLE:
                    animator.SetTrigger("idle");
                    print("idle");
                    break;
                case PlayerAction.RUN:
                    animator.SetTrigger("run");
                    print("run");
                    break;
            }
        }
    }

}
