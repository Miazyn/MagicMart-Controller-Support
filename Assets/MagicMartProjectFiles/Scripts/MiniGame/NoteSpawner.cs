using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] NotesToSpawn;
    [SerializeField] float distanceBetweenNotes;


    public List<GameObject> spawnedNotes;

    public IEnumerator CreateNotes(int counter)
    {
        while (counter > 0)
        {
            GameObject myNote = Instantiate(NotesToSpawn[Random.Range(0, NotesToSpawn.Length -1)]);
            myNote.transform.position = new Vector3(transform.position.x, transform.position.y);
            spawnedNotes.Add(myNote);
            counter--;
            yield return new WaitForSeconds(distanceBetweenNotes);
        }
    }
}
