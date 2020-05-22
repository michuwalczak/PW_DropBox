using System;

namespace Library
{
    public static class FileOperation
    {
        public static int ParseFileName(string fileName)
        {
            char[] separator = { '_' };
            return Convert.ToInt32(fileName.Split(separator)[1]);
        }
    }
}
