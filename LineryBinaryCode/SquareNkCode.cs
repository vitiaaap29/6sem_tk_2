using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;

namespace LineryBinaryCode
{
    public class SquareNkCode : NkCode
    {
        public override BitNumber Generate(BitNumber message)
        {
            BitNumber result = new BitNumber();
            Size oldSize = calculateOptimalSquareSize(message.Length);

            //добавление бита чётности по строкам
            BitNumber[] table = new BitNumber[(int)oldSize.Height + 1];
            int start = 0;
            int lengthLine = (int)oldSize.Width;
            int end = lengthLine - 1;
            for (int i = 0; i < table.Length - 1; i++ )
            {
                table[i] = new BitNumber(message, start, end);
                //если чётно, то не нужна ещё одна единица
                bool parityBit = !table[i].IsParity;
                table[i].PushBack(parityBit);
                start = end + 1;
                end += lengthLine;
            }

            //по столбцам
            table[(int)oldSize.Height] = new BitNumber((int)oldSize.Width + 1);
            for (int i = 0; i < table[0].Length; i++)
            {
                int countOnes = 0;
                for (int j = 0; j < table.Length - 1; j++)
                {
                    if (table[j][i] == true)
                    {
                        countOnes++;
                    }
                }
                table[(int)oldSize.Height][i] = countOnes % 2 != 0;
            }
            //на бит в нижнем левом углу до фонаря

            result = BitNumber.Split(table);

            return result;
        }
        public override BitNumber Decode(BitNumber codeWord)
        {
            BitNumber result = new BitNumber();
            Size oldSize = calculateOptimalSquareSize(codeWord.Length);
            Size newSize = new Size(oldSize.Width - 1, oldSize.Height - 1);

            return result;
        }

        /// <summary>
        /// Оптимальные размеры прямоугольника из length элементов
        /// </summary>
        /// <param name="length">Количесвто элементов</param>
        /// <returns></returns>
        private Size calculateOptimalSquareSize(int length)
        {
            Size size = new Size(1, length);

            for (int i = 2; i < length / 2; i++)
            {
                if (length % i == 0)
                {
                    size.Width = i;
                    size.Height = length / i;
                }
            }

            return size;
        }

    }
}
