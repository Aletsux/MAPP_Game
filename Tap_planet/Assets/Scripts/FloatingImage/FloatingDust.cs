using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDust : MonoBehaviour
{
    private int starDust;
    private int currentDust;

    public GameObject floatingStarDust;

    private Transform dustTrans;

    public GameObject dustPos;

    public GameObject dustParent;

    public GameObject GC;
    private GameController gameController;

    private float timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();
        starDust = GameController.GetStardust();
    }

    private void OnEnable()
    {
        gameController = GC.GetComponent<GameController>();
        starDust = GameController.GetStardust();
    }

    // Update is called once per frame
    void Update()
    {
        //when you buy it go down

        if (currentDust < starDust)
        {
            starDust = currentDust;
        }

            currentDust = GameController.GetStardust();
        if (currentDust > starDust)
        {
            GameObject newFlot = GameObject.Instantiate(floatingStarDust, dustPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("DustParent").transform);
            CleanDust();
            starDust = currentDust;
        }
    }

    public void DustTrans(Transform transform)
    {
        dustTrans = transform;
    }

    public void CleanDust()
    {

        int numKids = dustParent.transform.childCount;

        if (numKids > 0)
        {
            for (int i = 0; i < numKids; i++)
            {
                GameObject prefab = dustParent.transform.GetChild(i).gameObject;
                Destroy(prefab, timer);

            }
        }
    }
}
