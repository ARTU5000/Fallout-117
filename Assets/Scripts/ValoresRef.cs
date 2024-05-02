using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public class ValoresRef : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tuclase primera = new Tuclase(7);
        Tuclase segunda = primera;
        segunda.value = 5;
        Debug.Log("Clases: " + primera.value);

        Tustruct primero = new Tustruct(7);
        Tustruct segundo = primero;
        segunda.value = 5;
        Debug.Log("struct: " + primero.value);

        NativeArray<Tustruct> testNativeArray = new NativeArray<Tustruct>(new Tustruct[]
        {
            new Tustruct(1),
            new Tustruct(2),
            new Tustruct(3),
        }, Allocator.TempJob);
        PruebaJob testjob = new PruebaJob { testNativeArray = testNativeArray };
        testjob.Run();

        for (int i = 0;i < testNativeArray.Length; i++)
        {
            Debug.Log(testNativeArray[i].value);
        }
        testNativeArray.Dispose();
    }

    public class Tuclase
    {
        public int value;
        public Tuclase(int value) 
        { 
            this.value = value;
        }
    }

    public struct Tustruct
    {
        public int value;
        public Tustruct(int value)
        {
            this.value = value;
        }
    }

    public struct PruebaJob: IJob
    {
        public NativeArray<Tustruct> testNativeArray;

        public void Execute()
        {
            for (int i = 0; i < testNativeArray.Length; i++)
            {
                Tustruct mistruct = testNativeArray[i];
                mistruct.value++;
                testNativeArray[i] = mistruct;
            }
        }
    }
}
