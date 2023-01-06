using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticle : MonoBehaviour
{
    [SerializeField] private Transform leftParticleTransform;
    [SerializeField] private Transform rightParticleTransform;

    private PoolingManager poolingManager;

    private Collider coll;

    private void Start()
    {
        poolingManager = PoolingManager.Instance;
        coll = GetComponent<Collider>();
    }
    public void LeftParticle()
    {
        if (!coll.enabled) return;
        poolingManager.InstantiateFromPool("WalkParticle", leftParticleTransform.position, leftParticleTransform.rotation, false);
    }
    public void RightParticle()
    {
        if (!coll.enabled) return;
        poolingManager.InstantiateFromPool("WalkParticle", rightParticleTransform.position, rightParticleTransform.rotation, false);
    }
}
