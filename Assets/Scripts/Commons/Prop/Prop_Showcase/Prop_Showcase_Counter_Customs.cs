using UnityEngine;


public class Prop_Showcase_Counter_Customs : Prop_Showcase_Base
{
    public new virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("进入展柜");
            EOnCollisionEnter?.Invoke(other.gameObject, this.gameObject);
        }
    }

    public new virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("离开展柜");
            EOnCollisionExit?.Invoke(other.gameObject, this.gameObject);
        }
    }
}