using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

    #region Serialize/Deserialize
    [Serializable]
    public class Product:IDeserializationCallback
    {
        //[Serializable]
        public int ID { get; set; }
        //[Serializable]
        public string Name { get; set; }

        [NonSerialized]
        private string FCompany;

        public override string ToString()
        {
            return string.Format("ID = {0}; Name = {1}; Company = {2}", this.ID, this.Name, this.FCompany);
        }

        public void OnDeserialization(object sender)
        {
            //throw new NotImplementedException();
            // this is a callback funciton. It would be called after the deserialize finished.
            // the parameter passed in always be null. If you need deserialize FCompany in this function, you could only assign a default value for it.
            FCompany = "Company 0";
        }
    }

    public class Serializer
    {
        IFormatter formatter;

        public Serializer()
        {
            formatter = new BinaryFormatter();
        }
        public void SerializeProduct(string aFile, Product aProd)
        {
            Stream fs = File.OpenWrite(aFile);
            formatter.Serialize(fs, aProd);
            fs.Dispose();
        }
        public Product DeserializeProduct(string aFile)
        {
            Stream fs = File.OpenRead(aFile);
            return (Product)formatter.Deserialize(fs);
            fs.Dispose();
        }
    }
    #endregion
}
