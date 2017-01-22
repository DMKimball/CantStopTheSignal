using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilAI : MonoBehaviour
{

    public static GameObject player;

    public GameObject prefab;

    public Vector3 startRadius = Vector3.forward;
    public float expansionRate = 0.0f;
    public float hackTime = 5.0f;

    private MeshRenderer meshRender;
    private bool useOwnRender = true;
    private ChangeRangeColor[] colorChanges;

    // Use this for initialization
    void Start()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");
        meshRender = GetComponent<MeshRenderer>();
        meshRender.enabled = useOwnRender;
        transform.localScale = startRadius;
    }

    // Update is called once per frame
    void Update()
    {
        float scaleUp = Time.deltaTime * expansionRate;
        transform.localScale = new Vector3(transform.localScale.x + scaleUp, transform.localScale.y + scaleUp, transform.localScale.z);
    }

    public void Activate()
    {
        enabled = false;
        StartCoroutine("AssumeDirectControl");
    }

    IEnumerator AssumeDirectControl()
    {
        yield return new WaitForSecondsRealtime(hackTime);
        if (colorChanges.Length > 0)
        {
            foreach (ChangeRangeColor changer in colorChanges)
            {
                if (!changer.name.Contains("EvilAI")) changer.ChangeToNewState(DeviceState.evil);
                if (changer.transform.parent == transform.parent.parent) startRadius = changer.transform.localScale;
            }
        }
        enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Controllable target = collision.GetComponent<Controllable>();
        EvilAI infected = collision.GetComponentInChildren<EvilAI>();
        if(target && !infected && !collision.isTrigger)
        {
            GameObject newEvilAI = Instantiate(prefab, collision.transform);
            newEvilAI.transform.localPosition = Vector3.zero;
            EvilAI script = newEvilAI.GetComponent<EvilAI>();
            if(collision.GetComponent<DroidInteractor>())
            {
                script.expansionRate = 0;
                script.startRadius = new Vector3(0.75f, 0.75f, 1.0f);
                script.hackTime = 20.0f;
            } else if(collision.GetComponent<RemoteInteractorAdapter>())
            {
                Transform parent = collision.transform.parent;
                script.colorChanges = parent.GetComponentsInChildren<ChangeRangeColor>();
                script.expansionRate = 0;
                script.hackTime = 5.0f;
                if (script.colorChanges.Length > 0)
                {
                    script.useOwnRender = false;
                }
                else
                {
                    PointJump p_jump = parent.GetComponentInChildren<PointJump>();
                    if(p_jump)
                    {
                        script.startRadius = new Vector3(0.75f, 0.75f, 1.0f);
                    }
                }
            }
            script.Activate();
        }
    }
}
