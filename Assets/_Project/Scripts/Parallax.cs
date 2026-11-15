using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier = 0.1f;
    [SerializeField] protected Transform player;

    private Material parallaxMaterial;
    private float Playerx;

    void Start()
    {
        parallaxMaterial = GetComponent<Renderer>().material;
		FindPlayer();
	}

	void Update()
	{
		if (player == null) return;
		float delatx = player.position.x - Playerx;
		parallaxMaterial.mainTextureOffset += new Vector2(delatx * parallaxMultiplier, 0);
		Playerx = player.position.x;
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		FindPlayer();
	}

	private void FindPlayer()
	{
		if (Player._instance != null)
		{
			player = Player._instance.transform;
			Playerx = player.position.x;
		}
	}
}
