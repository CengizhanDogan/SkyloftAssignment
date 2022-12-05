using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticle : MonoBehaviour
{
    [SerializeField] private Transform leftParticleTransform;
    [SerializeField] private Transform rightParticleTransform;

    private PoolingManager poolingManager;

    private void Start()
    {
        poolingManager = PoolingManager.Instance;
    }
    public void LeftParticle()
    {
        poolingManager.InstantiateFromPool("WalkParticle", leftParticleTransform.position, leftParticleTransform.rotation);
    }
    public void RightParticle()
    {
        poolingManager.InstantiateFromPool("WalkParticle", rightParticleTransform.position, rightParticleTransform.rotation);
    }
}
