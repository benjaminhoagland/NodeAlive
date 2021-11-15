using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

public class DisplayRouteTime : MonoBehaviour
{
    [ShowInInspector][ReadOnly] public float? RouteTime = 0f;
}
