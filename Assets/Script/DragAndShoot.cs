using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndShoot : MonoBehaviour
{
    [Header("Movement")]
    public float maxPower;
    [Tooltip("Set gravity to 0 if you want a top down ball game like billiardo.")]
    public float gravity = 1;
    [Tooltip("Slow the ball movement while aiming to make it easier to aim.")]
    [Range(0f, 0.1f)] public float slowMotion;

    [Tooltip("Allows you to aim and shot even when the ball is still moving.")]
    public bool shootWhileMoving = false;
    [Tooltip("Drag forward to aim instead of reverse aiming.")]
    public bool forwardDraging = true;
    [Tooltip("Show the draging line in the screen so you will not get confused where you aiming")]
    public bool showLineOnScreen = false;
    [Tooltip("Allow you to click whenever in the screen to start aiming, turn it off if you only want to start aiming while clicking in the ball")]
    public bool freeAim = true;

    public int linePoints = 50;
    public float timeIntervalInPoints = 0.01f;

    GameObject launcher;
    Transform direction;
    Rigidbody2D rb;
    LineRenderer line;
    LineRenderer screenLine;

    // Vectors // 
    Vector2 startPosition;
    Vector2 targetPosition;
    Vector2 startMousePos;
    Vector2 currentMousePos;

    float shootPower;
    bool canShoot = true;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        line = GetComponent<LineRenderer>();
        direction = transform.GetChild(0);
        screenLine = direction.GetComponent<LineRenderer>();
        launcher = GameObject.FindGameObjectWithTag("Launcher");
    }

    void Update()
    {

            if (Input.GetMouseButtonDown(0))
            {
                if (freeAim)
                    MouseClick();
                else
                    BallClick();
            }
            if (Input.GetMouseButton(0) && isAiming)
        {
                MouseDrag();

                if (shootWhileMoving) rb.velocity /= (1 + slowMotion);

            }

            if (Input.GetMouseButtonUp(0) && isAiming)
            {
                MouseRelease();
            }


            if (shootWhileMoving)
                return;
    }

    private bool objectClicked()
    {

        RaycastHit2D[] hit = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), 3.4f, Vector2.zero);

        for(int i = 0; i < hit.Length; i++)
            if (hit[i].collider != null && hit[i].collider.gameObject == gameObject)
                return true;
        
        return false;
    }


    void MouseClick()
    {

        isAiming = true;

        if (shootWhileMoving)
        {
            Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.right = dir * 1;

            startMousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else
        {
            if (canShoot)
            {
                Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                transform.right = dir * 1;

                startMousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
        }

    }

    private bool isAiming = false;

    void BallClick()
    {
        if (!objectClicked())
            return;

        isAiming = true;

        if (shootWhileMoving)
        {
            Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.right = dir * 1;

            startMousePos = transform.position;
        }
        else
        {
            if (canShoot)
            {
                Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                transform.right = dir * 1;

                startMousePos = transform.position;
            }
        }

    }
    void MouseDrag()
    {
        if (!freeAim)
            startMousePos = transform.position;

        if (shootWhileMoving)
        {
            LookAtShootDirection();
            DrawLine();

            if (showLineOnScreen)
                DrawScreenLine();

            float distance = Vector2.Distance(currentMousePos, startMousePos);

            if (distance > 1)
            {
                line.enabled = true;

                if (showLineOnScreen)
                    screenLine.enabled = true;
            }
        }
        else
        {
            if (canShoot)
            {
                LookAtShootDirection();
                DrawLine();

                if (showLineOnScreen)
                    DrawScreenLine();

                float distance = Vector2.Distance(currentMousePos, startMousePos);

                if (distance > 1)
                {
                    line.enabled = true;

                    if (showLineOnScreen)
                        screenLine.enabled = true;
                }
            }
        }

    }
    void MouseRelease()
    {
        if (shootWhileMoving)
        {
            Shoot();
            screenLine.enabled = false;
            line.enabled = false;
        }
        else
        {
            if (canShoot)
            {
                Shoot();
                screenLine.enabled = false;
                line.enabled = false;
                rb.gravityScale = gravity;
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("LaunchBall");
            }
        }

        isAiming = false;

    }

    void LookAtShootDirection()
    {
        Vector3 dir = startMousePos - currentMousePos;

        if (forwardDraging)
        {
            transform.right = dir * -1;
        }
        else
        {
            transform.right = dir;
            launcher.transform.right = dir;
            launcher.transform.RotateAround(launcher.transform.position, Vector3.forward, -5f);
        }

        float dis = Vector2.Distance(startMousePos, currentMousePos);
        dis *= 2f;


        if (dis < maxPower)
        {
            direction.localPosition = new Vector2(dis / 3, 0);
            shootPower = dis;
        }
        else
        {
            shootPower = maxPower;
            direction.localPosition = new Vector2(maxPower / 3, 0);
        }

    }
    public void Shoot()
    {
        canShoot = false;
        rb.velocity = transform.right * shootPower;
    }


    void DrawScreenLine()
    {
        screenLine.positionCount = 1;
        screenLine.SetPosition(0, startMousePos);


        screenLine.positionCount = 2;
        screenLine.SetPosition(1, currentMousePos);
    }

    void DrawLine()
    {
        startPosition = transform.position;
        Vector2 launchVelocity = transform.right * shootPower;
        line.positionCount = linePoints;

        float time = 0;

        for(int i = 0; i < linePoints; i++)
        {
            var x = (launchVelocity.x * time) + ((Physics.gravity.x * gravity) / 2 * time * time);
            var y = (launchVelocity.y * time) + ((Physics.gravity.y * gravity) / 2 * time * time);
            Vector2 point = new Vector2(x, y);
            line.SetPosition(i, point + startPosition);
            time += timeIntervalInPoints;
        }

        targetPosition = direction.transform.position;
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    }

    Vector3[] positions;


}
