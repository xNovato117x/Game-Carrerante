using UnityEngine;

public class GineteController : MonoBehaviour
{
    [SerializeField] private Transform gineteRef;

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(gineteRef.position, gineteRef.rotation);
    }
}
