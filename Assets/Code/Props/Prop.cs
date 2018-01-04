using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour {

    protected new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        DrawLine.OnStart += changeType;
    }

    private void OnDestroy()
    {
        DrawLine.OnStart -= changeType;
    }

    private void changeType() {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}
