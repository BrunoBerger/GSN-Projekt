using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JumpBetweenLanes : MonoBehaviour
{
    GameObject[] lanes;
    int currentLane;
    int targetLane;
    bool currentlyJumping = false;
    float jumpStartTime;
    float percentJumped;
    Vector3 jumpStartPos;
    Vector3 jumpEndPos;

    [SerializeField] JumpPreset jumpPreset = null;


    void checkSetup()
    {
        // Check if things are set correctly from the editor
        if (jumpPreset == null)
        {
            Debug.LogWarning("No Jump preset set!");
            enabled = false;
        } 
        if (lanes.Length<1) 
        {
            Debug.LogWarning("Nothing tagged 'lane' found!");
            enabled = false;
        }

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
        checkSetup();
        TpToCurrentLane();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !currentlyJumping)
            StartNewJump(-1);
        else if (Input.GetMouseButtonDown(1) && !currentlyJumping)
            StartNewJump(+1);

        if (currentlyJumping)
        {
            float timeJumped = Time.time - jumpStartTime;
            percentJumped = timeJumped / jumpPreset.jumpDuration;

            Vector3 lerpPos = Vector3.Lerp(jumpStartPos, jumpEndPos, percentJumped);
            float currentJumpHeight = jumpPreset.verticalMovement.Evaluate(percentJumped);
            transform.position = new Vector3(
                lerpPos.x, 
                lerpPos.y + currentJumpHeight, 
                0
            );

            if (timeJumped >= jumpPreset.jumpDuration)
            {
                currentlyJumping = false;
                currentLane = targetLane;
                TpToCurrentLane(); // To correct any slight differences
            }
        }
    }

    void StartNewJump(int jumpDirectionAndDistance)
    {
        currentlyJumping = true;
        targetLane = GetValidLane(jumpDirectionAndDistance);
        jumpStartPos = transform.position;
        jumpEndPos = lanes[targetLane].transform.position + jumpPreset.offsetOffLane;
        jumpStartTime = Time.time;
    }
    int GetValidLane(int _lane = 0)
    {
        return Mathf.Clamp(currentLane + _lane, 0, lanes.Length - 1); ;
    }
    void TpToCurrentLane()
    {
        transform.position = new Vector3(
            lanes[GetValidLane(0)].transform.position.x,
            lanes[GetValidLane(0)].transform.position.y, 
            transform.position.z
        );
        transform.position += jumpPreset.offsetOffLane;
    }
}

