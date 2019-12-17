using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxyPlotExample2
{
    /* 流速传感器 */
    class FlowSensor
    {
        private SerialPort m_serialPort = null;

        public FrameDecoder m_frameDecoder = new FrameDecoder();
        private Queue<TaskCompletionSource<string>> m_cmdRespTaskCompQue = new Queue<TaskCompletionSource<string>>();

        public FlowSensor()
        {
            FrameDecoder.Test();
        }

        public void Open(string portName)
        {
            m_serialPort?.Close();
            m_serialPort = new SerialPort(portName);
            m_serialPort.BaudRate = 115200;
            m_serialPort.Parity = Parity.None;
            m_serialPort.StopBits = StopBits.One;
            m_serialPort.DataBits = 8;
            m_serialPort.Handshake = Handshake.None;
            m_serialPort.RtsEnable = true;

            m_serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            m_frameDecoder.CmdRespRecved += new FrameDecoder.CmdRespRecvHandler((string cmdResp) => {
                Console.WriteLine($"CmdRespRecved: {cmdResp}");
                if (m_cmdRespTaskCompQue.Count > 0)
                {
                    /* 通知CMD Task执行结果 */
                    TaskCompletionSource<string> taskComp = null;
                    lock (m_cmdRespTaskCompQue)
                    {
                        taskComp = m_cmdRespTaskCompQue.Dequeue();
                    }
                    taskComp?.SetResult(cmdResp);
                }
            });

            m_serialPort.Open();
        }

        public void Close()
        {
            m_serialPort.Close();
            m_serialPort = null;
        }

        /* 创建CMD Task */
        private Task<string> ExcuteCmdTask(string cmd)
        {
            /* 将CMD Task记录到完成队列 */
            var cmdRespTaskComp = new TaskCompletionSource<string>();
            lock (m_cmdRespTaskCompQue)
            {
                m_cmdRespTaskCompQue.Enqueue(cmdRespTaskComp);
            }

            /* 发送CMD */
            m_serialPort.Write(cmd);

            /* 返回Task */
            var task = cmdRespTaskComp.Task;
            return task;
        }

        /* 执行命令(异步版本) */
        public async Task<string> ExcuteCmdAsync(string cmd, int timeOut)
        {
            /* 创建CMD Task */
            var cmdTask = ExcuteCmdTask(cmd);

            /* 异步等待执行完毕或超时 */
            var task = await Task.WhenAny(cmdTask, Task.Delay(timeOut));
            if (task == cmdTask)
            { // CMD Task执行完毕
                return cmdTask.Result;
            }

            /* 超时 */
            lock (m_cmdRespTaskCompQue)
            {
                /* 删除完成队列中的记录 */
                m_cmdRespTaskCompQue.Dequeue();
            }

            return string.Empty;
        }

        /* 执行命令(同步版本) */
        public string ExcuteCmd(string cmd, int timeOut)
        {
            /* 创建CMD Task */
            var cmdTask = ExcuteCmdTask(cmd);

            /* 同步等待执行完毕或超时 */
            var compTask = Task.WhenAny(cmdTask, Task.Delay(timeOut));
            var task = compTask.Result;
            if (task == cmdTask)
            { // CMD Task执行完毕
                return cmdTask.Result;
            }

            /* 超时 */
            lock (m_cmdRespTaskCompQue)
            {
                /* 删除完成队列中的记录 */
                m_cmdRespTaskCompQue.Dequeue();
            }

            return string.Empty;
        }

        private void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {
            SerialPort sp = sender as SerialPort;
            int dataLen = sp.BytesToRead;
            byte[] dataBuf = new byte[dataLen];
            sp.Read(dataBuf, 0, dataLen);
            m_frameDecoder.FrameDecode(dataBuf);
        }
    }
}
