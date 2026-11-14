using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier = 0.1f;
    [SerializeField] protected Transform player;

    private Material parallaxMaterial;
    private float Playerx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parallaxMaterial = GetComponent<Renderer>().material;
        Playerx = player.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float delatx = player.position.x - Playerx;
        parallaxMaterial.mainTextureOffset += new Vector2(delatx + parallaxMultiplier, 0);
        Playerx = player.position.x;
    }
}
