using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApps
{
    public class StreamTest
    {
        int BufferSize = 2;

        private string _SrcFile, _TgtFile;
        private Stream _Source, _Target;
        public StreamTest(string aSrcFile, string aTgtFile)
        {
            _SrcFile = aSrcFile;
            _TgtFile = aTgtFile;

            _Source = new FileStream(aSrcFile, FileMode.Open, FileAccess.Read);
            _Target = new FileStream(aTgtFile, FileMode.OpenOrCreate, FileAccess.Write);
        }

        public void Run()
        {
            byte[] buffer = new byte[BufferSize]; int bytesRead;

            do
            {
                bytesRead = _Source.Read(buffer, 0, BufferSize);
                _Target.Write(buffer, 0, bytesRead);
            }
            while (bytesRead > 0);

            _Source.Dispose();
            _Target.Dispose();
        }
    }
}
