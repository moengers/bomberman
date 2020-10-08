using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    int seed = 2306;

    Vector3 position;
    //1-100 100=jedesmal
    int spawnrateHeathpack;
    int spawnrateExtraBomb;

    public bool destroyedByExplosion;

    public GameObject hearthPrefab;
    public GameObject bobPrefab;

    // Start is called before the first frame update
    void Start()
    {
        destroyedByExplosion = false;
        spawnrateHeathpack = 40;
        spawnrateExtraBomb = 40;
        position = this.transform.position;
        Random.InitState(seed);
    }

    private void OnDestroy()
    {
        if (destroyedByExplosion)
        {
            SpawnUpgrade();
        }
    }

    private void SpawnUpgrade()
    {
        int i = Random.Range(1, 101);
        
        if (i < spawnrateHeathpack)
        {
            Instantiate(hearthPrefab, position , Quaternion.identity);
        }
        //Falls kein Herz spawnt wird geprüft, ob eine extra Bombe spawnen könnte
        else if(i < spawnrateHeathpack + spawnrateExtraBomb)
        {
            Instantiate(bobPrefab, position, Quaternion.Euler(-90,0,-90));
        }
    }
}
