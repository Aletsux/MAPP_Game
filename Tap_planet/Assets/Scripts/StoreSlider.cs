using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSlider : MonoBehaviour
{
    private Transform posClose;
    private Transform posOpen;

    public float speed;
    private Transform startPos;
    Vector3 nextPos;

    Vector3 temp;

    private bool openStore = false;
    private bool closeStore = true;

    private float timer = 0f;
    private float timeBeforeReset = 0.5f;
    //private bool isActive = false;
    private StoreSlider storeSlider;
    public GameObject SS;
    public GameObject canvas;
    private int nextUpdate = 2;
    private RectTransform rt;
    private float height;
    public GameObject StoreScript;
    private StoreScript storeScript;
    private int firstTime = 0;

    float xValueCan;
    float yValueCan;

    public GameObject openStoreRef;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    private void OnEnable()
    {
        storeSlider = SS.GetComponent<StoreSlider>();
        storeScript = StoreScript.GetComponent<StoreScript>();

        posOpen = canvas.transform;
        //posOpen = openStoreRef.transform;
       

        posClose = posOpen;
        startPos = posClose;
        
        nextPos = startPos.position;

        rt = (RectTransform)canvas.transform;
        height = rt.rect.height;


    }
    // Update is called once per frame
    void Update()
    {
        //sätt timer som går ut och sen false och gör så rörelse
        if (openStore)
        {
            nextPos = posOpen.position;


            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }

        if (closeStore)
        {
            //nextPos = posClose.position;
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);



            if (!StoreScript.transform.hasChanged || StoreScript.activeInHierarchy)
            {
                //float xvalue = transform.position.x;
                //float yvalue = transform.position.y;
                //yvalue -= height;
                //Vector3 temp;
                //temp = new Vector3(xvalue, yvalue, 0);

                ifStuck();

                transform.position = Vector3.MoveTowards(transform.position, temp, speed * Time.deltaTime);

                if (transform.position.y <= temp.y)
                {
                    storeScript.CloseStore();
                    //Debug.Log("Close");
                }

                //Debug.Log("OHNO");
            }

            if (transform.position.y <= nextPos.y)
            {
                storeScript.CloseStore();
                //Debug.Log("Close");
            }
        }

        
    }

    public void ifStuck()
    {
        float xvalue = transform.position.x;
        float yvalue = transform.position.y;
        yvalue -= height;
        //Vector3 temp;
        temp = new Vector3(xvalue, yvalue, 0);
    }


    public void CloseSlider()
    {
        float xvalue = transform.position.x;
        float yvalue = transform.position.y;
        yvalue -= height;
        nextPos = new Vector3(xvalue, yvalue, 0);
        //SS.transform.position = new Vector3(xvalue, yvalue, 0);
        closeStore = true;
        openStore = false;
        //Debug.Log(transform.position);

    }



    public void OpenSlider()
    {

        float positionY=canvas.transform.position.y;

        positionY -= height;

        if(firstTime == 0)
        {
            if (transform.position.y != positionY)
            {
                float xvalue = transform.position.x;
                float yvalue = transform.position.y;
                //float height = canvas.transform.height
                yvalue -= height;
                SS.transform.position = new Vector3(xvalue, yvalue, 0);
            }
            firstTime = 1;
        }


        Debug.Log(transform.position);
        openStore = true;
        closeStore = false;
    }


}
