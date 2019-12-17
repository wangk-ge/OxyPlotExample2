﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxyPlotExample2
{
    class KalmanFilter
    {
        private float fQ; // process noise covariance
        private float fR; // measurement noise covariance
        private float fP; // error in estimate
        private float fX; // estimated value
        private float fK; // kalman gain

        /*************************************************
        * Function: KALMAN_Init
        * Description: 初始化卡尔曼滤波器
        * Author: wangk
        * Returns:
        * Parameter:
        *   fQ 过程噪声协方差, Q增大, 动态响应变快, 收敛稳定性变坏
        *   fR 测量噪声协方差, R增大, 动态响应变慢, 收敛稳定性变好
        *   fP 初始的状态协方差, 表示初值与真值的不确定性, P越接近0代表越相信仪器的测量结果
        *   fInitialEstimate 初始估计值
        * History:
        *************************************************/
        public KalmanFilter(float fQ, float fR, float fP, float fInitialEstimate)
        {
            this.fQ = fQ;
            this.fR = fR;
            this.fP = fP;
            fX = fInitialEstimate;
            fK = 0.0f;
        }

        /*************************************************
        * Function: KALMAN_Input
        * Description: 输入测量值到卡尔曼滤波器
        * Author: wangk
        * Returns: 返回滤波结果(估计值)
        * Parameter:
        *   fMeasuredValue 测量值
        * History:
        *************************************************/
        public float Input(float fMeasuredValue)
        {
            fP = fP + fQ;
            fK = fP / (fP + fR);
            fX = fX + fK * (fMeasuredValue - fX);
            fP = (1.0f - fK) * fP;

            return fX;
        }
    }
}
