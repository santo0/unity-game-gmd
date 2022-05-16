using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private int BOT_CEN = 0; //Bottom center position
    private int BOT_LEF = 1; //Bottom left position
    private int BOT_RIG = 2; //Bottom right position
    private int LAT_BOT_LEF = 3; //Lateral left position
    private int LAT_BOT_RIG = 4; //Lateral right position

    private int LAT_TOP_LEF = 5; //Lateral left position
    private int LAT_TOP_RIG = 6; //Lateral right position



    public float rayPosOffsetY = 0.7f;
    public float rayPosOffsetX = 0.7f;
    public float rayLength = 0.3f;
    public BoxCollider2D col;

    private Vector3[] RaysPos;

    private bool isGrounded;
    private bool isOnLedge;

    private bool isAtWall;

    private bool isTopRight;
    private bool isTopLeft;
    private bool isBotRight;
    private bool isBotLeft;

    delegate bool IsValidCollider(Collider2D col);
    IsValidCollider IsLevel = delegate (Collider2D col)
    { return col.gameObject.layer == LayerMask.NameToLayer("Level"); };
    IsValidCollider IsPlatform = delegate (Collider2D col)
    { return col.gameObject.layer == LayerMask.NameToLayer("Platform"); };
    IsValidCollider IsWalkable = delegate (Collider2D col)
    {
        return col.gameObject.layer == LayerMask.NameToLayer("Level") ||
               col.gameObject.layer == LayerMask.NameToLayer("Platform");
    };


    private void Awake()
    {
        RaysPos = new Vector3[7]; //7 rays
    }


    public void drawRays()
    {

        Debug.DrawRay(RaysPos[BOT_CEN], Vector2.down * rayLength, Color.red);
        Debug.DrawRay(RaysPos[BOT_LEF], Vector2.down * rayLength, Color.green);
        Debug.DrawRay(RaysPos[BOT_RIG], Vector2.down * rayLength, Color.blue);
        Debug.DrawRay(RaysPos[LAT_TOP_LEF], Vector2.left * rayLength, Color.yellow);
        Debug.DrawRay(RaysPos[LAT_TOP_RIG], Vector2.right * rayLength, Color.red);
        Debug.DrawRay(RaysPos[LAT_BOT_LEF], Vector2.left * rayLength, Color.yellow);
        Debug.DrawRay(RaysPos[LAT_BOT_RIG], Vector2.right * rayLength, Color.red);
    }

    private void FixedUpdate()
    {

        var yHalfExtents = col.bounds.extents.y;
        //get the center
        var colCenterY = col.bounds.center.y;
        //get the lower border
        var colLowerY = (colCenterY - yHalfExtents);
        var colHighY = colCenterY + yHalfExtents;

        RaysPos[BOT_CEN] = new Vector3(transform.position.x, colLowerY, 0);
        RaysPos[BOT_LEF] = new Vector3(transform.position.x - rayPosOffsetY, colLowerY, 0);
        RaysPos[BOT_RIG] = new Vector3(transform.position.x + rayPosOffsetY, colLowerY, 0);

        RaysPos[LAT_TOP_LEF] = new Vector3(transform.position.x - rayPosOffsetX, colHighY, 0);
        RaysPos[LAT_BOT_LEF] = new Vector3(transform.position.x - rayPosOffsetX, colCenterY, 0);

        RaysPos[LAT_TOP_RIG] = new Vector3(transform.position.x + rayPosOffsetX, colHighY, 0);
        RaysPos[LAT_BOT_RIG] = new Vector3(transform.position.x + rayPosOffsetX, colCenterY, 0);

        drawRays();

        isGrounded = checkIfGrounded();

        isTopLeft = checkRayCastHits(RaysPos[LAT_TOP_LEF], Vector2.left, IsWalkable);
        isBotLeft = checkRayCastHits(RaysPos[LAT_BOT_LEF], Vector2.left, IsWalkable);
        isTopRight = checkRayCastHits(RaysPos[LAT_TOP_RIG], Vector2.right, IsWalkable);
        isBotRight = checkRayCastHits(RaysPos[LAT_BOT_RIG], Vector2.right, IsWalkable);

        isOnLedge = checkIfOnLedge();

        isAtWall = checkIfWall();


        //printCollisionState();


    }

    void printCollisionState()
    {
        Debug.Log("ground=" + isGrounded + ", isTopLeft=" + isTopLeft + ", isTopRight=" +
        isTopRight + ", isBotLeft=" + isBotLeft + ", isBotRight=" + isBotRight +
        ", isOnLedge=" + isOnLedge + ", isAtWall=" + isAtWall);
    }

    public bool isPlayerGrounded()
    {
        return isGrounded;
    }

    public bool isPlayerOnLedge()
    {
        return isOnLedge;
    }

    public bool isPlayerAtWall()
    {
        return isAtWall;
    }

    public bool isCollTopRight()
    {
        return isTopRight;
    }

    public bool isCollTopLeft()
    {
        return isTopLeft;
    }

    public bool isCollBotRight()
    {
        return isBotRight;
    }
    public bool isCollBotLeft()
    {
        return isBotLeft;
    }

    private bool checkIfGrounded()
    {
        return checkRayCastHits(RaysPos[BOT_CEN], Vector2.down, IsWalkable) ||
               checkRayCastHits(RaysPos[BOT_LEF], Vector2.down, IsWalkable) ||
               checkRayCastHits(RaysPos[BOT_RIG], Vector2.down, IsWalkable);
    }

    //hit.collider.tag == "Level"
    private bool checkRayCastHits(Vector3 origin, Vector2 dir, IsValidCollider validate)
    {
        RaycastHit2D[] HitList = Physics2D.RaycastAll(origin, dir, rayLength);
        foreach (RaycastHit2D hit in HitList)
        {
            if (hit.collider != null && validate(hit.collider)) return true;
        }
        return false;
    }

    private bool checkIfOnLedge()
    {
        return (!isTopRight && isBotRight && !isBotLeft && !isTopLeft) || (!isTopLeft && isBotLeft && !isBotRight && !isTopRight);
    }

    private bool checkIfWall()
    {
        return (isTopRight && isBotRight) || (isTopLeft && isBotLeft);
    }

}
