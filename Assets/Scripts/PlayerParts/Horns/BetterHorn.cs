using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterHorn : HornTemplate {
    
    public override void Start() {
        Damage = 10f;
        base.Start();
    }

}