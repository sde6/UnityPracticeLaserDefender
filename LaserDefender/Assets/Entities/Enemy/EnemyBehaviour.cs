using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 150;
	public float projectileSpeed = 1;
	public GameObject projectile;
	public float projectileFrequency = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
	}
	
	void Update () {
		float probability = Time.deltaTime * projectileFrequency;
		if(Random.value < probability){
			this.Fire();
		}
	}
	
	void Fire(){
		GameObject missle = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		missle.rigidbody2D.velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health<=0){
				this.Death();
			}
			
		}
	}
	
	void Death(){
		Destroy(this.gameObject);
		scoreKeeper.Score(scoreValue);
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
	}
		
	
}
