// jave.lin 2019.08.08
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private int p0Hash;
    private int upDirHash;

    private Material mat;

    public TestMoveVertices verticeObj;
    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<SkinnedMeshRenderer>().material;

        p0Hash = Shader.PropertyToID("p0");
        upDirHash = Shader.PropertyToID("upDir");
    }

    

    // Update is called once per frame
    void FixedUpdate() // 这里使用FixedUpdate不然一直抖动
    {
        var p0 = verticeObj.p0T.transform.position;
        var p1 = verticeObj.p1T.transform.position;
        var p2 = verticeObj.p2T.transform.position;

        var upDir = Vector3.Cross(p1 - p0, p2 - p0).normalized;

        mat.SetVector(p0Hash, p0);          // 传入平面的某个点
        mat.SetVector(upDirHash, upDir);    // 传入平面法线
    }
}
