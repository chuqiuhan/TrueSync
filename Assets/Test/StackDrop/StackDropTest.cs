using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;

public class StackDropTest : TrueSyncBehaviour
{
    [SerializeField]
    private int xSize = 5;
    [SerializeField]
    private int ySize = 5;
    [SerializeField]
    private int zSize = 5;

    [SerializeField]
    private FP xGap = 0.1;
    [SerializeField]
    private FP yGap = 0.1;
    [SerializeField]
    private FP zGap = 0.1;

    private FP boxWidth = 1;

    [SerializeField]
    private GameObject obj;

    private List<GameObject> objStack = new List<GameObject>();

    private int frame = 0;

    public override void OnSyncedStart()
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                for (int k = 0; k < zSize; k++)
                {
                    GameObject tObj = TrueSyncManager.SyncedInstantiate(obj, new TSVector(
                        (i - 1) * (boxWidth + xGap),
                        (j - 1) * (boxWidth + yGap), 
                        (k - 1) * (boxWidth + zGap)), TSQuaternion.identity);
                    TSMaterial tMaterial = tObj.AddComponent<TSMaterial>();
                    tMaterial.restitution = 0;
                    tMaterial.friction = 1;
                    objStack.Add(tObj);
                }
            }
        }
    }

    public override void OnSyncedUpdate()
    {
        frame++;
        int objSize = objStack.Count;
        FP positionCheckSum = FP.Zero;
        FP rotationCheckSum = FP.Zero;
        for (int i=0; i<objSize; i++)
        {
            GameObject obj = objStack[i];
            TSTransform tTransform = obj.GetComponent<TSTransform>();
            positionCheckSum += (tTransform.position.x + tTransform.position.y + tTransform.position.z);
            rotationCheckSum += (tTransform.rotation.x + tTransform.rotation.y + tTransform.rotation.z + tTransform.rotation.w);
        }

        //Debug.Log("frame :" + frame + " positionCheckSum: " + positionCheckSum.ToString("F8") + " rotationCheckSum: " + rotationCheckSum.ToString("F8"));
    }
}
