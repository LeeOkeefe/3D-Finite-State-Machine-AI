using UnityEngine;

public class GuardRaycasting : MonoBehaviour
{
    private GuardBehaviour Behaviour;
    public GameObject Player;

    private float SeenTimer;

    [SerializeField] private SightState CurrentState;
    [SerializeField] private SightState LastState;

    public GuardState CurrentGuardState;

    private void Awake()
    {
        Behaviour = GetComponent<GuardBehaviour>();
    }

    private void Update()
    {
        var targetDir = Player.transform.position - transform.position;
        var angle = Vector3.Angle(targetDir, transform.forward);

        if (angle > 65f)
        {
            TransitionState(SightState.NotSeen);
            return;
        }

        Physics.Linecast(transform.position, Player.transform.position, out var hitInfo);

        if (hitInfo.transform != Player.transform)
        {
            TransitionState(SightState.NotSeen);
            return;
        }

        SeenTimer += Time.deltaTime;
        if (CurrentState == SightState.NotSeen)
        {
            TransitionState(SightState.ShortSeen);
        }
        else if (CurrentState == SightState.ShortSeen)
        {
            if (SeenTimer > 2f)
            {
                TransitionState(SightState.LongSeen);
            }
        }
    }

    private void TransitionState(SightState state)
    {
        if (CurrentState == state)
            return;

        LastState = CurrentState;
        CurrentState = state;

        switch (state)
        {
            case SightState.NotSeen:
                Behaviour.LostSight();
                SeenTimer = 0f;
                break;
            case SightState.ShortSeen:
                Behaviour.SeenPlayer();
                break;
            case SightState.LongSeen:
                Behaviour.LongSight();
                break;
        }
    }

    private enum SightState
    {
        NotSeen,
        ShortSeen,
        LongSeen
    }
}
