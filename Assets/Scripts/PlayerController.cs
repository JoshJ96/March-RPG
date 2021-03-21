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
        FocusedInteractable
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
                    break;
                case States.FocusedInteractable:
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
                agent.SetDestination(value.transform.position);
            }
            Focus = value;
        }
    }

    /*----------------------------
                Start
    -----------------------------*/
    private void Start()
    {
        path = new NavMeshPath();
        //todo: set spawn from room transfer/save loading
        //transform.position = GameDataManager.instance.GetSavedPlayerPosition();

        //Event subscriptions
        GameEvents.instance.navClick += NavClick;
        GameEvents.instance.interactableClicked += InteractableClicked;

        //Components
        agent = GetComponent<NavMeshAgent>();
        //todo: animator
    }

    //delete this
    NavMeshPath path;


    /*----------------------------
                Update
    -----------------------------*/
    private void Update()
    {
        //Delete this       (which point is closer?)
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 test1 = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            Vector3 test2 = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);

            print(Helpers.GetNavPathDistance(transform.position, test1));
            print(Helpers.GetNavPathDistance(transform.position, test2));
        }

        //Animate the player
        animator.SetFloat("Blend", agent.velocity.normalized.magnitude);

        switch (CurrentState)
        {
            case States.Normal:
                break;
            case States.FocusedInteractable:
                //Keep following target during update
                agent.SetDestination(focus.transform.position);
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