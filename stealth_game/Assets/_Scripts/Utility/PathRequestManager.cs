using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour {

    Queue<PathRequest> pathReqestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathRequestManager instance;
    PathFinding pathFinding;

    bool isProcessingPath;

    private void Awake() {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
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
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
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
