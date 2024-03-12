using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal.Converters;
using UnityEngine;

public class Unit01StateMachine : MonoBehaviour, IEnemyDeath
{
    public float speed = 2.5f;
    public int waitTime;
    public float turnSpeed;

    GameObject pathFinder;
    GameObject map;
    public GameObject currentlySelectedObject;

    LineRenderer lineRenderer;
    GameObject investigatingVisualiser;

    TilePiece currentTile;
    TilePiece requestedTile;
    TilePiece firstTile;
    TilePiece lastTile;

    public TilePiece currentCoord;

    TilePiece[] path;
    List<Vector3> tilePiecePositions;

    int targetIndex;
    float moving;

    //state variables 
    Unit01BaseState currentState;
    Unit01StateFactory states;

    // getters and setters
    public Unit01BaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Unit01StateFactory States { get { return states; } set { states = value; } }

    public TilePiece CurrentTile { get { return currentTile; } set { currentTile = value; } }
    public TilePiece RequestedTile { get { return requestedTile; } set { requestedTile = value; } }
    public TilePiece FirstTile { get { return firstTile; } set { firstTile = value; } }
    public TilePiece LastTile { get { return lastTile; } set { lastTile = value; } }
    public TilePiece[] Path { get { return path; } set { path = value; } }

    public int TargetIndex { get { return targetIndex; } set { targetIndex = value; } }
    public float Moving { get { return moving; } set { moving = value; } }

    public LineRenderer LineRenderer { get { return lineRenderer; } set { lineRenderer = value; } }
    public GameObject InvestigatingVisualiser { get { return investigatingVisualiser; } set { investigatingVisualiser = value; } }

    public List<Vector3> TilePiecePositions { get { return tilePiecePositions; } set { tilePiecePositions = value; } }



    void Awake() {
        // set references
        pathFinder = GameObject.Find("A*");
        map = GameObject.Find("MapHex");
    }

    void Start() {

        // renders display of patrol path
        lineRenderer = gameObject.transform.Find("UI").Find("Path_render").GetComponent<LineRenderer>();

        // displays if investigating
        investigatingVisualiser = GameObject.Find("Investigating_visualiser");
        investigatingVisualiser.SetActive(false);

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

        // set line visibility if object is selected
        if (currentlySelectedObject.GetComponent<currentSelectedObject>().currentObject == gameObject) {
            lineRenderer.enabled = true;
        }
        else {
            lineRenderer.enabled = false;
        }
    }

    // based off enemy death interface, will run when enemy within certain range dies
    public void EnemyDeath(TilePiece deathLocation) {
        
        // exit current state
        currentState.ExitState();

        // set tile of other enemies death to requested tile so other enemies go to investigate there
        requestedTile = deathLocation;

        // enter into investigation state

        currentState = states.Investigating();
        currentState.EnterState();
        
    }
}


    



        






