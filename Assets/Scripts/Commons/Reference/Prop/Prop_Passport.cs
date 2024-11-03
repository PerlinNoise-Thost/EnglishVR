using UnityEngine;

public class Prop_Passport : Prop_Base
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.name == "TODO:这里写展柜的名称")
            {
                Debug.Log("展柜进入正确");
                
                return;
            }
            
            Debug.Log("展柜进入错误");
        }
    }

    public override string PropID { get; set; }
}