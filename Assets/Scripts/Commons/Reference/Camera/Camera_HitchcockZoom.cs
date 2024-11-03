using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera_HitchcockZoom : MonoBehaviour
{
    //初始化视锥高度
    private float initHeightAtDist;
    //相机
    [Header("需要移动的相机")]
    public Camera cam;
    //目标位置
    [Header("目标位置")]
    public Transform target; 
    //移动速度
    [Header("移动速度")]
    public float zoomSpeed = 2f;
    //停止区域(0.1f一般就进入了)
    [Header("停止区域")]
    public float stopArea = 0.1f;

    /// <summary>
    /// 开启希区柯克变焦
    /// </summary>
    /// <returns></returns>
    public IEnumerator HitChcokZoom()
    {
        cam.transform.LookAt(target);
        
        var distance = Vector3.Distance(cam.transform.position, target.position);
        initHeightAtDist = FrustumHeightAtDistance(cam, distance);
        
        //目标位置
        Vector3 targetPosition = target.position; 

        while (true)
        {
            // 根据相机与目标物的距离，计算调整相机的FOV值
            var currDistance = Vector3.Distance(cam.transform.position, target.position);
            cam.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);

            // 使用Lerp进行插值
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * zoomSpeed);

            // 检查是否到达目标位置
            if (Vector3.Distance(cam.transform.position, targetPosition) < 1f)
            {
                Debug.Log("希区柯克变焦结束");
                yield break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// 计算初始视锥高度
    /// </summary>
    /// <param name="camera">相机</param>
    /// <param name="distance">长度</param>
    /// <returns></returns>
    private float FrustumHeightAtDistance(Camera camera, float distance)
    {
        return 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    /// <summary>
    /// 计算视锥高度一定下，移动相机位置时，FOV的对应调整值
    /// </summary>
    /// <param name="height">视锥高度</param>
    /// <param name="distance">距离</param>
    /// <returns></returns>
    private float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// 测试用
    /// </summary>
    public void StartHitchcockZoom()
    {
        StartCoroutine(HitChcokZoom());
    }
}