using JetBrains.Rider.Unity.Editor;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controls the steps in the in coaching card.
    /// </summary>
    public class StepManager : MonoBehaviour
    {
        [SerializeField] GameObject ResultScreen;
        [SerializeField] TMP_Text ResultTime;

        GameObject currentLevel;

        [Serializable]
        class Step
        {
            [SerializeField]
            public GameObject stepObject;

            [SerializeField]
            public GameObject Scene;

            [SerializeField]
            public bool HideMenu;
        }
        [SerializeField]
        List<Step> m_StepList = new List<Step>();

        int m_CurrentStepIndex = 0;

        public void Next()
        {
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
            m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
        }

        public void Prev()
        {
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
            m_CurrentStepIndex--;
            if(m_CurrentStepIndex < 0) m_CurrentStepIndex = m_StepList.Count - 1;
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
        }

        public void Play()
        {
            
            currentLevel = Instantiate(m_StepList[m_CurrentStepIndex].Scene);
            currentLevel.GetComponent<LevelObject>().OnLevelCompleted += ShowMenu;

            if (m_StepList[m_CurrentStepIndex].HideMenu)
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
        public void Show()
        {
            ShowMenu(this, 0);
        }
        void ShowMenu(object sender, float totalSeconds)
        {
            if (currentLevel) Destroy(currentLevel);
            ResultScreen.SetActive(true);
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            ResultTime.text = time.ToString("mm':'ss");
        }
    }
}
