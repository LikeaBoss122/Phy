using TMPro;
using UnityEngine;

public class SphereStoper : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Sphere")
            return;

        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        _resultText.text = rigidbody.velocity.magnitude.ToString();
        Destroy(collision.gameObject);
    }
}