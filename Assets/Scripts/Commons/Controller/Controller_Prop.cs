using UnityEngine;
using UnityEngine.Serialization;

public class Controller_Prop : Controller_Base
{
    [Header("Grab")]
    public Prop_Passport PropPassport;
    public Prop_Charger PropCharger;
    public Prop_Troller PropTroller;
    public Prop_Luggage PropLuggage;
    public Prop_Suitcase PropSuitcase;

    [FormerlySerializedAs("PropShowcaseT1")] [Header("Showcase")] 
    public Prop_Showcase_T1_1 propShowcaseT11;
    public Prop_Showcase_T1_2 propShowcaseT12;
    public Prop_Showcase_T1_3 propShowcaseT13;
    public Prop_Showcase_T1_4 propShowcaseT14;
    public Prop_Showcase_T1_5 propShowcaseT15;
}