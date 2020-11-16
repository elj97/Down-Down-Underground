using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newlaserscript : MonoBehaviour
{
    public GameObject RangedSpellPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space) )
            RangedSpell();
    }

    RangedAttack()
    {
        Vector3 SpawnLaser = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        GameObject clone;
        clone = Instantiate(RangedSpellPrefab, SpawnLaser, Quaternion.identity);
        selectedUnit.transform.GetComponent<RangedSpell>().Target = selectedUnit;
    }
}
