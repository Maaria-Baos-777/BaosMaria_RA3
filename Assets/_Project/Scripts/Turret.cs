using UnityEngine;

public class Turret : MonoBehaviour
{
	public Transform player;
	public float visionRange = 6f;
	public GameObject bulletPrefab;
	private float timer;
	public float bulletSpeed = 0.1f;
	public float timeBetweenShots = 10.1f;
	[SerializeField] private Transform firePoint;


	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= timeBetweenShots)
		{
			Shot();
			timer = 0;
		}
	}
	void Shot()
	{
		GameObject bullet = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
		Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
		bulletRb.linearVelocity =- firePoint.right * bulletSpeed;

		Destroy(bullet, 3f);
	}
}
