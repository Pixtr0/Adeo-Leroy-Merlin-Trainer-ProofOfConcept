using UnityEngine;

public class SpinObject : MonoBehaviour
{
    Vector3 Rotation = Vector3.zero;

    private void Start()
    {
        Rotation = transform.rotation.eulerAngles;
    }
    private void FixedUpdate()
    {
        Rotation += Vector3.up * 60f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(Rotation);
    }
}
