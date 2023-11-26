using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PotionMixer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToAddToCauldron;

    [SerializeField]
    private LevelExit levelExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectsToAddToCauldron.Contains(other.gameObject))
        {
            objectsToAddToCauldron.Remove(other.gameObject);
            Destroy(other.gameObject);
            //TODO: Do something fancy to show that cauldron is bigger in size.

            if (objectsToAddToCauldron.Count == 0)
            {
                levelExit.EndLevel();
            }
        }
    }
}
