using System.Collections;
using System.Collections.Generic;
using Table;
using UnityEngine;

public class Main : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		Debug.Log(10);
		var name = TestTable.GetKeyData(1).name;
		Debug.Log(name);
	}


}