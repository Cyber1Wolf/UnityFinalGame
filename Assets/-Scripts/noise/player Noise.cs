using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    [Tooltip("������������ �����, �� ���� ���� ��� ��� 100 % �������")]
    public float maxRadius = 10f;

    /// <summary>
    /// ���������� ��� �����, ���� ������� ������� ���
    /// </summary>
    /// <param name="loudness">0-1 � �������� ����� �� (������ = 0.3, ������ = 1)</param>
    public void MakeNoise(float loudness = 1f)
    {
        float radius = loudness * maxRadius;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in hits)
        {
            NoiseListener listener = hit.GetComponent<NoiseListener>();
            if (listener != null)
            {               
                float distance = Vector3.Distance(transform.position, listener.transform.position);
                float volumeAtListener = Mathf.Clamp01(1 - distance / radius);
                listener.OnHeardNoise(transform.position, volumeAtListener);
            }
        }
    }
}
