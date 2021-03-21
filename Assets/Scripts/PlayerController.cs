using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    /*----------------------------
          Variables
    -----------------------------*/

    #region Player State Machine

    public enum States
    {
        Normal,
        FocusedInteractable,
        Interacting
    }

    public States CurrentState = States.Normal;

    public States currentState
    {
        get
        {
            return CurrentState;
        }
        private set
        {
            switch (value)
            {
                case States.Normal:
                    agent.isStopped = false;
                    break;
                case States.FocusedInteractable:
                    agent.isStopped = false;
                    break;
                case States.Interacting:
                    agent.isStopped = true;
                    break;
                default:
                    break;
            }
            GameEvents.instance.ChangePlayerState(value);
            CurrentState = value;
        }
    }

    #endregion

    NavMeshAgent agent;
    Interactable Focus;
    Vector3 target;

    public Animator animator;

    Interactable focus
    {
        get
        {
            return Focus;
        }
        set
        {
            if (value != null)
            {
                target = Helpers.GetClosestPoint(transform.position, value.interactPoints);

                agent.SetDestination(Helpers.GetClosestPoint(transform.position, value.interactPoints));
            }
            Focus = value;
        }
    }

    /*----------------------------
                Start
    -----------------------------*/
    private void Start()
    {
        //todo: set spawn from room transfer/save loading
        //transform.position = GameDataManager.instance.GetSavedPlayerPosition();

        //Event subscriptions
        GameEvents.instance.navClick += NavClick;
        GameEvents.instance.interactableClicked += InteractableClicked;

        //Components
        agent = GetComponent<NavMeshAgent>();
        //todo: animator
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
                break;
            case States.FocusedInteractable:
                agent.SetDestination(target);
                if (transform.position.x == target.x && transform.position.z == target.z)
                {
                    print("Interact");
                    currentState = States.Interacting;

                }
                break;
            case States.Interacting:
                break;
            default:
                break;
        }
    }

    private void LateUpdate()
    {
        if (focus != null)
        {
            target = Helpers.GetClosestPoint(transform.position, focus.interactPoints);
        }
        else
        {
            target = transform.position;
        }
    }

    /*----------------------------
            Event Responses
    -----------------------------*/
    private void NavClick(Vector3 point)
    {
        switch (currentState)
        {
            case States.Normal:
                currentState = States.Normal;
                agent.SetDestination(point);
                focus = null;
                break;
            case States.FocusedInteractable:
                currentState = States.Normal;
                agent.SetDestination(point);
                focus = null;
                break;
            case States.Interacting:
                currentState = States.Normal;
                agent.SetDestination(point);
                focus = null;
                break;
        }
    }

    private void InteractableClicked(Interactable obj)
    {
        switch (currentState)
        {
            case States.Normal:
                currentState = States.FocusedInteractable;
                focus = obj;
                break;
            case States.FocusedInteractable:
                currentState = States.FocusedInteractable;
                focus = obj;
                break;
        }
    }
}