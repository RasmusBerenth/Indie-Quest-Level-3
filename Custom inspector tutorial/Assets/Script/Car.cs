using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public string m_Make = "Toyota";
    public int m_YearBuild = 1980;
    public Color m_Color = Color.black;

    //This car has 4 tires
    public Tire[] m_Tire = new Tire[4];
}
