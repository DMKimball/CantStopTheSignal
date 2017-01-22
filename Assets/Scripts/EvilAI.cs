using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilAI : MonoBehaviour
{

    public static GameObject player;
    public static float dps = 3.0f;

    public GameObject prefab;

    public Vector3 startRadius = Vector3.forward;
    public float expansionRate = 0.0f;
    public float hackTime = 5.0f;

    private MeshRenderer meshRenderRange;
    private MeshRenderer meshRenderPulse;
    private ChangeRangeColor[] colorChanges;
    private float rad;
    private List<Collider2D> wiredTargets = new List<Collider2D>();


    void Awake()
    {
        meshRenderRange = GetComponent<MeshRenderer>();
        meshRenderPulse = GetComponentInChildren<MeshRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");
        transform.localScale = startRadius;
    }

    // Update is called once per frame
    void Update()
    {
        float scaleUp = Time.deltaTime * expansionRate;
        transform.localScale = new Vector3(transform.localScale.x + scaleUp, transform.localScale.y + scaleUp, transform.localScale.z);
        //if (player && Vector3.Distance(transform.position, player.transform.position) <= rad) GameManager.instance.LoseHealth(dps*Time.deltaTime);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, rad);
        foreach (Collider2D c in colliders)
        {
            ResolveCollision(c);
        }
    }

    public void Activate()
    {
        enabled = false;
        StartCoroutine("AssumeDirectControl");
    }

    IEnumerator AssumeDirectControl()
    {
        yield return new WaitForSecondsRealtime(hackTime);
        if (colorChanges != null && colorChanges.Length == 3)
        {
            foreach (ChangeRangeColor changer in colorChanges)
            {
                if (!changer.name.Contains("EvilAI")) changer.ChangeToNewState(DeviceState.evil);
                if (changer.transform.parent == transform.parent.parent) startRadius = changer.transform.localScale;
            }
            rad = Mathf.Max(startRadius.x, startRadius.y)/2;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, rad);
            foreach (Collider2D c in colliders)
            {
                ResolveCollision(c);
            }
        } else if(wiredTargets.Count > 0)
        {
            foreach(Collider2D coll in wiredTargets)
            {
                ResolveCollision(coll);
            }
        }
        enabled = true;
    }

    private void ResolveCollision(Collider2D collision)
    {
        Controllable target = collision.GetComponent<Controllable>();
        EvilAI infected = collision.GetComponentInChildren<EvilAI>();
        if (target && !infected && !collision.isTrigger)
        {
            GameObject newEvilAI = Instantiate(prefab, collision.transform);
            newEvilAI.transform.localPosition = Vector3.zero;
            newEvilAI.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
            EvilAI script = newEvilAI.GetComponent<EvilAI>();
            if (collision.GetComponent<DroidInteractor>())
            {
                script.expansionRate = 0;
                script.startRadius = new Vector3(0.75f, 0.75f, 1.0f);
                script.hackTime = 20.0f;
            }
            else if (collision.GetComponent<RemoteInteractorAdapter>())
            {
                Transform parent = collision.transform.parent;
                script.colorChanges = parent.GetComponentsInChildren<ChangeRangeColor>();
                script.expansionRate = 0;
                script.hackTime = 5.0f;
                if (script.colorChanges.Length == 3)
                {
                    script.meshRenderRange.enabled = false;
                    script.meshRenderPulse.enabled = false;
                }
                else
                {
                    PointJump p_jump = parent.GetComponentInChildren<PointJump>();
                    if (p_jump)
                    {
                        script.startRadius = new Vector3(0.75f, 0.75f, 1.0f);
                        foreach(Transform trans in p_jump.waypoints)
                        {
                            foreach (Collider2D coll in trans.GetComponentsInChildren<Collider2D>())
                            {
                                script.wiredTargets.Add(coll);
                            }
                        }
                    }
                }
            }
            script.Activate();
        }
    }
}
