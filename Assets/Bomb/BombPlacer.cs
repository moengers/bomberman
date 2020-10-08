using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlacer : MonoBehaviour
{
    GameObject[] allBombsArray;
    GameObject[] allBarrelArray;
    GameObject[] allPlayerArray;
    GameObject[] allHeartArray;
    GameObject[] allUpgradeBombArray;
    
    //Noch Hardcodet
    //public int explotionRange = 2;

    public GameObject bombPrefab;

    float bombExplotionTime = 2f;

    public GameObject explosionPrefab1;

    private void Update()
    {
        
    }
    
    public void PlaceABomb(Vector3 position)
    {
        int x = (int)Math.Round(position.x);
        int z = (int)Math.Round(position.z);
        //Bomben dürfen nicht auf Bomben platziert werden
        bool isBombOnABomb = false;
        allBombsArray = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject possibleBomb in allBombsArray)
        {
            if(possibleBomb.transform.position.x == x && possibleBomb.transform.position.z == z)
            {
                Debug.Log("Es ist nicht möglich eine Bombe auf eine Bombe zu setzen");
                isBombOnABomb = true;
            }
        }
        if (!isBombOnABomb)
        {
            GameObject bomb = Instantiate(bombPrefab, position, Quaternion.Euler(-90, 0, -90));

            StartCoroutine(StartBombExpoltionCoroutine(bomb, x, z));
        }
    }

    IEnumerator StartBombExpoltionCoroutine(GameObject bomb, int x, int z)
    {
        //falls es sich um eine Kettenreaktion handelt, wird nicht 2 Sekunden gewartet
        yield return new WaitForSeconds(bombExplotionTime);
        
        //GameObject explosion = Instantiate(explosionPrefab1, position + new Vector3(0, 1f, 0), Quaternion.Euler(0, 0, 0));
        if(!(bomb == null))
        {
            HandleExplotion(bomb, x, z);
        }
        //explosion.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //DestroyAroundObjekts(bomb.transform.position);

        yield return new WaitForSeconds(0.25f);

        Destroy(bomb);
    }

    void GetAllBarrelsAndBombsandPlayer()
    {
        allBarrelArray = GameObject.FindGameObjectsWithTag("Barrel");
        allBombsArray = GameObject.FindGameObjectsWithTag("Bomb");
        allPlayerArray = GameObject.FindGameObjectsWithTag("PlayerUnit");
        allHeartArray = GameObject.FindGameObjectsWithTag("Heart");
        allUpgradeBombArray = GameObject.FindGameObjectsWithTag("UpgradeBomb");

    }
    //Veraltet
    /*
    private void DestroyAroundObjekts(Vector3 bombPosition)
    {
        foreach(GameObject gameObject in GetAroundBombsAndBarrels(bombPosition))
        {
            Destroy(gameObject);
        }
    }*/

    GameObject[] GetAroundBombsAndBarrelsII(Vector3 bombPosition)
    {
        GameObject[] gameObjectsAround = { null, null, null, null, null, null, null, null };

        return gameObjectsAround;
    }
    //Veraltet
    /*
    GameObject[] GetAroundBombsAndBarrels(Vector3 bombPosition)
    {
        GetAllBarrelsAndBombsandPlayer();
        //[x+1, x+2, x-1, x-2, z+1, z+2, z-1, z-2]
        GameObject[] gameObjectsAround = { null, null, null, null, null, null, null, null };
        int x = (int) bombPosition.x;
        int z = (int) bombPosition.z;

        bool foundxPlus = false;
        bool foundxMinus = false;
        bool foundzPlus = false;
        bool foundzMinus = false;

        //Debug.Log("x: " + x + "   z: " + z);
        //Wenn x gerade liegt Bombe im x Korridor
        if (x%2 == 0)
        {
            //ist da ein GameObject
            foreach (GameObject barrel in allBarrelArray)
            {
                //is a Barrel in Range of 2?
                int barrelz = (int)barrel.transform.position.z;
                int barrelx = (int)barrel.transform.position.x;

                

                if (barrelz == z + 1)
                {
                    if (barrelx == x)
                    {
                        Debug.Log("Found z+1");
                        gameObjectsAround[0] = barrel;
                        gameObjectsAround[1] = null;
                        foundzPlus = true;
                    }                    
                }
                else if(!foundzPlus)
                {                    
                    if (barrelz == z + 2)
                    {
                        if (barrelx == x)
                        {
                            Debug.Log("Found z+2");
                            gameObjectsAround[1] = barrel;
                            foundzPlus = true;
                        }
                    }                        
                }  
                
                if (barrelz == z - 1)
                {
                    if (barrelx == x)
                    {
                        Debug.Log("Found z-1");
                        gameObjectsAround[2] = barrel;
                        foundzMinus = true;
                        gameObjectsAround[3] = null;
                    }
                }
                else if(!foundzMinus)
                {
                    if (barrelz == z - 2)
                    {
                        if (barrelx == x)
                        {
                            Debug.Log("Found z-2");
                            gameObjectsAround[3] = barrel;
                            foundzMinus = true;
                        }
                    }                        
                }                   
            }
            //ist da eine andere Bombe?
            foreach (GameObject bomb in allBombsArray)
            {
                //is a bomb in Range of 2?
                int bombz = (int)bomb.transform.position.z;
                int bombx = (int)bomb.transform.position.x;

                if (bombz == z + 1)
                {
                    if(bombx == x)
                    {
                        gameObjectsAround[0] = bomb;
                        foundzPlus = true;
                        gameObjectsAround[1] = null;
                    }
                }
                else if(!foundzPlus)
                {
                    if (bombz == z + 2)
                    {
                        if (bombx == x)
                        {
                            gameObjectsAround[1] = bomb;
                            foundzPlus = true;
                        }
                    }                        
                }
                    
                   
                if (bombz == z - 1)
                {
                    if (bombx == x)
                    {
                        gameObjectsAround[2] = bomb;
                        foundzMinus = true;
                        gameObjectsAround[3] = null;
                    }
                }
                else if(!foundzMinus)
                {
                    if (bombz == z - 2)
                    {
                        if (bombx == x)
                        {
                            gameObjectsAround[3] = bomb;
                            foundzMinus = true;
                        }
                    }                        
                }
                    
            }
        }
        //Gleiche nochmal mit x und z vertauscht
        if (z % 2 == 0)
        {
            //ist da ein GameObject
            foreach (GameObject barrel in allBarrelArray)
            {
                //is a Barrel in Range of 2?
                int barrelx = (int)barrel.transform.position.x;
                int barrelz = (int)barrel.transform.position.z;

                if (barrelx == x + 1)
                {
                    if (barrelz == z)
                    {
                        Debug.Log("Found x+1");
                        gameObjectsAround[4] = barrel;
                        foundxPlus = true;
                        gameObjectsAround[5] = null;
                    }
                }
                else if (foundxPlus);
                {
                    if (barrelx == x + 2)
                    {
                        if (barrelz == z)
                        {
                            Debug.Log("Found x+2");
                            gameObjectsAround[5] = barrel;
                            foundxPlus = true;
                        }
                    }
                }

                if (barrelx == x - 1)
                {
                    if (barrelz == z)
                    {
                        Debug.Log("Found x-1");
                        gameObjectsAround[6] = barrel;
                        foundxMinus = true;
                        gameObjectsAround[7] = null;
                    }
                }
                else if(foundxMinus)
                {
                    if (barrelx == x - 2)
                    {
                        if (barrelz == z)
                        {
                            Debug.Log("Found x-2");
                            gameObjectsAround[7] = barrel;
                            foundxMinus = true;
                        }
                    }
                }
            }
            //ist da eine andere Bombe?
            foreach (GameObject bomb in allBombsArray)
            {
                //is a bomb in Range of 2?
                int bombx = (int)bomb.transform.position.x;
                int bombz = (int)bomb.transform.position.z;

                if (bombx == x + 1)
                {
                    if (bombz == z)
                    {
                        gameObjectsAround[4] = bomb;
                        foundxPlus = true;
                        gameObjectsAround[5] = null;
                    }
                }
                else if (!foundxPlus)
                {
                    if (bombx == x + 2)
                    {
                        if (bombz == z)
                        {
                            gameObjectsAround[5] = bomb;
                            foundxPlus = true;
                        }
                    }
                }


                if (bombx == x - 1)
                {
                    if (bombz == z)
                    {
                        gameObjectsAround[6] = bomb;
                        foundxMinus = true;
                        gameObjectsAround[7] = null;
                    }
                }
                else if(!foundxMinus)
                {
                    if (bombx == x - 2)
                    {
                        if (bombz == z)
                        {
                            gameObjectsAround[7] = bomb;
                            foundxMinus = true;
                        }
                    }
                }

            }
        }

        return gameObjectsAround;
    }*/

    void HandleExplotion(GameObject bomb, int x, int z)
    {
        if(bomb.GetComponent<bombIsChainReaktion>().isChainreaction == false)
        {
            if (!(bomb == null))
            {
                InExplotionRangeObject[] inExplotionRangeObjects = GetExplotionRangeWithObjects(GetExplodingRange(x, z));
                int[,] correctedExplotionrange = GetCorrectedExplotionRange(inExplotionRangeObjects);
            
                PlaceExplotions(correctedExplotionrange);

                //Destroy Around Barrels
                DestroyAroundBarrels(inExplotionRangeObjects);

                //Destroy Around Bombs + Chainreaktion
                DestroyAroundBombs(inExplotionRangeObjects);

                //Destroy Around Hearts
                DestroyAroundHearts(inExplotionRangeObjects);

                //Destroy Around Upgrade Bombs
                DestroyAroundUpgradeBobs(inExplotionRangeObjects);

                //Damge the Players
                DamdamagePlayer(correctedExplotionrange);
            }
        }
        else
        {
            if (!(bomb == null))
            {
                InExplotionRangeObject[] inExplotionRangeObjects = GetExplotionRangeWithObjects(GetExplodingRange(x, z));
                int[,] correctedExplotionrange = GetCorrectedExplotionRange(inExplotionRangeObjects);
            
                PlaceExplotions(correctedExplotionrange);

                //Destroy Around Barrels
                DestroyAroundBarrels(inExplotionRangeObjects);

                //Destroy Around Bombs + Chainreaktion
                DestroyAroundBombs(inExplotionRangeObjects);

                //Destroy Around Hearts
                DestroyAroundHearts(inExplotionRangeObjects);

                //Destroy Around Upgrade Bombs
                DestroyAroundUpgradeBobs(inExplotionRangeObjects);

                //Damge the Players
                DamdamagePlayer(correctedExplotionrange);
                bomb.GetComponent<bombIsChainReaktion>().isChainreaction = false;
            }

        }
    }

    void DestroyAroundBarrels(InExplotionRangeObject[] ieroArr)
    {
        for(int i = 0; i < ieroArr.Length; i++)
        {
            if(ieroArr[i].Object != null)
            {
                if (ieroArr[i].Object.tag == "Barrel")
                {
                    GameObject barrel = ieroArr[i].Object;
                    barrel.GetComponent<Barrel>().destroyedByExplosion = true;
                    Destroy(barrel);
                }
            }
        }
    }

    void DestroyAroundBombs(InExplotionRangeObject[] ieroArr)
    {
        for (int i = 0; i < ieroArr.Length; i++)
        {
            if (ieroArr[i].Object != null)
            {
                if (ieroArr[i].Object.tag == "Bomb")
                {
                    GameObject bomb = ieroArr[i].Object;
                    bomb.GetComponent<bombIsChainReaktion>().isChainreaction = true;
                    Debug.Log("Chainreaktion soll gestartet werden");
                    StartCoroutine(ChainReaktionCoroutine(bomb, (int)bomb.transform.position.x, (int)bomb.transform.position.z));
                }
            }
        }
    }
    IEnumerator ChainReaktionCoroutine(GameObject bomb, int x, int z)
    {
        if (!(bomb == null))
        {
            Debug.Log("Handle soll ausgeführt werden");
            yield return new WaitForSeconds(0.3f);
            HandleExplotion(bomb, x, z);
            yield return new WaitForSeconds(0.25f);
            Destroy(bomb);
        }
    }


    void DestroyAroundHearts(InExplotionRangeObject[] ieroArr)
    {
        for (int i = 0; i < ieroArr.Length; i++)
        {
            if (ieroArr[i].Object != null)
            {
                if (ieroArr[i].Object.tag == "Heart")
                {
                    Destroy(ieroArr[i].Object);
                }
            }
        }
    }

    void DestroyAroundUpgradeBobs(InExplotionRangeObject[] ieroArr)
    {
        for (int i = 0; i < ieroArr.Length; i++)
        {
            if (ieroArr[i].Object != null)
            {
                if (ieroArr[i].Object.tag == "UpgradeBomb")
                {
                    Destroy(ieroArr[i].Object);
                }
            }
        }
    }


    void PlaceExplotions(int[,] explotionrange)
    {
        for(int i = 0; i <=8; i++)
        {
            int ix = explotionrange[i, 0];
            int iz = explotionrange[i, 1];
            if(ix != 100 && iz != 100)
            {
                StartCoroutine(PlaceExplotionCoroutine(ix, iz));
            }
        }
    }

    IEnumerator PlaceExplotionCoroutine(int x, int z)
    {
        GameObject explosion = Instantiate(explosionPrefab1, new Vector3(x, 1f, z), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(1f);
        Destroy(explosion);
    }

    //Gibt x und z Werte in abhäniggkeit der Eingabe in einer Kreutzform zurück
    //   |
    // --|--
    //   |
    //Wenn dort ein Mauerstück ist wird eine null eingetragen (rein logisch betrachtet)
    // 0 = original
    // 1,2 = rechts
    // 3,4 = links
    // 5,6 = oben
    // 7,8 = unten
    // +1,+2
    int[,] GetExplodingRange(int x, int z)
    {
        int[,] explodingFields = new int[9,2];

        explodingFields[0, 0] = x;
        explodingFields[0, 1] = z;
        for (int i = 1; i<=8; i++)
        {
            explodingFields[i, 0] = 100;
            explodingFields[i, 1] = 100;
        }
        
        if(x%2 == 0)
        {
            if( z == 6)
            {
                explodingFields[7, 0] = x;
                explodingFields[7, 1] = z - 1;

                explodingFields[8, 0] = x;
                explodingFields[8, 1] = z - 2;
            }
            else if(z == 5)
            {
                explodingFields[7, 0] = x;
                explodingFields[7, 1] = z - 1;

                explodingFields[8, 0] = x;
                explodingFields[8, 1] = z - 2;

                explodingFields[5, 0] = x;
                explodingFields[5, 1] = z + 1;
            }
            else if (z == -6)
            {
                explodingFields[5, 0] = x;
                explodingFields[5, 1] = z + 1;

                explodingFields[6, 0] = x;
                explodingFields[6, 1] = z + 2;
            }
            else if (z == -5)
            {
                explodingFields[5, 0] = x;
                explodingFields[5, 1] = z + 1;

                explodingFields[6, 0] = x;
                explodingFields[6, 1] = z + 2;

                explodingFields[7, 0] = x;
                explodingFields[7, 1] = z - 1;
            }
            else
            {
                explodingFields[5, 0] = x;
                explodingFields[5, 1] = z + 1;

                explodingFields[6, 0] = x;
                explodingFields[6, 1] = z + 2;

                explodingFields[7, 0] = x;
                explodingFields[7, 1] = z - 1;

                explodingFields[8, 0] = x;
                explodingFields[8, 1] = z - 2;
            }
        }

        if (z % 2 == 0)
        {
            if (x == 6)
            {
                explodingFields[3, 0] = x - 1;
                explodingFields[3, 1] = z;

                explodingFields[4, 0] = x - 2;
                explodingFields[4, 1] = z;
            }
            else if (x == 5)
            {
                explodingFields[3, 0] = x - 1;
                explodingFields[3, 1] = z;

                explodingFields[4, 0] = x - 2;
                explodingFields[4, 1] = z;

                explodingFields[1, 0] = x + 1;
                explodingFields[1, 1] = z;
            }
            else if (x == -6)
            {
                explodingFields[1, 0] = x + 1;
                explodingFields[1, 1] = z;

                explodingFields[2, 0] = x + 2;
                explodingFields[2, 1] = z;
            }
            else if (x == -5)
            {
                explodingFields[1, 0] = x + 1;
                explodingFields[1, 1] = z;

                explodingFields[2, 0] = x + 2;
                explodingFields[2, 1] = z;

                explodingFields[3, 0] = x - 1;
                explodingFields[3, 1] = z;
            }
            else
            {
                explodingFields[1, 0] = x + 1;
                explodingFields[1, 1] = z;

                explodingFields[2, 0] = x + 2;
                explodingFields[2, 1] = z;

                explodingFields[3, 0] = x - 1;
                explodingFields[3, 1] = z;

                explodingFields[4, 0] = x - 2;
                explodingFields[4, 1] = z;
            }            
        }
        
        return explodingFields;
        
    }

    //Gibt für die Exploding Range alle darauf liegenden Gameobjecte zurück, die in Range sind
    // 0 = original
    // 1,2 = rechts
    // 3,4 = links
    // 5,6 = oben
    // 7,8 = unten
    // +1,+2
    InExplotionRangeObject[] GetExplotionRangeWithObjects(int[,] normalExplotionRange)
    {
        //Debug.Log("GetExplotionRangeWithObjects  x: " + normalExplotionRange[1, 0] + "   z: " + normalExplotionRange[1, 1]);

        //Update Barrel, Bomb, Player Arrays
        GetAllBarrelsAndBombsandPlayer();

        InExplotionRangeObject[] inExplotionRangeObjects = new InExplotionRangeObject[9];
        inExplotionRangeObjects[0] = new InExplotionRangeObject(null, normalExplotionRange[0, 0], normalExplotionRange[0, 1]);
        inExplotionRangeObjects[1] = new InExplotionRangeObject(null, 100, 100);
        inExplotionRangeObjects[2] = new InExplotionRangeObject(null, 100, 100);
        inExplotionRangeObjects[3] = new InExplotionRangeObject(null, 100, 100);
        inExplotionRangeObjects[4] = new InExplotionRangeObject(null, 100, 100);
        inExplotionRangeObjects[5] = new InExplotionRangeObject(null, 100, 100);
        inExplotionRangeObjects[6] = new InExplotionRangeObject(null, 100, 100);
        inExplotionRangeObjects[7] = new InExplotionRangeObject(null, 100, 100);
        inExplotionRangeObjects[8] = new InExplotionRangeObject(null, 100, 100);

        //Auf Position 0 ist nur der Startwert
        //+2 da erst alle in Range von 1 untersucht werden
        for (int i = 1; i <9; i = i+2)
        {
            //Ist auf der Position ein Barrel in erster Reihe
            //Wird nur geprüft wenn da noch kein Object ist
            if (inExplotionRangeObjects[i].Object == null)
            {
                
                inExplotionRangeObjects[i].x = normalExplotionRange[i, 0];
                inExplotionRangeObjects[i].z = normalExplotionRange[i, 1];
                foreach (GameObject barrel in allBarrelArray)
                {
                    int barrelx = (int)barrel.transform.position.x;
                    int barrelz = (int)barrel.transform.position.z;
                    if (normalExplotionRange[i, 0] == barrelx && normalExplotionRange[i, 1] == barrelz)
                    {
                        inExplotionRangeObjects[i].Object = barrel;
                    }
                }
            }
            //Ist auf der Posion eine Bombe in erster Reihe
            //Wird nur geprüft wenn da noch kein Object ist
            if (inExplotionRangeObjects[i].Object == null)
            {
                foreach (GameObject bomb in allBombsArray)
                {
                    int bombx = (int)bomb.transform.position.x;
                    int bombz = (int)bomb.transform.position.z;
                    if (normalExplotionRange[i, 0] == bombx && normalExplotionRange[i, 1] == bombz)
                    {
                        inExplotionRangeObjects[i].Object = bomb;
                    }
                }
            }
            //Ist auf der Posion ein Herz in erster Reihe
            //Wird nur geprüft wenn da noch kein Object ist
            if (inExplotionRangeObjects[i].Object == null)
            {
                foreach (GameObject heart in allHeartArray)
                {
                    int heartx = (int)heart.transform.position.x;
                    int heartz = (int)heart.transform.position.z;
                    if (normalExplotionRange[i, 0] == heartx && normalExplotionRange[i, 1] == heartz)
                    {
                        inExplotionRangeObjects[i].Object = heart;
                    }
                }
            }
            //Ist auf der Posion eine Upgrade Bombe in erster Reihe
            //Wird nur geprüft wenn da noch kein Object ist
            if (inExplotionRangeObjects[i].Object == null)
            {
                foreach (GameObject upBomb in allUpgradeBombArray)
                {
                    int upBombx = (int)upBomb.transform.position.x;
                    int upBombz = (int)upBomb.transform.position.z;
                    if (normalExplotionRange[i, 0] == upBombx && normalExplotionRange[i, 1] == upBombz)
                    {
                        inExplotionRangeObjects[i].Object = upBomb;
                    }
                }
            }

        }
        //Nun wird die 2. Reihe überprüft, aber nur wenn davor in der Reihe nichts gefunden wurde
        for (int i = 2; i <= 9; i = i + 2)
        {
            
            //Wenn das Glied davor leer ist, dann liegt in der inneren Reihe kein Object
            if (inExplotionRangeObjects[i - 1].Object == null)
            {
                //Wenn davor kein Object in erster Reihe steht, werden hier auch die zweite reihe übergeben
                inExplotionRangeObjects[i].x = normalExplotionRange[i, 0];
                inExplotionRangeObjects[i].z = normalExplotionRange[i, 1];
                //Ist auf der Position ein Barrel in zweiter Reihe
                //Wird nur geprüft wenn da noch kein Object ist
                if (inExplotionRangeObjects[i].Object == null)
                {
                    foreach (GameObject barrel in allBarrelArray)
                    {
                        int barrelx = (int)barrel.transform.position.x;
                        int barrelz = (int)barrel.transform.position.z;
                        if (normalExplotionRange[i, 0] == barrelx && normalExplotionRange[i, 1] == barrelz)
                        {
                            inExplotionRangeObjects[i].Object = barrel;
                        }
                    }
                }
                //Ist auf der Posion eine Bombe in zweiter Reihe
                //Wird nur geprüft wenn da noch kein Object ist
                if (inExplotionRangeObjects[i].Object == null)
                {
                    foreach (GameObject bomb in allBombsArray)
                    {
                        int bombx = (int)bomb.transform.position.x;
                        int bombz = (int)bomb.transform.position.z;
                        if (normalExplotionRange[i, 0] == bombx && normalExplotionRange[i, 1] == bombz)
                        {
                            inExplotionRangeObjects[i].Object = bomb;
                        }
                    }
                }
                //Ist auf der Posion ein Herz in zweiter Reihe
                //Wird nur geprüft wenn da noch kein Object ist
                if (inExplotionRangeObjects[i].Object == null)
                {
                    foreach (GameObject heart in allHeartArray)
                    {
                        int heartx = (int)heart.transform.position.x;
                        int heartz = (int)heart.transform.position.z;
                        if (normalExplotionRange[i, 0] == heartx && normalExplotionRange[i, 1] == heartz)
                        {
                            inExplotionRangeObjects[i].Object = heart;
                        }
                    }
                }
                //Ist auf der Posion eine Upgrade Bombe in zweiter Reihe
                //Wird nur geprüft wenn da noch kein Object ist
                if (inExplotionRangeObjects[i].Object == null)
                {
                    foreach (GameObject upBomb in allUpgradeBombArray)
                    {
                        int upBombx = (int)upBomb.transform.position.x;
                        int upBombz = (int)upBomb.transform.position.z;
                        if (normalExplotionRange[i, 0] == upBombx && normalExplotionRange[i, 1] == upBombz)
                        {
                            inExplotionRangeObjects[i].Object = upBomb;
                        }
                    }
                }

            }

        }
        //Die inizieriende Bombe mus in [0] gelöscht werden
        inExplotionRangeObjects[0].Object = null;

        //Debug
        //for (int i = 0; i <= 8; i++)
        //{
        //    if (inExplotionRangeObjects[i].Object == null)
        //    {
        //        Debug.Log("GetExplotionRangeWithObjects: " + i + " O: " + "NULL" + "  x: " + inExplotionRangeObjects[i].x + "  z :" + inExplotionRangeObjects[i].z);
        //    }
        //    else
        //    {
        //        Debug.Log("GetExplotionRangeWithObjects: " + i + " O: " + inExplotionRangeObjects[i].Object.tag + "  x: " + inExplotionRangeObjects[i].x + "  z :" + inExplotionRangeObjects[i].z);
        //    }
        //}


        return inExplotionRangeObjects;
    }

    int [,] GetCorrectedExplotionRange(InExplotionRangeObject[] inExplotionRangeObjects)
    {
        int[,] correctedExplotionRange = new int[9, 2];
        //Debug.Log("GetCorrectedExplotionRange  x: " + inExplotionRangeObjects[1].x + "   z: " + inExplotionRangeObjects[1].z);
        /*Debug.Log("GetCorrectedExplotionRange  x: " + inExplotionRangeObjects[3].x + "   z: " + inExplotionRangeObjects[3].z);
        Debug.Log("GetCorrectedExplotionRange  x: " + inExplotionRangeObjects[5].x + "   z: " + inExplotionRangeObjects[5].z);
        Debug.Log("GetCorrectedExplotionRange  x: " + inExplotionRangeObjects[7].x + "   z: " + inExplotionRangeObjects[7].z);*/
        int i = 0;
        foreach (InExplotionRangeObject iero in inExplotionRangeObjects)
        {
            if(iero.x == 0 && iero.z == 0 && iero.Object == null)
            {

            }
            else
            {
                correctedExplotionRange[i, 0] = iero.x;
                correctedExplotionRange[i, 1] = iero.z;
            }

            i++;
        }
        return correctedExplotionRange;
    }


    void DamdamagePlayer(int[,] explotionRange)
    {
        for (int i = 0; i <= 8; i++)
        {
            int ix = explotionRange[i, 0];
            int iz = explotionRange[i, 1];

            GameObject lostPlayer;

            if (ix != 100 && iz != 100)
            {
                foreach(GameObject player in allPlayerArray)
                {
                    if(Math.Round(player.transform.position.x) == ix && Math.Round(player.transform.position.z) == iz)
                    {
                        int health =  player.GetComponent<PlayerUnit>().MinusOneHealth();
                        if(health == 0)
                        {
                            player.GetComponent<PlayerUnit>().ShowLost();
                            lostPlayer = player;
                            //Benachricht alle anderen, dass sie gewonnen haben
                            foreach (GameObject winningPlayer in allPlayerArray)
                            {
                                if (winningPlayer != lostPlayer)
                                {
                                    winningPlayer.GetComponent<PlayerUnit>().ShowWin();
                                }
                            }
                        }
                        
                    }
                }
            }
        }
    }



    class InExplotionRangeObject
    {
        public GameObject Object;
        public int x, z;

        public InExplotionRangeObject(GameObject @object, int x, int z)
        {
            Object = @object;
            this.x = x;
            this.z = z;
        }
    }

}
