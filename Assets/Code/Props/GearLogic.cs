using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearLogic : Prop {

    public float Torque = 1f;
    public float MaxAngularVelocity = 150f;

	private void FixedUpdate ()
    {
        if(rigidbody2D.angularVelocity < MaxAngularVelocity)
            rigidbody2D.AddTorque(Torque);
	}
}
