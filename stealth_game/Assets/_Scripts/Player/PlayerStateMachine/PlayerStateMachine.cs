using System.Collections.Generic;
using UnityEngine;
using System;



public class PlayerStateMachine : MonoBehaviour {

    // references
    Camera gameCamera;
    public LayerMask layerMask;
    GameObject pathFinder;
    GameObject map;

    //state variables
    [SerializeField]
    PlayerStateBase currentState;
    PlayerStateFactory states;

    // player attributes
    [SerializeField]
    float speed;
    [SerializeField]
    float playerNoiseLevel;

    [SerializeField]
    TilePiece currentTile;


    // player controls
    bool isMoveRequested = false;
    [SerializeField]
    bool isStealth = true;

    // for path finding
    TilePiece requestedTile;
    TilePiece[] path;
    List<Vector3> tilePiecePositions;

    int targetIndex;

    // UI
    [SerializeField]
    LineRenderer lineRenderer;

    // getters and setters
    public Camera GameCamera { get { return gameCamera; } set { gameCamera = value; } }
    public GameObject PathFinder { get { return pathFinder; } set { pathFinder = value; } }
    public GameObject Map { get { return map; } set { map = value; } }

    public PlayerStateBase CurrentState { get { return currentState; }  set { currentState = value; } }
    public PlayerStateFactory States { get { return states; } set { states = value; } }

    public float Speed { get { return speed; } set { speed = value; } }
    public float PlayerNoiseLevel { get { return playerNoiseLevel; } set { playerNoiseLevel = value; } }

    public TilePiece CurrentTile { get { return currentTile; } set { currentTile = value; } }
    
    public bool IsMoveRequested { get { return isMoveRequested; } set { isMoveRequested = value; } }
    public bool IsStealth { get { return isStealth; } set { isStealth = value; } }

    public TilePiece RequestedTile { get { return requestedTile; } set { requestedTile = value; } }
    public TilePiece[] Path { get { return path; } set { path = value; } }
    public List<Vector3> TilePiecePositions { get { return tilePiecePositions; } set { tilePiecePositions = value; } }
    public int TargetIndex { get { return targetIndex; } set { targetIndex = value; } }

    public LineRenderer LineRenderer { get { return lineRenderer; } set { lineRenderer = value; } }


    void Awake() {

        // assign reference variables
        gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        pathFinder = GameObject.Find("A*");
        map = GameObject.Find("MapHex");
        //lineRenderer = GameObject.Find("Path_render").GetComponent<LineRenderer>();

        // setup state
        states = new PlayerStateFactory(this);
        currentState = states.Idle();
        currentState.EnterState();

        // setup random position
        currentTile = map.GetComponent<MapGeneratorHex>().GetRandomTile();
        transform.position = new Vector3(currentTile.gameObject.transform.position.x, transform.position.y, currentTile.gameObject.transform.position.z);

    }

    // Update is called once per frame
    void Update() {

        // check inputs
        CheckMouseClick();
        CheckForCtrlToggle();

        // update current state
        currentState.UpdateState();
    }

    void CheckMouseClick() {
        // get mouse point on ground
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, layerMask)) {
                if (hitInfo.collider.GetComponent<TilePiece>() != null) {

                    // set requested tile
                    requestedTile = hitInfo.collider.GetComponent<TilePiece>();

                    // switch to walking state
                    isMoveRequested = true;
                    
                }
            }
        }
    }

    void CheckForCtrlToggle() {
        if (Input.GetKeyDown(KeyCode.LeftControl)) { 
            if (!isStealth) {
                isStealth = true;
            }
            else {
                isStealth = false;
            }
        }
    }
}



                      













