using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private float lookRadius = 1000f;

    [SerializeField] private int damage;

    [SerializeField] private float attackFrequency = 0.5f;

    [SerializeField] private float errorAllowance = 0.1f;

    private bool attacking;

    private bool stoppedForPause;

    GameObject target;

    NavMeshAgent agent;

    private GameManager gameManager;

    AudioSource audioSource;

    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        target = gameManager.GetPlayerInstance();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PauseMenu.IsOn)
        {
            if (!stoppedForPause)
            {
                CancelInvoke("AttackPlayer");
                if (!agent.isStopped)
                {
                    agent.isStopped = true;
                    stoppedForPause = true;
                }
            }
            return;
        }
        else
        {
            try
            {
                if (stoppedForPause)
                {
                    if (agent.isActiveAndEnabled)
                    {
                        if (agent.isStopped)
                        {
                            agent.isStopped = false;
                            stoppedForPause = false;
                        }
                    }
                }
            }
            catch { }
        }

        if (target != null && agent != null)
        {
            float _distance = Vector3.Distance(target.transform.position, transform.position);
            animator.SetFloat("ForwardVelocity", (agent.velocity.magnitude / agent.speed));
            if (_distance <= lookRadius)
            {
                if (agent.isActiveAndEnabled)
                {
                    try
                    {
                        agent.SetDestination(target.transform.position);
                    }
                    catch { }
                }
                
                if (_distance <= agent.stoppingDistance + errorAllowance)
                {
                    if (!attacking)
                    {
                        InvokeRepeating("AttackPlayer", 0f, 1f / attackFrequency);
                        attacking = true;
                    }
                    FaceTarget();
                }
                else
                {
                    if (attacking)
                    {
                        CancelInvoke("AttackPlayer");
                        attacking = false;
                    }
                }
            }
        }
        else
        {
            target = gameManager.GetPlayerInstance();
            if (attacking)
            {
                CancelInvoke("AttackPlayer");
                attacking = false;
            }
        }
    }

    private void AttackPlayer()
    {
        target.GetComponent<Player>().TakeDamage(damage);
        audioSource.Play();
    }

    private void FaceTarget()
    {
        Vector3 _direction = (target.transform.position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5f);
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
