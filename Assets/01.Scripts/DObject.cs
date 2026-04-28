using System;
using UnityEngine;

namespace Dowon
{
    public abstract class DObject : MonoBehaviour
    {
        private GameObject		m_CacheGameObject;

        private Transform		m_CacheTransform;

        #region Property.

        public Transform CacheTransform
        {
            get
            {
                if (object.ReferenceEquals(m_CacheTransform, null))
                {
                    m_CacheTransform = transform;
                }

                return m_CacheTransform;
            }
        }

        public GameObject CacheGameObject
        {
            get
            {
                if (object.ReferenceEquals(m_CacheGameObject, null))
                {
                    m_CacheGameObject = gameObject;
                }

                return m_CacheGameObject;
            }
        }

        #endregion Property.


        /// <summary>
        /// Object 제거 시 해당 함수를 통하여 내부에서 사용중인 객체를 모두 해제하여 GC대상이 되도록 구현한다.
        /// </summary>
        public abstract void DisposeObject();

        /// <summary>
        /// 오브젝트 출력 공통 함수.
        /// </summary>
        public virtual void Show()
        {
            CacheGameObject.SetActive(true);
        }

        /// <summary>
        /// 오브젝트 숨김 공통 함수.
        /// </summary>
        public virtual void Hide()
        {
            CacheGameObject.SetActive(false);
        }
    }
}