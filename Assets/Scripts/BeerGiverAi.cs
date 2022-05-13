using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeerGiverAi : MonoBehaviour
{
    GameObject[] lanes;
    int currentLane;
    int targetLane;
    bool currentlyJumping = false;
    float jumpStartTime;
    float percentJumped;
    Vector3 jumpStartPos;
    Vector3 jumpEndPos;

    [SerializeField] GameState gameState;
    [SerializeField] JumpPreset jumpPreset = null;

    [SerializeField, Range(0f, 1f)] float jumpRate;
    [SerializeField] float minimumTimeBetweenJumps;
    [SerializeField] float maximumTimeBetweenJumps;
    [SerializeField] float timeSinceLastJump = 0;

    [SerializeField] GameObject beerPrefab;
    [SerializeField] StreetGenerator streetGenerator;

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
        TpToCurrentLane();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastJump += Time.deltaTime;
        if (!currentlyJumping && timeSinceLastJump > minimumTimeBetweenJumps)
        {
            dropBeer();
            if (Random.value < 0.4f)
                StartNewJump(-1);
            else if (Random.value < 0.8f)
                StartNewJump(+1);
            else
                StartNewJump(0);
        }


        if (currentlyJumping)
        {
            float timeJumped = Time.time - jumpStartTime;
            percentJumped = timeJumped / jumpPreset.jumpDuration;

            Vector3 lerpPos = Vector3.Lerp(jumpStartPos, jumpEndPos, percentJumped);
            float currentJumpHeight = jumpPreset.verticalMovement.Evaluate(percentJumped);
            transform.position = new Vector3(
                lerpPos.x,
                lerpPos.y + currentJumpHeight,
                transform.position.z
            );

            if (timeJumped >= jumpPreset.jumpDuration)
            {
                currentlyJumping = false;
                currentLane = targetLane;
                TpToCurrentLane(); // To correct any slight differences
            }
        }  
    }


    void dropBeer()
    {
        Vector3 dropPos = new Vector3(
            lanes[currentLane].gameObject.transform.position.x,
            lanes[currentLane].gameObject.transform.position.y,
            transform.position.z
        );
        GameObject newBeer = Instantiate(beerPrefab, dropPos, Quaternion.identity);
        //TODO play vfx + sfx
        streetGenerator._decorationQueue.Enqueue(newBeer);
    }

    void StartNewJump(int jumpDirectionAndDistance)
    {
        currentlyJumping = true;
        timeSinceLastJump = 0;

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

