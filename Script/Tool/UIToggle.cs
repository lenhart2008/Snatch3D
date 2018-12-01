using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Function: UIToggle 支持Toggle选中时更换图片（其他功能待完善）
/// Date: 2017-12-7
/// </summary>
/// 
public class UIToggle : MonoBehaviour
{
    public Image TargetGraphic;
    public Sprite normalImage;
    public Vector2 normalSize;
    public Sprite pressedImage;
    public Vector2 pressedSize;
    public Text TargetText;
    public int textNormalFontSize = 20;
    public Vector2 textNormalPos;
    public int textPressedSize = 28;
    public Vector2 textPressedPos;
    private RectTransform textRt;
    public ToggleGroup TarToggleGroup;

    private Toggle m_toggle;
    private Action<bool> onToggleValueChangCallback;

    private bool m_isOn = false;
    void Awake()
    {
        m_toggle = GetToggel();
        m_toggle.targetGraphic = TargetGraphic;
        if (textRt != null)
        {
            textRt = TargetText.GetComponent<RectTransform>();
        }
        

        if (null != m_toggle)
        {
            m_toggle.onValueChanged.AddListener(OnToggleValueChange);
        }
        else
        {
           Debug.LogError("ChangeToggleImg::Awake():  m_toggle is null please check");
        }

        if (TarToggleGroup!= null)
        {
            SetToggleGroup(TarToggleGroup);
        }
    }

    Toggle GetToggel()
    {
        if (gameObject.GetComponent<Toggle>() != null)
            return gameObject.GetComponent<Toggle>();
        else
            return gameObject.AddComponent<Toggle>();
    }

    public void RegisterToggleValueChangeCallBack(Action<bool> callback)
    {
        onToggleValueChangCallback += callback;
    }

    public void OnToggleValueChange(bool isOn)
    {
        if (isOn != m_isOn)
        {
            m_isOn = isOn;

            ChangeToggleLabel(isOn);
            if (onToggleValueChangCallback != null)
            {
                onToggleValueChangCallback(isOn);
            }
        }
    }

    public void SetToggleGroup(ToggleGroup tg)
    {
        TarToggleGroup = tg;
        m_toggle.group = TarToggleGroup;
    }

    private void ChangeToggleLabel(bool isOn)
    {
        if (null != TargetGraphic)
        {
            if (isOn)
            {
                TargetGraphic.sprite = pressedImage;
                if (pressedSize != Vector2.zero)
                {
                    TargetGraphic.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pressedSize.x);
                    TargetGraphic.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pressedSize.y);
                }
                if (TargetText != null)
                {
                    TargetText.fontSize = textPressedSize;
                    if(textRt != null)
                        textRt.anchoredPosition = textPressedPos;
                }
            }
            else
            {
                TargetGraphic.sprite = normalImage;
                if (normalSize != Vector2.zero)
                {
                    TargetGraphic.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, normalSize.x);
                    TargetGraphic.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, normalSize.y);
                }
                if (TargetText != null)
                {
                    TargetText.fontSize = textNormalFontSize;
                    if(textRt != null)
                        textRt.anchoredPosition = textNormalPos;
                }
            }
        }
        else
        {
            Debug.LogError("BlueprintType::ChangeToggleImg():  m_toggle is null please check");
        }
    }

    public void SetToggle(bool bIsOn)
    {
        m_toggle.isOn = bIsOn;     
    }
}
