using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    [Tooltip("Максимальний радіус, на який чути шум при 100 % гучності")]
    public float maxRadius = 10f;

    [Tooltip("Шари, які містять слухачів шуму")]
    public LayerMask listenerLayer;

    /// <summary>
    /// Викликайте цей метод, коли гравець створює шум
    /// </summary>
    /// <param name="loudness">0-1 — наскільки гучна дія (ходьба = 0.3, постріл = 1)</param>
    public void MakeNoise(float loudness = 1f)
    {
        loudness = Mathf.Clamp01(loudness);
        float radius = loudness * maxRadius;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, listenerLayer);

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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }
}
