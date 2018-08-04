using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFinDefault : BackFinTemplate {

    public override void Start() {
        MovementSpeed = 10;
        MovementAcceleration = 10;
        base.Start();
    }

}
