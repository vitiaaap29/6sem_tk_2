using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineryBinaryCode
{
    public class AdjacentClass
    {
        public BitNumber[][] GenerateTableAdjacentClasses(int n)
        {
            BitNumber[][] table = new BitNumber[n][];
            table[0] = new BitNumber[n];
            for (int i = 0; i < table[0].Length; i++)
            {
                table[0][i] = new BitNumber(i, n);
            }

            for (int j = 1; j < n; j++)
            {
                table[j] = new BitNumber[n];
                table[j][0] = new BitNumber(j, n);
                for (int i = 0; i < table[j].Length; i++)
                {
                    table[j][i] = (new BitNumber(i, n) ^ table[j][0]);
                }
            }
            return table;
        }

        public BitNumber Decode(BitNumber message, BitNumber[][] tableAdjacentClasses)
        {
            for (int i = 0; i < tableAdjacentClasses.GetLength(0); i++)
            {
                for (int j = 0; j < tableAdjacentClasses.GetLength(i); j++)
                {
                    if (message.Equals(tableAdjacentClasses[i][j]))
                    {
                        return message ^ tableAdjacentClasses[i][0];
                    }
                }
            }
            return null;
        }
    }
}
