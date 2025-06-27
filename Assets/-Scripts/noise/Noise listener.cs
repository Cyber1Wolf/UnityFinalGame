using UnityEngine;
using UnityEngine.AI;  

public class NoiseListener : MonoBehaviour
{
    [Tooltip("̳�������� ����� �������, ���� ����� ������ ������")]
    [Range(0f, 1f)]
    public float hearingThreshold = 0.1f;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    /// <param name="sourcePos">������� ����</param>
    /// <param name="volume">������������ ������� � ��� �����</param>
    public void OnHeardNoise(Vector3 sourcePos, float volume)
    {
        if (volume < hearingThreshold) return;
        agent.SetDestination(sourcePos);
    }
}
