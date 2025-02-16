using UnityEngine;
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

    Mesh mesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Renderer>().material = DefaultMaterial;
        mesh = GetComponent<MeshFilter>().sharedMesh;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Renderer>().material = DefaultMaterial;
        Collider[] Colliders = Physics.OverlapSphere(transform.position, SnappingRange);
        for (int i = 0; i < Colliders.Length; i++)
        {
            Collider col = Colliders[i];
            if (col.CompareTag("Snapable"))
            {
                GetComponent<Renderer>().material = CloseMaterial;
                if (col.GetComponent<MeshFilter>().mesh = mesh)
                {
                    col.TryGetComponent(out XRGrabInteractable xr);
                    if (InstantSnap)
                    {
                        Destroy(col.GetComponent<XRGrabInteractable>());
                        Destroy(col.GetComponent<Rigidbody>());
                        col.transform.position = transform.position;
                        col.transform.rotation = transform.rotation;
                        col.transform.SetParent(transform, false);
                        GetComponent<Renderer>().enabled = false;
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
