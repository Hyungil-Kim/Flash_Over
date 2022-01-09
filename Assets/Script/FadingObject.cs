using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
public class FadingObject : MonoBehaviour, IEquatable<FadingObject>
{
    public List<Renderer> Renderers = new List<Renderer>();
    public Vector3 Position;
    public List<Material> Materials = new List<Material>();
    [HideInInspector]
    public float InitialAlpha;

    public Vector3Int cellpos;
    private Tilemap tilemap;
    private void Awake()
    {
        Position = transform.position;
        if (Renderers.Count == 0)
        {
            Renderers.AddRange(GetComponentsInChildren<Renderer>());
        }
        for (int i = 0; i < Renderers.Count; i++)
        {
            Materials.AddRange(Renderers[i].materials);
        }

        InitialAlpha = Materials[0].color.a;

        tilemap = GetComponentInParent<Tilemap>();
        cellpos = tilemap.WorldToCell(transform.position);
        AllTile.wallTile.Add(this);
    }

    private void Start()
    {

    }
    public bool Equals(FadingObject other)
    {
        return Position.Equals(other.Position);
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode();
    }
}
