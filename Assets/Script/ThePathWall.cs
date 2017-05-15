using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePathWall : MonoBehaviour {
    public Transform[] originPiont;
    public Transform[] destinationPiont;
    Vector2 locationPointLeft;
    Vector2 locationPointRight;
    Vector2 desPointLeft;
    Vector2 desPointRight;
    float varControllerForMoving = 0.5f;
    Transform wallTransform;

    // Use this for initialization
    void Start() {
        wallTransform = this.transform;
        posData = moveToward = wallTransform.localPosition;
        posData.y -= varControllerForMoving;
       /* if (originPiont != null && originPiont.Length > 0) {
            locationPointLeft = originPiont[0].localPosition;
            locationPointRight = originPiont[1].localPosition;
         }

        if (destinationPiont != null && destinationPiont.Length > 0)
        {
            desPointLeft = destinationPiont[0].localPosition;
            desPointRight = destinationPiont[1].localPosition;
        }*/
    }
    Vector3 moveToward;
    Vector3 posData;
    Vector3 target;
    Vector3 moveDirection;
    void Update()
    {
        //Debug.Log("get position.."+moveToward);
        moveToward = posData;
        if (moveToward.y < -5.5f)
        {
            if(LevelGeneratorManager.levelGeneratorInstance && this.gameObject.name != "Triangle" && this.gameObject.name != "THeFireBall")
            {
                LevelGeneratorManager.levelGeneratorInstance.createRandomLevelParam(Random.Range(1,4));
                LevelGeneratorManager.levelGeneratorInstance.gameObjectOnLevelScroll.Remove(LevelGeneratorManager.levelGeneratorInstance.gameObjectOnLevelScroll[0]);
            }
            Destroy(this.gameObject);
        }

        moveDirection = moveToward;
        moveDirection.y -= varControllerForMoving;
        moveDirection.z = 0;
        //moveDirection.Normalize();
        posData = wallTransform.localPosition;
        target = moveDirection;
        wallTransform.localPosition = Vector3.Slerp(posData, target, Time.deltaTime*1.5f);
    }

    /// <summary>
    /// getting origin location
    /// </summary>
    public Vector2 getOriginLocationPoint (bool isLeft)
    {
        if (isLeft)
        {
            if (locationPointLeft == null)
            {
                locationPointLeft = originPiont[0].localPosition;
            }
            return locationPointLeft;
        }
        else
        {
            if (locationPointRight == null)
            {
                locationPointRight = originPiont[1].localPosition;
            }
            return locationPointRight;
        }
    }

    /// <summary>
    /// getting end location points
    /// </summary>
    public Vector2 getEndLocationPoint(bool isLeft)
    {
        if (isLeft)
        {
            if (desPointLeft == null)
            {
                desPointLeft = destinationPiont[0].localPosition;
            }
            return desPointLeft;
        }
        else
        {
            if (desPointRight == null)
            {
                desPointRight = destinationPiont[1].localPosition;
            }
            return desPointRight;
        }
    }
}
