using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;
using UnityEngine;

public class RopeCreating : MonoBehaviour
{
    [ContextMenu("CreateRopeParticles")]
    private void Create10SegmentsOfRope()
    {
        Joint firstParticleOfRope = transform.GetChild(1).GetComponent<Joint>();
        Stack<Joint> otherRopes = new Stack<Joint>();
        
        foreach (var item in GetComponentsInChildren<Joint>())
        {
            otherRopes.Push(item);
        }

        for (int i = 0; i < 10; i++)
        {
            var newObj = Instantiate(firstParticleOfRope, otherRopes.Peek().transform.position - Vector3.down * -0.4f, otherRopes.Peek().transform.rotation, transform);
            newObj.GetComponent<Joint>().connectedBody = otherRopes.Peek().transform.GetComponent<Rigidbody>();
            otherRopes.Push(newObj);
        }

    }
}
