using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    /*----------------------------
              Variables
    -----------------------------*/
    //Definitions
    public enum States
    {
        Normal,
        Interacting
    }

    //Fields
    private Vector3 target;
    private States currentState = States.Normal;
    private Interactable focus;

    //Properties
    public Vector3 Target
    {
        get {return target;}
        private set
        {
            agent.SetDestination(value);
            target = value;
        }
    }
    public States CurrentState
    {
        get {return currentState;}
        private set
        {
            switch (value)
            {
                case States.Normal:
                    agent.isStopped = false;
                    break;
                case States.Interacting:
                    agent.isStopped = true;
                    break;
                default:
                    break;
            }
            GameEvents.instance.ChangePlayerState(value);
            currentState = value;
        }
    }
    public Interactable Focus
    {
        get {return focus;}
        private set
        {
            if (value != null)
            {
                bool keepFocus = true;
                Vector3 tempTarget = Helpers.GetClosestPoint(transform.position, value, out keepFocus);
                if (keepFocus)
                {
                    focus = value;
                    Target = tempTarget;
                }
                else
                {
                    focus = null;
                    Target = transform.position;
                }
            }
            else
            {
                GameEvents.instance.InteractableDefocused();
                focus = value;
            }
        }
    }

    //Unity Components
    NavMeshAgent agent;
    public Animator animator;   //TODO: get component

    /*----------------------------
                Start
    -----------------------------*/
    private void Start()
    {
        //TODO: set spawn from room transfer/save loading   transform.position = GameDataManager.instance.GetSavedPlayerPosition();

        //Event subscriptions
        GameEvents.instance.navClick += NavClick;
        GameEvents.instance.interactableClicked += InteractableClicked;

        //Components
        agent = GetComponent<NavMeshAgent>();
    }

    /*----------------------------
                Update
    -----------------------------*/
    private void Update()
    {
        //Animate the player
        animator.SetFloat("Blend", agent.velocity.normalized.magnitude);

        //Keep following focus
        if (Focus != null)
        {
            if (transform.position.x == Target.x && transform.position.z == Target.z)
            {
                if (currentState == States.Normal)
                {
                    CurrentState = States.Interacting;
                    StartCoroutine(RotateTowards(Focus.transform.position, 0.3f));
                }
            }
            else
            {
                bool keepFocus = true;
                Vector3 tempTarget = Helpers.GetClosestPoint(transform.position, Focus, out keepFocus);
                if (keepFocus)
                {
                    Target = tempTarget;
                }
                else
                {
                    Focus = null;
                    Target = transform.position;
                }
            }
        }
    }

    /*-------------------------------------------------------------------
                            RotateTowards
            Rotates towards a Vector3 destination for X speed
    ---------------------------------------------------------------------*/
    float turnSmoothVelocity;
    public IEnumerator RotateTowards(Vector3 destination, float speed)
    {
        while (true)
        {
            if (CurrentState != States.Interacting)
            {
                break;
            }

            //Get directional vector towards destination
            Vector3 targetVector = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);

            //Calculate movement angle and rotate
            float targetAngle = Mathf.Atan2(targetVector.x, targetVector.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, speed);

            if (Mathf.Abs(targetAngle - angle) < 0.2f)
            {
                break;
            }

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            yield return null;
        }

    }

    /*----------------------------
              Game Events
    -----------------------------*/
    private void NavClick(Vector3 point)
    {
        CurrentState = States.Normal;
        Target = point;
        Focus = null;
    }

    private void InteractableClicked(Interactable obj)
    {
        switch (currentState)
        {
            case States.Normal:
                Focus = obj;
                break;
            case States.Interacting:
                CurrentState = States.Normal;
                Focus = obj;
                break;
        }
    }
}