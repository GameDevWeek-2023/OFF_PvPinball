using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkPrefabBase : NetworkBehaviour
{
    public Rigidbody rigidbody;
    public Collider collider;
    public override void OnStartServer()
    {
        base.OnStartServer();

        
    }

    public override void OnStartClient()
    {
        if (!isServer)
        {
            base.OnStartClient();
            if (rigidbody != null)
            {
                // only simulate ball physics on server
                Destroy(rigidbody);
            }

            if (collider != null)
            {
                Destroy(collider);
            }
        }
    }
}
