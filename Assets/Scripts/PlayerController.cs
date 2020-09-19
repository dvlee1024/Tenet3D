using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    Animator animator;

    public float speed = 0.5f;

    PlayerAction curAction;
    PlayerAction lastAction;

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
                    animator.SetTrigger("run");
                }
            }
        }

        if (hasTargetPlace)
        {
            if(DistanceFromTarget() > 0.1f)
            {
                UpdateDirection();
                Vector3 v = Vector3.MoveTowards(transform.position, curTarget, speed * Time.deltaTime);
                transform.position = v;
                curAction = PlayerAction.RUN;
            } else
            {
                hasTargetPlace = false;
                curAction = PlayerAction.IDLE;
                transform.position = curTarget;
                animator.ResetTrigger("run"); // 不加会有bug
                animator.SetTrigger("idle");
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

        if(lastAction != curAction)
        {
            switch (curAction)
            {
                case PlayerAction.IDLE:
                    animator.SetTrigger("idle");
                    break;
                case PlayerAction.RUN:
                    animator.SetTrigger("run");
                    break;
            }
        }
    }

}
