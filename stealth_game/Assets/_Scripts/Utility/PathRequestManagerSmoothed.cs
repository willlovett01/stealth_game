using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PathRequestManagerSmoothed : MonoBehaviour {

    Queue<PathRequest> pathReqestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathRequestManagerSmoothed instance;
    PathFindingSmoothed pathFindingSmoothed;

    bool isProcessingPath;

    private void Awake() {
        instance = this;
        pathFindingSmoothed = GetComponent<PathFindingSmoothed>();
    }

    public static void RequestPath(TilePiece pathStart, TilePiece pathEnd, Action<TilePiece[], bool> callback) {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathReqestQueue.Enqueue(newRequest);

        // check if path request process available
        instance.TryProcessNext();
    }

    void TryProcessNext() {
        if (!isProcessingPath && pathReqestQueue.Count > 0) {
            currentPathRequest = pathReqestQueue.Dequeue();
            isProcessingPath = true;
            pathFindingSmoothed.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(TilePiece[] path, bool success) {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest {
        public TilePiece pathStart;
        public TilePiece pathEnd;
        public Action<TilePiece[], bool> callback;

        public PathRequest(TilePiece _start, TilePiece _end, Action<TilePiece[], bool> _callback) {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}

