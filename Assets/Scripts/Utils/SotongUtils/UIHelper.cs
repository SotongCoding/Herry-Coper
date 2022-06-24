using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace SotongUtils
{
    public class UIHelper
    {
        public static void ChangeFillAmount(Image target, float fillAmount)
        {
            target.fillAmount = fillAmount;
        }

        /// <summary>
        /// Change Fill Amount move slightly
        /// </summary>
        /// <param name="target">Image target</param>
        /// <param name="fillAmount">Final Fill amount</param>
        /// <param name="time">Duration of moving</param>
        public static void ChangeFillAmount(Image target, float fillAmount, float time)
        {
            MoveFill();
            async void MoveFill()
            {
                var end = Time.time + time + 0.5f;
                while (Time.time < end)
                {
                    target.fillAmount = Mathf.Lerp(1, fillAmount, time);
                    await System.Threading.Tasks.Task.Yield();
                }
            }

        }
    }
}