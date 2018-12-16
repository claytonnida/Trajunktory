using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour
{
    // The array of all spawnable sprites.
    public Sprite[] spawnables;
    // The array of objects that have been spawned.
    public GameObject[] spawnedObjects;
    // The number of spawned objects in the game.
    public int numObjects;

	void Start ()
    {
        spawnables = new Sprite[15];
        spawnedObjects = new GameObject[100];

        // Load our sprites into an array.
        for (int i = 1; i < 16; i++)
        {
            spawnables[i-1] = Resources.Load<Sprite>("TestingSpawn/Asset " + i);
        }
	}
	
	void Update ()
    {
        // Check if there is an object in the first position of the array.
        if(spawnedObjects[0])
        {
            // Then check to see how far it has fallen down.
            // If it has fallen beyond -15 then destroy it and decrease the object counter.
            if(spawnedObjects[0].transform.position.y < -15)
            {
                Destroy(spawnedObjects[0]);
                numObjects--;
            }        
        }
        // Loop through, and check for null spots in the array.
        // If one is null move it to the end of the array.
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
           if(spawnedObjects[i] == null)
            {
                MoveNullsToEnd(spawnedObjects, i);
            }
        }

    }

    // This is the method that creates a new random object.
    public void SpawnRandomObject()
    {
        // Create new empty GameObject.
        GameObject spawnedObj = new GameObject("spawnedObj" + numObjects);
        // Set it's position at the cent of the hole.
        spawnedObj.transform.SetPositionAndRotation(new Vector2(-0.37f, 4.365f), new Quaternion(0, 0, 1, 0));
        // Add the sprite renderer and a random sprite.
        SpriteRenderer sr = spawnedObj.AddComponent<SpriteRenderer>();
        sr.sprite = spawnables[UnityEngine.Random.Range(0,14)];
        // Give the object a rigid body so that the objects can be effected by gravity.
        spawnedObj.AddComponent<Rigidbody2D>();
        // Give it a collider so that it can collide with other objects.
        spawnedObj.AddComponent<PolygonCollider2D>();
        // Randomize the objects rotation.
        spawnedObj.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));
        // Put the objects layer infront of the background.
        sr.sortingOrder = 1;
        // Add it to our array of objects in the game, and increase the counter.
        spawnedObjects[numObjects++] = spawnedObj;
    }

    // Moves null values to the end of the array.
    private void MoveNullsToEnd(GameObject[] gameObjects, int index)
    {
        for (int i = index; i < gameObjects.Length - 1; i++)
        {
            GameObject temp = gameObjects[i];
            gameObjects[i] = gameObjects[i + 1];
            gameObjects[i + 1] = temp;
        }
    }
    
}
