using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit01StateMachine : MonoBehaviour, IMakeSound
{
    public float speed = 2.5f;
    public int waitTime;
    public float turnSpeed;

    [SerializeField]
    GameObject pathFinder;
    [SerializeField]
    GameObject map;
    [SerializeField]
    GameObject currentlySelectedObject;
    public GameObject player;

    // weapons
    public Unit01Gun gun;

    // displays
    LineRenderer lineRenderer;
    GameObject investigatingVisualiser;
    GameObject chasingVisualiser;
    GameObject visionConeVisualiser;

    // tiles
    [SerializeField]
    TilePiece currentTile;
    [SerializeField]
    TilePiece requestedTile;
    TilePiece firstTile;
    TilePiece lastTile;
    TilePiece playerTile;

    public TilePiece currentCoord;

    [SerializeField]
    int targetIndex;

    [SerializeField]
    TilePiece[] path;
    List<Vector3> tilePiecePositions;

    // unit attributes

    float moving;
    [SerializeField]
    bool isInvestigating;
    [SerializeField]
    bool seePlayer;
    bool hearSound;
    bool hearingOnCooldown;
    bool stunned;

    //state variables 
    Unit01BaseState currentState;
    Unit01StateFactory states;

    // getters and setters
    public Unit01BaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Unit01StateFactory States { get { return states; } set { states = value; } }

    public GameObject Player { get { return player; } set { player = value; } }
    public Unit01Gun Gun { get { return gun; } set { gun = value; } }

    public TilePiece CurrentTile { get { return currentTile; } set { currentTile = value; } }
    public TilePiece RequestedTile { get { return requestedTile; } set { requestedTile = value; } }
    public TilePiece FirstTile { get { return firstTile; } set { firstTile = value; } }
    public TilePiece LastTile { get { return lastTile; } set { lastTile = value; } }
    public TilePiece PlayerTile { get { return playerTile; } set { playerTile = value; } }
    public TilePiece[] Path { get { return path; } set { path = value; } }

    public int TargetIndex { get { return targetIndex; } set { targetIndex = value; } }
    public float Moving { get { return moving; } set { moving = value; } }
    public bool IsInvestigating { get { return isInvestigating; } set { isInvestigating = value; } }
    public bool HearSound { get { return hearSound; } set { hearSound = value; } }
    public bool SeePlayer { get { return seePlayer; } set { seePlayer = value; } }
    public bool Stunned { get { return stunned; } set { stunned = value; } }

    public LineRenderer LineRenderer { get { return lineRenderer; } set { lineRenderer = value; } }
    public GameObject InvestigatingVisualiser { get { return investigatingVisualiser; } set { investigatingVisualiser = value; } }
    public GameObject ChasingVisualiser { get { return chasingVisualiser; } set { chasingVisualiser = value; } }
    public GameObject VisionConeVisualiser { get { return visionConeVisualiser; } set { visionConeVisualiser = value; } }


    public List<Vector3> TilePiecePositions { get { return tilePiecePositions; } set { tilePiecePositions = value; } }

    void Awake() {

        // assign references
        visionConeVisualiser = transform.Find("UI/Unit_01_vision_visualiser").gameObject;
        investigatingVisualiser = transform.Find("UI/Investigating_visualiser").gameObject;
        chasingVisualiser = transform.Find("UI/Chasing_visualiser").gameObject;
        LineRenderer = transform.Find("UI/Path_render").gameObject.GetComponent<LineRenderer>();
    }

    void Start() {

        // displays if investigating
        investigatingVisualiser.SetActive(false);
        chasingVisualiser.SetActive(false);

        // initialise empty list for tile piece positions, this list will be used to generate a path of tiles to walk along
        tilePiecePositions = new List<Vector3>();

        // get a random tile for start and end to generate an inital random patrol path
        currentTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();
        requestedTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();

        // save the random tiles to variables so we can return back to our original path (currentTile and requestedTiles will be changing)
        firstTile = currentTile;
        lastTile = requestedTile;

        // set inital position of unit
        transform.position = new Vector3(currentTile.gameObject.transform.position.x, transform.position.y, currentTile.gameObject.transform.position.z);

        // setup state
        states = new Unit01StateFactory(this);
        currentState = states.Patrolling();
        currentState.EnterState();
    }

    private void Update() {

        // update current state
        currentState.UpdateState();

        // set line visibility if object is selected
        if (currentlySelectedObject.GetComponent<currentSelectedObject>().currentObject == gameObject) {
            lineRenderer.enabled = true;
        }

        else {
            lineRenderer.enabled = false;
        }
    }
        
    // if enemy hears a sound
    public void MakeSound(TilePiece soundLocation) {

        if (!hearingOnCooldown) {
            // exit current state
            //currentState.ExitState();

            // set tile of sound to requested tile 
            requestedTile = soundLocation;

            // enter into investigation state
            hearSound = true;
            StartCoroutine(HearingCooldownTimer());

        }
    }

    public void OnSeePlayer() {
        if (seePlayer == false) {
            seePlayer = true;
        }
    }

    // will get the current tile player is on, used in the chasing state to continuously aim at player
    public void GetPlayerTile() {
        playerTile = player.GetComponent<PlayerStateMachine>().CurrentTile;
    }

    // cooldown timer for hearing, starts when emeny hears a sound (prevents it triggering hearing constantly and bugging out)
    IEnumerator HearingCooldownTimer() {
        hearingOnCooldown = true;
        yield return new WaitForSeconds(4);
        hearingOnCooldown = false;
    }
}


        
        






       


    



        






