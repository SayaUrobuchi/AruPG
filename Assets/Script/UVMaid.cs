using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 讓這份 Script 在非 Play Mode 也能執行
// 掛著這個 Component 的 GameObject 一旦發生變動，就會執行 Update
[ExecuteInEditMode]
// 這限制 GameObject 上必須有 RawImage 才能掛 UVMaid
[RequireComponent(typeof(RawImage))]
public class UVMaid : MonoBehaviour
{
	// 對齊基準
    public enum FitType
    {
		// 改變 H 配合 W
        RefToW, 
		// 改變 W 配合 H
        RefToH, 
		// 讓圖維持比例縮放至填滿範圍不留空白的最小倍率，可以超出去
        Fill, 
		// 讓圖維持比例縮放至不超出去的最大倍率
        NotFill, 
    }
    
    public FitType RefType = FitType.RefToH;

    private RawImage target;

    public Rect UV
    {
        get
        {
            Init();
            return target.uvRect;
        }

        set
        {
            Init();
            target.uvRect = value;
        }
    }

    public Texture Texture
    {
        get
        {
            Init();
            return target.texture;
        }

        set
        {
            Init();
            target.texture = value;
        }
    }

	// Use this for initialization
	void Start ()
    {
        Init();
        if (!Application.isPlaying)
        {
            AdjustUV();
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (!Application.isPlaying)
        {
            AdjustUV();
        }
    }
#endif

    private void Init()
    {
        if (target == null)
        {
            target = GetComponent<RawImage>();
        }
    }

	// [ContextMenu(...)] 可以在 Component 的右鍵選單上追加項目，選了就會執行這個 method
    [ContextMenu("AdjustUV")]
    public void AdjustUV()
    {
        Init();

        Texture tex = target.texture;
        if (tex != null)
        {
            Rect size = target.rectTransform.rect;
            Rect uv = target.uvRect;

			// 計算出參照的基準是寬還是高
            bool refX = (RefType == FitType.RefToW);
            switch (RefType)
            {
            case FitType.Fill:
                if (tex.width / (float)tex.height < size.x / size.y)
                {
                    refX = true;
                    uv.width = 1;
                }
                else
                {
                    uv.height = 1;
                }
                break;
            case FitType.NotFill:
                if (tex.width / (float)tex.height > size.x / size.y)
                {
                    refX = true;
                    uv.width = 1;
                }
                else
                {
                    uv.height = 1;
                }
                break;
            }

			// 以 橫軸 為基準時：用 顯示的寬 算出 顯示的高 再去推算 UV 所佔的縮放比例
            if (refX)
            {
                float y = size.width * tex.height / tex.width;
                float h = size.height / y;
                uv.height = h * uv.width;
            }
            else
            {
                float x = size.height * tex.width / tex.height;
                float w = size.width / x;
                uv.width = w * uv.height;
            }
            target.uvRect = uv;
        }
    }
}
