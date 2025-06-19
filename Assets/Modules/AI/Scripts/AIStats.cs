using UnityEngine;

[CreateAssetMenu(menuName = "AI/AI Stats")]
public class AIStats : ScriptableObject
{
    public float detectionRange = 10f;
    public float fieldOfView = 60f;
    public float damagePerHit = 10f;
}
