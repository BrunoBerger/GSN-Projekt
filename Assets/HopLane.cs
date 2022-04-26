using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HopLane : MonoBehaviour
{
    GameObject[] lanes;
    int currentLane;
    int targetLane;
    bool currentlyJumping = false;
    float jumpStartTime;
    float percentJumped;
    Vector3 jumpStartPos;
    Vector3 jumpEndPos;

    [SerializeField] JumpPreset myJump = null;


    void Awake()
    {
        // Check if things are set correctly from the editor
        if (myJump == null)
            Debug.LogWarning("No Jump preset set!");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find & order all of the lanes in the scene and snap to the nearest
        lanes = GameObject.FindGameObjectsWithTag("lane").OrderBy(i => i.transform.position.x).ToArray();
        float initalDistance = float.MaxValue;
        foreach (var lane in lanes)
        {
            float distanceToLane = Mathf.Abs(transform.position.x - lane.transform.position.x);
            if (distanceToLane < initalDistance)
            {
                initalDistance = distanceToLane;
                currentLane = System.Array.IndexOf(lanes, lane);
            }
        }
        tpToCurrentLane();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !currentlyJumping)
            startNewJump(-1);
        else if (Input.GetMouseButtonDown(1) && !currentlyJumping)
            startNewJump(+1);

        if (currentlyJumping)
        {
            float timeJumped = Time.time - jumpStartTime;
            percentJumped = timeJumped / myJump.jumpDuration;

            Vector3 lerpPos = Vector3.Lerp(jumpStartPos, jumpEndPos, percentJumped);
            float currentJumpHeight = myJump.verticalMovement.Evaluate(percentJumped);
            transform.position = new Vector3(
                lerpPos.x, 
                lerpPos.y + currentJumpHeight, 
                0
            );

            if (timeJumped >= myJump.jumpDuration)
            {
                currentlyJumping = false;
                currentLane = targetLane;
                tpToCurrentLane(); // To correct any slight differences
            }
        }
    }

    void startNewJump(int jumpDirectionAndDistance)
    {
        currentlyJumping = true;
        targetLane = getValidLane(jumpDirectionAndDistance);
        jumpStartPos = transform.position;
        jumpEndPos = lanes[targetLane].transform.position + myJump.offsetOffLane;
        jumpStartTime = Time.time;
    }
    int getValidLane(int _lane = 0)
    {
        return Mathf.Clamp(currentLane + _lane, 0, lanes.Length - 1); ;
    }
    void tpToCurrentLane()
    {
        transform.position = new Vector3(
            lanes[getValidLane(0)].transform.position.x,
            lanes[getValidLane(0)].transform.position.y, 
            transform.position.z
        );
        transform.position += myJump.offsetOffLane;
    }
}

