using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NoiseListener : MonoBehaviour
{
    [Tooltip("̳�������� ����� �������, ���� ����� ������ ������")]
    [Range(0f, 1f)] public float hearingThreshold = 0.1f;
    [Tooltip("��� ������� ������ �� ��� (� ��������)")]
    public float reactionDelay = 0.5f;
    [Tooltip("��� ������ �� ���� ���� ����� �����������")]
    public float investigateDuration = 3f;
    private NavMeshAgent agent;
    private bool isInvestigating = false;
    private Coroutine currentInvestigation;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    /// ����� ����������� ��� ���������� ����
    /// <param name="sourcePos">������� ������� ����</param>
    /// <param name="volume">������� � ����� ������� (0-1)</param>
    public void OnHeardNoise(Vector3 sourcePos, float volume)
    {
        if (volume < hearingThreshold) return;
        if (currentInvestigation != null)
        {
            StopCoroutine(currentInvestigation);
        }

        currentInvestigation = StartCoroutine(InvestigateSound(sourcePos));
    }
    private IEnumerator InvestigateSound(Vector3 sourcePos)
    {
        isInvestigating = true;
        Vector3 lookDir = sourcePos - transform.position;
        lookDir.y = 0;
        if (lookDir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(lookDir);
            float t = 0;
            while (t < 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);
                t += Time.deltaTime * 2f;
                yield return null;
            }
        }        
        yield return new WaitForSeconds(reactionDelay);
        agent.SetDestination(sourcePos);
        while (Vector3.Distance(transform.position, sourcePos) > agent.stoppingDistance + 0.1f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(investigateDuration);
        isInvestigating = false;
        currentInvestigation = null;
    }
}
