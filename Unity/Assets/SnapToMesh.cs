using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapToMesh : MonoBehaviour
{

    [Header("Materials")]
    [SerializeField] Material DefaultMaterial;
    [SerializeField] Material CloseMaterial;
    [SerializeField] Material CorrectMaterial;
    [SerializeField] Material IncorrectMaterial;
    [Space]
    [Header("Settings")]
    [SerializeField] float SnappingRange;
    [SerializeField] bool InstantSnap;
    public bool Locked;

    public UnityEvent OnSnapObject;

    Mesh mesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GetComponent<Renderer>().material = DefaultMaterial;
        mesh = GetComponent<MeshFilter>().sharedMesh;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Locked)
        {
            GetComponent<Renderer>().material = DefaultMaterial;
            Collider[] Colliders = Physics.OverlapSphere(transform.position, SnappingRange);
            for (int i = 0; i < Colliders.Length; i++)
            {
                Collider col = Colliders[i];
                if (col.CompareTag("Snapable"))
                {
                    col.TryGetComponent(out XRGrabInteractable xr);
                    GetComponent<Renderer>().material = CloseMaterial;
                    if (InstantSnap && col.GetComponent<MeshFilter>().sharedMesh == mesh)
                    {
                        Destroy(col.GetComponent<XRGrabInteractable>());
                        col.transform.SetParent(transform, false);
                        Destroy(col.GetComponent<Rigidbody>());
                        col.transform.localPosition = Vector3.zero;
                        col.transform.localRotation = Quaternion.identity;
                        GetComponent<Renderer>().enabled = false;
                        Locked = true;
                        OnSnapObject.Invoke();
                    }
                }
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SnappingRange);
    }
}
