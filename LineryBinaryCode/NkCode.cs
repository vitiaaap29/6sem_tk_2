using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineryBinaryCode
{
    public abstract class NkCode
    {
        public class DecodeErorr : Exception
        {
            private int countErrorsInWord;

            public int CountErrorsInWord
            {
                get { return countErrorsInWord; }
            }
            public DecodeErorr(string message, int countErrors)
                : base(message)
            {
                this.countErrorsInWord = countErrors;
            }
        }

        public NkCode()
        {

        }

        protected int getCountOnes(BitNumber vector)
        {
            int result = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] == true)
                {
                    result++;
                }
            }
            return result;
        }

        protected int getCountOnes(byte[] vector)
        {
            int result = 0;
            foreach (byte b in vector)
            {
                if (b == 1)
                {
                    result++;
                }
            }

            return result;
        }

        protected bool checkOnParity(BitNumber vector)
        {
            bool result = false;
            if (getCountOnes(vector) % 2 == 0)
            {
                result = true;
            }
            return result;
        }

        protected bool checkOnParity(byte[] vector)
        {
            bool result = false;
            if (getCountOnes(vector) % 2 == 0)
            {
                result = true;
            }
            return result;
        }

        public abstract BitNumber Generate(BitNumber message);
        public abstract BitNumber Decode(BitNumber codeWord);
    }
}
