using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    Transform playerTransform;
    Vector3 posData;
    float moveSpeed = 2 ;
    private Vector3 moveDirection;

    // Use this for initialization
    void Start () {
        playerTransform = this.transform;
        if (playerTransform)
        {
            posData = playerTransform.localPosition;
        }
	}
    Vector3 target = Vector3.zero;
    Vector3 moveToward = Vector3.zero;

    // Update is called once per frame
    void Update () {

        if (LevelGeneratorManager.levelGeneratorInstance && !LevelGeneratorManager.levelGeneratorInstance.isGameInitialize)
            return;

        if (Input.GetButton("Fire1"))
        {
            moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("get position.."+moveToward);
            moveDirection = moveToward - posData;
            moveDirection.z = 0;
            moveDirection.Normalize();
            posData = playerTransform.localPosition;
            target = moveDirection * moveSpeed + posData;
            target.y = posData.y;
            playerTransform.localPosition = Vector3.Lerp(posData, target, Time.deltaTime);
        }
       

    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.tag == "Thewall")
        {                       
            
            if(LevelGeneratorManager.levelGeneratorInstance)
            {
                LevelGeneratorManager.levelGeneratorInstance.assignGameOverdata();
            } else {
                Destroy(this.gameObject);
            }
        }
    }

}
