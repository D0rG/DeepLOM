using UnityEngine;

[AddComponentMenu("_DeepLOM/FirePoint")]
public class FirePoint : MonoBehaviour
{
    [SerializeField][Range(0, 10f)] private float fireLifeTime;
    [SerializeField] private GameObject fireParticle;
    private float putOutFireSpeed;
    private float currLifeTime;

    private void Awake()
    {
        currLifeTime = fireLifeTime;
        putOutFireSpeed = Time.fixedDeltaTime / fireLifeTime;
    }

    public void PutOutFire()
    {
        if (currLifeTime <= 0)
        {
            fireParticle.active = false;
        }

        if (fireParticle.active) 
        {
            currLifeTime -= putOutFireSpeed * Time.deltaTime; 
        }
    }

    public void StartBurning()
    {
        currLifeTime = fireLifeTime;
        fireParticle.active = true;
    }
}