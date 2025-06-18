using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public Transform[] GetWaypoints()
    {
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }
        return children;
    }
}
