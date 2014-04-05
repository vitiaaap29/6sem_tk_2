using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineryBinaryCode
{
    public class ParityCheckNkCode : NkCode
    {
        public override BitNumber Generate(BitNumber message)
        {
            BitNumber result = new BitNumber(message.Length + 1);
            if (!checkOnParity(message))
            {
                result[message.Length] = true;
            }

            for (int i = 0; i < message.Length; i++)
            {
                result[i] = message[i];
            }

            return result;
        }

        public override BitNumber Decode(BitNumber codeWord)
        {
            BitNumber result = new BitNumber(codeWord.Length - 1);
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = codeWord[i];
            }
            return result;
        }

    }
}
