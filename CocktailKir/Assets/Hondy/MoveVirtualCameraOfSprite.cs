using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveVirtualCameraOfSprite : IEventAction
{

    ///
    /// <summary>   EventManagerが管理しているGameObject.    </summary>
    ///
    /// <remarks>   Hondy, 2016/10/18.  </remarks>
    ///

    GameObject m_manageCanvasObject;
    
    Vector3 m_nowPosition;

    Vector3 m_nextPosition;

    // 補間方法
    // とりあえず線形

    public override void Action()
    {
        // 全体のimageとかの移動量算出
        Vector3 _movementValue = m_nextPosition - m_nowPosition;

        for (int i = 0; i< m_manageCanvasObject.transform.childCount;i++)
        {
            // 将来的にはカメラからの距離で見た目的な移動量が変わるかも
            Image _image = m_manageCanvasObject.transform.GetChild(i).GetComponent<Image>();
            _image.rectTransform.position += _movementValue;
        }
    }
}
