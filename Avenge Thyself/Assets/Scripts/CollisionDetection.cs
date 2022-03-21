using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private int BOT_CEN = 0; //Bottom center position
    private int BOT_LEF = 1; //Bottom left position
    private int BOT_RIG = 2; //Bottom right position
    private int LAT_LEF = 3; //Lateral left position
    private int LAT_RIG = 4; //Lateral right position

    public float rayPosOffsetY = 0.7f;
    public float rayPosOffsetX = 0.7f;
    public float rayLength = 0.3f;
    public BoxCollider2D col;

    private Vector3[] RaysPos;

    private bool isGrounded;
    private bool isLeftWall;
    private bool isRightWall;


    private void Awake() {
        RaysPos = new Vector3[5]; //5 rays
    }


    public void drawRays() {

        Debug.DrawRay(RaysPos[BOT_CEN], Vector2.down * rayLength, Color.red);
        Debug.DrawRay(RaysPos[BOT_LEF], Vector2.down * rayLength, Color.green);
        Debug.DrawRay(RaysPos[BOT_RIG], Vector2.down * rayLength, Color.blue);
        Debug.DrawRay(RaysPos[LAT_LEF], Vector2.left * rayLength, Color.yellow);
        Debug.DrawRay(RaysPos[LAT_RIG], Vector2.right * rayLength, Color.red);
    }

    private void FixedUpdate() {

        var yHalfExtents = col.bounds.extents.y;
        //get the center
        var colCenterY = col.bounds.center.y;
        //get the lower border
        var colLowerY =(colCenterY - yHalfExtents);

        RaysPos[BOT_CEN] = new Vector3(transform.position.x, colLowerY, 0);
        RaysPos[BOT_LEF] = new Vector3(transform.position.x -rayPosOffsetY, colLowerY, 0);
        RaysPos[BOT_RIG] = new Vector3(transform.position.x + rayPosOffsetY, colLowerY, 0);

        RaysPos[LAT_LEF] = new Vector3(transform.position.x -rayPosOffsetX, colCenterY, 0);
        RaysPos[LAT_RIG] = new Vector3(transform.position.x + rayPosOffsetX, colCenterY, 0);
    
        drawRays();

        isGrounded = checkIfGrounded();

        isLeftWall = checkIfLeftWall();

        isRightWall = checkIfRightWall();

        Debug.Log("ground="+isGrounded+", left="+ isLeftWall+", right=%s"+isRightWall);

    }


    public bool isPlayerGrounded() {
        return isGrounded;
    }

    public bool isPlayerWithLeftWall() {
        return isLeftWall;
    }

    public bool isPlayerWithRightWall() {
        return isRightWall;
    }


    public bool checkIfGrounded() {
        RaycastHit2D[] GroundHitsCenter = Physics2D.RaycastAll(RaysPos[BOT_CEN], Vector2.down, rayLength);
        RaycastHit2D[] GroundHitsLeft = Physics2D.RaycastAll(RaysPos[BOT_LEF], Vector2.down, rayLength);
        RaycastHit2D[] GroundHitsRight = Physics2D.RaycastAll(RaysPos[BOT_RIG], Vector2.down, rayLength);

        RaycastHit2D[][] AllRaycastHits = {GroundHitsCenter, GroundHitsLeft, GroundHitsRight};

        foreach(RaycastHit2D[] HitList in AllRaycastHits) {
            if(checkRayCastHits(HitList)) return true;
        }

        return false;
    }

    public bool checkRayCastHits(RaycastHit2D[] HitList){
        foreach(RaycastHit2D hit in HitList) {
            if (hit.collider != null && hit.collider.tag == "Level") return true;
        }
        return false;
    }

    public bool checkIfLeftWall(){
        RaycastHit2D[] LeftLateralHits = Physics2D.RaycastAll(RaysPos[LAT_LEF], Vector2.left, rayLength);
        return checkRayCastHits(LeftLateralHits);
    }

    public bool checkIfRightWall(){
        RaycastHit2D[] RightLateralHits = Physics2D.RaycastAll(RaysPos[LAT_RIG], Vector2.right, rayLength);
        return checkRayCastHits(RightLateralHits);
    }

}
