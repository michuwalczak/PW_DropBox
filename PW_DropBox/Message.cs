using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW_DropBox
{
    public class Message
    {
        public enum DataType
        {
            Configuration,
            File,
            ListOfFiles
        }

        private static readonly char[] separators = { '\t', ',' };

        public DataType Command { get; set; }
        public string[] Data { get; set; }

        public string Set(DataType command, string[] data)
        {
            string[] frame = { Command.ToString(), string.Join(separators[1].ToString(), Data) };
            return string.Join(separators[0].ToString(), frame);
        }

        public Message Get(string message)
        {
            string[] messageParams = message.Split(separators[0]);

            this.Command = (DataType)Enum.Parse(typeof(DataType), messageParams[0]);
            this.Data = message.Split(separators[1]);

            return this;
        }
    }
}
