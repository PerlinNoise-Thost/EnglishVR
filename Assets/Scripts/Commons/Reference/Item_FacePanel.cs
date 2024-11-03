using System.Collections;
using UnityEngine;

public class Item_FacePanel : MonoBehaviour,IInitialization
{
    private UI_FacePanel_1 UI_FacePanel_1;
    private UI_FacePanel_2 UI_FacePanel_2;
    private UI_FacePanel_3 UI_FacePanel_3;
    private int nowFormIndex = 1;   //当前的形态
    private bool isFollowCamera;     //是否跟随相机
    private bool isFaceCamera;       //是否面向相机
    
    public Camera mainCamera;
    public Animation animation;

    public string DataSetSequence => GetType().Name;
    public IEnumerator Data_Set()
    {
        UI_FacePanel_1 = CenterManager.Instance._UI.UI_FacePanel_1;
        UI_FacePanel_2 = CenterManager.Instance._UI.UI_FacePanel_2;
        UI_FacePanel_3 = CenterManager.Instance._UI.UI_FacePanel_3;
        StartCoroutine(FaceAndFollowCamera());
        
        yield return null;
    }

    /// <summary>
    /// 切换形态
    /// </summary>
    /// <param name="target">目标形态</param>
    /// <returns></returns>
    public IEnumerator SwitchForm(int target)
    {
        yield return StartCoroutine(Deformation_CloseUI(nowFormIndex));
        yield return StartCoroutine(Deformation_PlayAnimation(target));
        yield return StartCoroutine(Deformation_OpenUI(target));
        nowFormIndex = target;
    }

    /// <summary>
    /// 打开UI
    /// </summary>
    /// <param name="target">目标UI</param>
    /// <returns></returns>
    private IEnumerator Deformation_OpenUI(int target)
    {
        if (target == 1)
        {
            yield return StartCoroutine(UI_FacePanel_1.OpenNowUI());
        }
        else if (target == 2)
        {
            yield return StartCoroutine(UI_FacePanel_2.OpenNowUI());
        }
        else if (target == 3)
        {
            yield return StartCoroutine(UI_FacePanel_3.OpenNowUI());
        }
    }
    
    /// <summary>
    /// 控制UI关闭
    /// </summary>
    /// <param name="target">目标UI</param>
    /// <returns></returns>
    private IEnumerator Deformation_CloseUI(int target)
    {
        if (target == 1)
        {
            if (nowFormIndex == 2)
            {
                yield return StartCoroutine(UI_FacePanel_2.ExitNowUI());
            }
            else
            {
                yield return StartCoroutine(UI_FacePanel_3.ExitNowUI());
            }
        }
        else if(target==2)
        {
            if (nowFormIndex == 1)
            {
                yield return StartCoroutine(UI_FacePanel_1.ExitNowUI());
            }
            else
            {
                yield return StartCoroutine(UI_FacePanel_3.ExitNowUI());
            }
        }
        else
        {
            if (nowFormIndex == 1)
            {
                yield return StartCoroutine(UI_FacePanel_1.ExitNowUI());
            }
            else
            {
                yield return StartCoroutine(UI_FacePanel_3.ExitNowUI());
            }
        }
    }
    
    /// <summary>
    /// 播放变形动画
    /// </summary>
    /// <param name="target">目标状态</param>
    /// <returns></returns>
    private IEnumerator Deformation_PlayAnimation(int target)
    {
        if (target == 1)
        {
            if (nowFormIndex == 2)
            {
                animation.Play("2_to_1");
            }
            else
            {
                animation.Play("3_to_1");
            }
        }
        else if(target==2)
        {
            if (nowFormIndex == 1)
            {
                animation.Play("1_to_2");
            }
            else
            {
                animation.Play("3_to_2");
            }
        }
        else
        {
            if (nowFormIndex == 1)
            {
                animation.Play("1_to_3");
            }
            else
            {
                animation.Play("2_to_3");
            }
        }

        while (animation.isPlaying)
        {
            yield return null;
        }
    }

    /// <summary>
    /// 设置跟随、面向状态
    /// </summary>
    /// <param name="follow">跟随</param>
    /// <param name="face">面向</param>
    /// <returns></returns>
    public IEnumerator SetFollowAndFace(bool follow,bool face)
    {
        isFollowCamera = follow;
        isFaceCamera = face;
        
        yield break;
    }

    /// <summary>
    /// 面板 2 3形态唤出
    /// </summary>
    /// <returns></returns>
    public IEnumerator CameraOut()
    {
        Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * 3;
        this.transform.position = targetPosition;
        
        yield break;
    }
    
    /// <summary>
    /// 面向、跟随相机
    /// </summary>
    /// <returns></returns>
    private IEnumerator FaceAndFollowCamera()
    {
        while (true)
        {
            if (isFaceCamera)
            {
                this.gameObject.transform.LookAt(mainCamera.transform.position);
            }

            if (isFollowCamera)
            {
                // 计算物体的新位置
                Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward;
                // 更新物体的位置
                transform.position = targetPosition;
                // 保持物体相对于摄像机的旋转
                transform.rotation = mainCamera.transform.rotation;
            }

            yield return null;
        }
    }
}