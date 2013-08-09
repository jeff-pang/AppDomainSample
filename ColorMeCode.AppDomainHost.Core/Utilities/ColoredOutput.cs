using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ColorMeCode.AppDomainHost.Core.Utilities
{
    public class ColoredOutput : TextWriter
    {
        int _color=1;
        static ReaderWriterLockSlim _consoleLock;
        static StreamWriter _writer;

        static ColoredOutput()
        {
            _consoleLock = new ReaderWriterLockSlim();
            _writer = new StreamWriter(Console.OpenStandardOutput());
        }
        public ColoredOutput(int color)
        {
            _color = color;            
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }

        public override void Write(char value)
        {
            Write(value.ToString());
        }
        public override void Write(string value)
        {
            Write(value, "");
        }
        public override void Write(string format, object arg0)
        {
            Write(format, arg0, "");
        }
        public override void Write(string format, object arg0, object arg1)
        {
            Write(format, arg0, arg1, "");
        }
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            Write(format, arg0, arg1, arg2, "");
        }

        public override void Write(string format, params object[] arg)
        {
            try
            {
                _consoleLock.EnterWriteLock();                                
                Console.ForegroundColor = (ConsoleColor)_color;
                _writer.Write(format,arg);
                _writer.Flush();                
                Console.ResetColor();
            }
            finally
            {
                _consoleLock.ExitWriteLock();
            }
        }

        public override void WriteLine()
        {
            WriteLine("");
        }
        public override void WriteLine(string value)
        {
            WriteLine(value, "");
        }
        public override void WriteLine(string format, object arg0)
        {
            WriteLine(format, arg0, "");
        }
        public override void WriteLine(string format, object arg0, object arg1)
        {
            WriteLine(format, arg0, arg1, "");
        }
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            WriteLine(format, arg0, arg1, arg2, "");
        }

        public override void WriteLine(string format, params object[] arg)
        {
            try
            {
                _consoleLock.EnterWriteLock();
                Console.ForegroundColor = (ConsoleColor)_color;
                _writer.WriteLine(format, arg);                
                _writer.Flush();
                Console.ResetColor();                
            }
            finally
            {
                _consoleLock.ExitWriteLock();
            }
        }
    }
}
