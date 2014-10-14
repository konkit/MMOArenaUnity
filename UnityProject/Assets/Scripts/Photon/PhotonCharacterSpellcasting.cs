using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhotonCharacterSpellcasting : MonoBehaviour {

    //Spell prefabs
    public GameObject[] prefabs;

    //List with spell data from server
    public List<SpellPossession> spellPossession;


    // setting of current spellCasting
    public int currentSpellNum = 1;

    public float spellHeight = 1.4f;
    public float currentCooldown = 0.0f;

    public CharacterControlInterface controlInterface;

    PhotonView photonView;

    // Use this for initialization
    void Start()
    {
        controlInterface = GetComponent<CharacterControlInterface>();
        photonView = GetComponent<PhotonView>();
        LoadSpellPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        if (spellPossession == null)
        {
            return;
        }

        if (currentCooldown > 0.0f)
        {
            currentCooldown -= Time.deltaTime * 10;
        }

        if (controlInterface.isPunch)
        {
            Debug.Log("Punch button activated");

            //castCurrentSpell();
            CastSpell();
        }
        else if (controlInterface.previousSpell)
        {
            PreviousSpell();
        }
        else if (controlInterface.nextSpell)
        {
            NextSpell();
        }
    }

    void castCurrentSpell()
    {
        if (currentCooldown <= 0.0f)
        {
            MouseController mouse = GetComponent<MouseController>();
            if (mouse) mouse.AlignRotation();

            Spell cntSpell = spellPossession[currentSpellNum].spell;
            GameObject cntPrefab = prefabs[cntSpell.prefabType];

            Vector3 instantiatePos = (transform.position + new Vector3(0.0f, spellHeight, 0.0f)) + transform.forward;
            Instantiate(cntPrefab, instantiatePos, transform.rotation);

            currentCooldown = cntSpell.cooldownTime;
        }
    }

    void PreviousSpell()
    {
        if (currentSpellNum <= 0)
        {
            currentSpellNum = spellPossession.Count - 1;
        }
        else
        {
            currentSpellNum--;
        }
    }

    void NextSpell()
    {
        if (currentSpellNum >= spellPossession.Count - 1)
        {
            currentSpellNum = 0;
        }
        else
        {
            currentSpellNum++;
        }
    }

    public void LoadSpells(Character characterData)
    {
        spellPossession = characterData.Spells;
    }

    private void LoadSpellPrefabs()
    {
        PrefabsHolder script = FindObjectOfType(typeof(PrefabsHolder)) as PrefabsHolder;
        prefabs = script.prefabs;
    }


    public void CastSpell() {
        if (currentCooldown <= 0.0f)
        {
            MouseController mouse = GetComponent<MouseController>();
            if (mouse) mouse.AlignRotation();

            Spell cntSpell = spellPossession[currentSpellNum].spell;

            photonView.RPC("InstantiateSpell", PhotonTargets.All, currentSpellNum);
            currentCooldown = cntSpell.cooldownTime;
        }
    }

    [RPC]
    public void InstantiateSpell(int prefabTypeNum) {
        GameObject cntPrefab = prefabs[prefabTypeNum];

        Vector3 instantiatePos = (transform.position + new Vector3(0.0f, spellHeight, 0.0f)) + transform.forward;
        Instantiate(cntPrefab, instantiatePos, transform.rotation);
    }
}
