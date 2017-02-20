using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 150;
	public float projectileSpeed = 1;
	public GameObject projectile;
	public float projectileFrequency = 0.5f;

	
	void Update () {
		float probability = Time.deltaTime * projectileFrequency;
		if(Random.value < probability){
			this.Fire();
		}
	}
	
	void Fire(){
		Vector3 startPos = transform.position + new Vector3(0,-1,0);
		GameObject missle = Instantiate(projectile, startPos, Quaternion.identity) as GameObject;
		missle.rigidbody2D.velocity = new Vector2(0, -projectileSpeed);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health<=0){
				Destroy(this.gameObject);
			}
			
			
		}
	}
		
	
}
