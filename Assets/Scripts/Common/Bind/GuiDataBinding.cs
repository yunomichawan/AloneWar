using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AloneWar.Common.Bind;
using AloneWar.Common.TaskHelper;

namespace AloneWar.Common.Bind
{
    /// <summary>
    /// UIに対して値を設定、取得するクラス
    /// </summary>
    public class GuiDataBinding
    {

        /// <summary>
        /// 親オブジェクトの子に対して値を設定。(オブジェクト名とプロパティ名が一致する場合のみ。)
        /// 対応コンポーネント：Image,SpriteRenderer,Text,InputField,DropDownBox
        /// </summary>
        /// <param name="parentObj"></param>
        /// <param name="component"></param>
        public static void SetGuiComponent(Transform parentObj, object component)
        {
            AsyncTaskHelper helper = new AsyncTaskHelper(() =>
            {
                for (int i = 0; i < parentObj.childCount; i++)
                {
                    Type setType = component.GetType();

                    Transform childTransform = parentObj.GetChild(i);

                    // 再帰
                    SetGuiComponent(childTransform, component);

                    PropertyInfo propertyInfo = setType.GetProperty(childTransform.name);

                    if (propertyInfo != null)
                    {
                        SetPropertyValue(childTransform, propertyInfo.GetGetMethod().Invoke(component, null));
                    }
                }
            });
            helper.TaskRun();
        }

        /// <summary>
        /// 値を設定 set value
        /// </summary>
        /// <param name="childTransform"></param>
        /// <param name="component"></param>
        public static void SetPropertyValue(Transform childTransform, object value)
        {
            if (value == null) return;
            if (value.GetType().Equals(typeof(Sprite)))
            {
                if (childTransform.GetComponent<Image>() != null)
                {
                    if (value.GetType().Equals(typeof(Sprite)))
                    {
                        childTransform.GetComponent<Image>().sprite = (Sprite)value;
                    }
                }
                else if (value.GetType().Equals(typeof(SpriteRenderer)))
                {
                    if (value.GetType().Equals(typeof(Sprite)))
                    {
                        childTransform.GetComponent<SpriteRenderer>().sprite = (Sprite)value;
                    }
                }
            }
            else
            {
                if (childTransform.GetComponent<InputField>() != null)
                {
                    if (value.GetType().Equals(typeof(List<string>)))
                    {
                        childTransform.GetComponent<InputField>().text = string.Join(",", ((List<string>)value).ToArray());
                    }
                    else
                    {
                        childTransform.GetComponent<InputField>().text = value.ToString();
                    }
                }
                else if (childTransform.GetComponent<Toggle>() != null)
                {
                    if (value.GetType().Equals(typeof(bool)))
                    {
                        childTransform.GetComponent<Toggle>().isOn = (bool)value;
                    }
                }
                else if (childTransform.GetComponent<Text>() != null)
                {
                    Text dispText = childTransform.GetComponent<Text>();


                    if (dispText != null)
                    {
                        dispText.text = value.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// UIからオブジェクトに値を設定する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentTransform">入力値などの親要素</param>
        /// <param name="baseObject">設定したいオブジェクト</param>
        /// <returns></returns>
        public static T SetGuiComponentToObject<T>(Transform parentTransform, object baseObject)
        {
            AsyncTaskHelper helper = new AsyncTaskHelper(() =>
            {
                for (int i = 0; i < parentTransform.childCount; i++)
                {
                    Transform childTransform = parentTransform.transform.GetChild(i);

                    // 再帰
                    SetGuiComponentToObject<T>(childTransform, baseObject);

                    PropertyInfo propertyInfo = typeof(T).GetProperty(childTransform.name);
                    if (propertyInfo != null)
                    {
                        SetComponentToProperty(childTransform, propertyInfo, baseObject);
                    }
                }
            });

            helper.TaskRun();

            return (T)baseObject;
        }

        /// <summary>
        /// 入力値をプロパティに設定する
        /// </summary>
        /// <param name="childTransform">値を取得対象ゲームオブジェクトのトランスフォーム</param>
        /// <param name="propertyInfo">プロパティ</param>
        /// <param name="component">プロパティが宣言されている親クラス</param>
        private static void SetComponentToProperty(Transform childTransform, PropertyInfo propertyInfo, object component)
        {
            object value = null;

            if (childTransform.GetComponent<InputField>() != null)
            {
                InputField inputField = childTransform.GetComponent<InputField>();
                value = inputField.text;
            }
            else if (childTransform.GetComponent<Toggle>() != null)
            {
                value = childTransform.GetComponent<Toggle>().isOn;
            }

            DataBinding<object>.SetPropertyValue(propertyInfo, component, value);
        }
    }
}