﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pieces;
    private Camera _currentcamera;
    private bool GrabbingObject => _grabbedTransform != null;
    private Transform _grabbedTransform;
    private Rigidbody _grabbedRigidbody;
    private float _currentTime;
    [SerializeField] private float timeThreshold = 0.11f;
    [SerializeField] private Transform topRight;
    [SerializeField] private Transform bottomLeft;
    private Dictionary<string, List<string>> adjacents;
    private Dictionary<string, List<string>> diagonal;
    private Dictionary<string, Transform> _piecesTransform;
    private float _adjacentDistance;
    private float _diagonalDistance;
    private float _allowedOffset = 0.4f;
    private bool _puzzleSolved;
    private bool _setupReady;


    private void Awake()
    {
        _setupReady = false;
        _puzzleSolved = false;
        _currentcamera = Camera.main;
        int index = 0;
        _piecesTransform = new Dictionary<string, Transform>();

        //Begin caca de codigo
        _adjacentDistance = Vector3.Distance(pieces[0].transform.position,pieces[1].transform.position);
        _diagonalDistance = Vector3.Distance(pieces[0].transform.position,pieces[4].transform.position);

        adjacents = new Dictionary<string, List<string>>();
        adjacents.Add("topleft", new List<string> {"top", "middleleft"});
        adjacents.Add("top", new List<string> {"topleft", "topright", "middle"});
        adjacents.Add("topright", new List<string> {"top", "middleright"});
        adjacents.Add("middleleft", new List<string> {"topleft", "middle", "lowerleft"});
        adjacents.Add("middle", new List<string> {"middleleft", "middleright", "top", "lower"});
        adjacents.Add("middleright", new List<string> {"middle", "topright", "lowerright"});
        adjacents.Add("lowerleft", new List<string> {"middleleft", "lower"});
        adjacents.Add("lower", new List<string> {"middle", "lowerleft", "lowerright"});
        adjacents.Add("lowerright", new List<string> {"lower", "middleright"});

        diagonal = new Dictionary<string, List<string>>();
        diagonal.Add("topleft", new List<string> {"middle"});
        diagonal.Add("top", new List<string> {"middleleft", "middleright",});
        diagonal.Add("topright", new List<string> {"middle"});
        diagonal.Add("middleleft", new List<string> {"top", "lower"});
        diagonal.Add("middle", new List<string> {"lowerleft", "lowerright", "topleft", "topright"});
        diagonal.Add("middleright", new List<string> {"top", "lower"});
        diagonal.Add("lowerleft", new List<string> {"middle"});
        diagonal.Add("lower", new List<string> {"middleleft", "middleright"});
        diagonal.Add("lowerright", new List<string> {"middle"});
        //end caca de codigo

        foreach (var piece in pieces)
        {
            var matRenderer = piece.GetComponent<Renderer>().material;
            var xoffset = 0.33f + 0.33f * (index % 3);
            var yoffset = index > 2 ? index > 5 ? 0.33f : 0.66f : 0.99f;
            matRenderer.mainTextureOffset = new Vector2(xoffset, yoffset);
            index++;

            var xpos = Random.Range(bottomLeft.position.x, topRight.position.x);
            var zpos = Random.Range(bottomLeft.position.z, topRight.position.z);

            _piecesTransform.Add(piece.name, piece.transform);

            piece.transform.position = new Vector3(xpos, 0f, zpos);
        }

        _setupReady = true;
    }

    private void Update()
    {
        if (_puzzleSolved == false && _setupReady)
        {
//            if (_grabbedTransform != null)
//                Debug.Log("objeto: " + _grabbedTransform.position + " MOUSE : " +
//                          _currentcamera.ScreenToWorldPoint(Input.mousePosition));
//            else
//                Debug.Log(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                //Apreta click
                RaycastHit hit;
                Ray ray = _currentcamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    _grabbedTransform = hit.transform;
                    _grabbedRigidbody = _grabbedTransform.gameObject.GetComponent<Rigidbody>();
                    _currentTime = timeThreshold + 1;
                    // Do something with the object that was hit by the raycast.
                    Debug.Log($"Le pegue a {_grabbedTransform.gameObject.name}");
                }
            }

            if (Input.GetMouseButtonUp(0) && _grabbedTransform != null)
            {
                //Suelta click
                _grabbedTransform.position =
                    new Vector3(_grabbedTransform.position.x, 0f, _grabbedTransform.position.z);

                _grabbedTransform = null;

                if (CheckIfPuzzleWasSolved())
                {
                    Debug.Log("GANASTE");
                }
                else
                {
                    Debug.Log("Todavia no ganaste");
                }
            }

            if (GrabbingObject && timeThreshold < _currentTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //Si Input getmousebuttondown es true es porque recien acaba de apretar el mouse en este frame
                    _grabbedTransform.position =
                        new Vector3(_grabbedTransform.position.x, 0.1f, _grabbedTransform.position.z);
                }


                _grabbedTransform.DOMove(ResolvePosition(), 0.1f);
            }
            else
            {
                _currentTime += Time.deltaTime;
            }
        }
    }

    private bool CheckIfPuzzleWasSolved()
    {
        _puzzleSolved = !adjacents.Any(piece => OutsideRange(piece, "adj")) &&
                        !diagonal.Any(piece => OutsideRange(piece, "diag"));
        return _puzzleSolved;
    }

    private bool OutsideRange(KeyValuePair<string, List<string>> piece, string dir)
    {
        var thisTransform = _piecesTransform[piece.Key];
        foreach (var currentTransform in _piecesTransform.Where(ie => piece.Value.Contains(ie.Key)).Select(e => e.Value)
        )
        {
            var dist = Vector3.Distance(thisTransform.position, currentTransform.position);
            Debug.Log($"{dir}: {(dir.Equals("adj") ? _adjacentDistance : _diagonalDistance)} - dist: {dist} - piezaThis: {piece.Key}");
            switch (dir)
            {
                case "adj":
                    if (dist > (1 + _allowedOffset) * _adjacentDistance ||
                        dist < (1 - _allowedOffset) * _adjacentDistance)
                        return true;
                    break;
                case "diag":
                    if (dist > (1 + _allowedOffset) * _diagonalDistance ||
                        dist < (1 - _allowedOffset) * _diagonalDistance)
                        return true;
                    break;
            }
        }

        return false;
    }

    private Vector3 ResolvePosition()
    {
        var mouseRealWorldPos = _currentcamera.ScreenToWorldPoint(Input.mousePosition);
        var x = mouseRealWorldPos.x > 0
            ? Mathf.Min(mouseRealWorldPos.x, topRight.position.x)
            : Mathf.Max(mouseRealWorldPos.x, bottomLeft.position.x);
        var z = mouseRealWorldPos.z > 0
            ? Mathf.Min(mouseRealWorldPos.z, topRight.position.z)
            : Mathf.Max(mouseRealWorldPos.z, bottomLeft.position.z);
        return new Vector3(x, 0.1f, z);
    }
}