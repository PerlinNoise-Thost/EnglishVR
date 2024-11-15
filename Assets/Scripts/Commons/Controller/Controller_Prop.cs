using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Controller_Prop : Controller_Base
{
    [Title("抓取的物品(第一关)")]
    [TabGroup("Grab")]
    public Prop_Passport PropPassport;
    [TabGroup("Grab")]
    public Prop_Charger PropCharger;
    [TabGroup("Grab")]
    public Prop_Troller PropTroller;
    [TabGroup("Grab")]
    public Prop_Luggage PropLuggage;
    [TabGroup("Grab")]
    public Prop_Suitcase PropSuitcase;

    [Title("第一关的展柜")]
    [TabGroup("ShowCase")]
    public Prop_Showcase_T1_1 propShowcaseT11;
    [TabGroup("ShowCase")]
    public Prop_Showcase_T1_2 propShowcaseT12;
    [TabGroup("ShowCase")]
    public Prop_Showcase_T1_3 propShowcaseT13;
    [TabGroup("ShowCase")]
    public Prop_Showcase_T1_4 propShowcaseT14;
    [TabGroup("ShowCase")]
    public Prop_Showcase_T1_5 propShowcaseT15;
    
    [Title("范围触发检测")]
    [TabGroup("Detection")]
    public Prop_Showcase_Counter PropShowcaseCounter;
    [TabGroup("Detection")]
    public Prop_Showcase_Counter_Customs PropShowcaseCounterCustoms;

}