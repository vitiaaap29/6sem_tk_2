using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineryBinaryCode
{
    public class RepetitionNkCode : NkCode
    {

        public override BitNumber Generate(BitNumber message)
        {
            BitNumber result = new BitNumber(message.Length * 2);
            for (int i = 0; i < message.Length; i++)
            {
                result[i * 2]     = message[i];
                result[i * 2 + 1] = message[i];
            }
            return result;
        }

        public override BitNumber Decode(BitNumber codeWord)
        {
            BitNumber result = new BitNumber(codeWord.Length / 2);
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = codeWord[i * 2];
            }

            return result;
        }

    }
}
