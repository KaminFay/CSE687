using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;

namespace guiApp
{

    class BufferBuilder
    {
        private FlatFunc flatFunc;

        public BufferBuilder(dllFunction func)
        {
            flatFunc = new FlatFunc
            {
                FunctionName = func.functionName,
                DllName = func.dllName,
                DllPath = func.dllPath
            };
        }

        public void SerializeAndSendBuffer(SendingSocket ss)
        {
            int maxByte = flatFunc.CalculateSize();
            
            byte[] buffer = new byte[maxByte];
            buffer = flatFunc.ToByteArray();
            ss.SendDataOverSocket(buffer);
        }

    }
}
