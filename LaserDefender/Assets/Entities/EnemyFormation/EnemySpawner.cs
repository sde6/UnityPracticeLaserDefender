using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	
	private float speedCoefficient = 0.125f;
	private bool movingRight = true;
	private float xmax, xmin;
	private float speed;


	// Use this for initialization
	void Start () {
		
		//Setting x axis min/max to match camera's boundaries
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;
		speed = speedCoefficient*(xmax-xmin);
				
		foreach (Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;	
		}
	}
	
	void Update (){
		
				
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime; 
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		if(leftEdgeOfFormation < xmin){
			movingRight = true;
		} else if(rightEdgeOfFormation > xmax){
			movingRight = false;
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}
	
}
