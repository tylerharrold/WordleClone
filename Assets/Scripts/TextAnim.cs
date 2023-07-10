using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    [SerializeField] float speed;
    private Transform myTransform;
    private float direction = 1.0f;

    // kill points
    private float killX;

    // Start is called before the first frame update
    void Start()
    {
        
        myTransform = GetComponent<Transform>();
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setDirection(float f)
    {
        direction = direction * f;
    }

    public void setKillX(float x)
    {
        killX = x;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.Translate(Vector3.right * speed * Time.deltaTime * direction);

        checkForKill();
    }

    private void checkForKill()
    {
        // if we are moving negative (right-to-left) we want to be LESS than killx
        // if we are moving position (left-to-right) we want to be GREATER than killx
        if(direction < 0)
        {
            if(gameObject.transform.position.x < killX)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(gameObject.transform.position.x > killX)
            {
                Destroy(gameObject);
            }
        }
    }
}
