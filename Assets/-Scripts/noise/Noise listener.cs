using UnityEngine;
using UnityEngine.AI;  

public class NoiseListener : MonoBehaviour
{
    [Tooltip("Мінімальний рівень гучності, який ворог здатен почути")]
    [Range(0f, 1f)]
    public float hearingThreshold = 0.1f;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    /// <param name="sourcePos">Джерело шуму</param>
    /// <param name="volume">Нормалізована гучність у цій точці</param>
    public void OnHeardNoise(Vector3 sourcePos, float volume)
    {
        if (volume < hearingThreshold) return;
        agent.SetDestination(sourcePos);
    }
}
