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

        switch (CurrentState)
        {
            case States.Normal:

                //Keep following focus
                if (Focus != null)
                {
                    if (transform.position.x == Target.x && transform.position.z == Target.z)
                    {
                        CurrentState = States.Interacting;
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
                break;
            case States.Interacting:
                break;
            default:
                break;
        }
    }

    /*----------------------------
            Event Responses
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
        }
    }
}