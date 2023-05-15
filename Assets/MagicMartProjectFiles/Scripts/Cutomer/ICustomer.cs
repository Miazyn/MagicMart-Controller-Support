using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomer
{
    public float CustomerSatisfaction { get; set; }
    public void RequestItem(SO_Recipe _request);
    public float GetCustomerSatisfaction();

}
