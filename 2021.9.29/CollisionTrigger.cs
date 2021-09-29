using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public Transform[] mSwordHitablePoints;
    public string enemyName;
    public int baseAttackPoint = 0;
    private Vector3[] mSwordHitablePointPrevPositions;
    public ParticleSystem collisionEffect2;
    public int toughnessKill;
    int collisonPart = -1;
    bool ifGround = false;
    int endCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        mSwordHitablePointPrevPositions = new
            Vector3[mSwordHitablePoints.Length];
        for (int i = 0; i < mSwordHitablePoints.Length; i++)
        {
            mSwordHitablePointPrevPositions[i] =
                mSwordHitablePoints[i].position;
        }
    }
    public void stopEffect2()
    {
        collisonPart = -1;
        //collisionEffect2.Stop();
    }
    public void moveEffect()
    {
        if (!collisionEffect2.isPlaying)
        {
            collisionEffect2.Play();
        }
        collisionEffect2.transform.position =
            mSwordHitablePoints[collisonPart].position;
    }
    public bool collisionTrigger()
    {
        RaycastHit hinfo = new RaycastHit();
        int mask = 1 << LayerMask.NameToLayer(enemyName);
        GameObject hitObj = null;
        for (int i = 0; i < mSwordHitablePointPrevPositions.Length; i++)
        {
            if (Physics.Linecast(mSwordHitablePoints[i].position,
                mSwordHitablePointPrevPositions[i], out hinfo, mask))
            {
                hitObj = hinfo.collider.gameObject;
                mSwordHitablePointPrevPositions[i] =
                    mSwordHitablePoints[i].position;
                if (collisonPart==-1&& hitObj != null)
                {
                    collisionEffect2.Stop();
                    collisonPart = i;
                    collisionEffect2.transform.position =
                  mSwordHitablePointPrevPositions[i];
                    collisionEffect2.Play();
                }
                break;
            }
        }
        if (collisonPart == -1) return false;
        collisionEffect2.transform.position =
                  mSwordHitablePoints[collisonPart].position;
            // 击中
        if (enemyName == "Player")
        {
            try
            {
                PlayerKeyBoardController mon = hitObj.
                    GetComponent<PlayerKeyBoardController>();
                mon.beingAttack(transform, toughnessKill);
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            try
            {
                Troll mon = hitObj.GetComponent<Troll>();
                mon.beingAttack(transform, toughnessKill);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //end
    }
}
