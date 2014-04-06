using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LineryBinaryCode
{
    //Велосипедный класс, но мне до фонаря у аптеки ночью при тусклом свете четверть века спустя.
    public class BitNumber
    {
        private BitArray source;

        public BitNumber()
        {
            source = new BitArray(1);
        }

        public BitNumber(int size)
        {
            source = new BitArray(size);
        }

        public BitNumber(BitNumber bitNumber)
        {
            source = new BitArray(bitNumber.source);
        }

        public BitNumber(BitNumber b, int start, int end)
        {
            if (start > b.Length - 1 || end > b.Length - 1 )
            {
                throw new IndexOutOfRangeException("start or end out of range in Bitnumber Constructor");
            }

            if (start > end)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            source = new BitArray(end - start + 1);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = b[start + i];
            }
        }

        /// <summary>
        /// Create BitNumber on fundament number
        /// </summary>
        /// <param name="number">Number, which lead to a binary number.</param>
        /// <param name="size">Count bits.</param>
        public BitNumber(int number, int size)
        {
            source = new BitArray(size);
            for (int i = size, j = 0; i >= 0; i--, j++)
            {
                int mask = 1 << i;
                source[j] = (number & mask) > 0;
            }
        }

        public BitNumber(String onlyOneAndZeroStr)
        {
            try
            {
                source = new BitArray(onlyOneAndZeroStr.Length);
                for (int i = 0; i < onlyOneAndZeroStr.Length; i++)
                {
                    source[i] = Int32.Parse(onlyOneAndZeroStr[i] + "") > 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BitNumber(BitArray bitArray)
        {
            source = new BitArray(bitArray);
        }

        public BitNumber(byte[] arrayByte)
        {
            source = new BitArray(arrayByte.Length);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = (arrayByte[i] > 0);
            }
        }

        public bool this[int i]
        {
            get
            {
                if (i < source.Length)
                {
                    return source.Get(i);
                }
                else
                {
                    throw new IndexOutOfRangeException("i = " + i + " > source.Length = " + source.Length);
                }
            }
            set
            {
                if (i < source.Length)
                {
                    source.Set(i, value);
                }
                else
                {
                    throw new IndexOutOfRangeException("i = " + i + " > source.Length = " + source.Length);
                }
            }
        }

        public static BitNumber operator^(BitNumber n1, BitNumber n2)
        {
            BitArray result = new BitArray(Math.Max(n1.source.Count, n2.source.Count));
            for (int i = 0; i < Math.Min(n1.source.Count, n2.source.Count); i++)
            {
                result[i] = n1.source[i] ^ n2.source[i];
            }
            return new BitNumber(result);
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is BitNumber)
            {
                BitNumber b = (BitNumber)obj;
                if (b.source == null & source == null)
                {
                    result = true;
                }
                else if (b.source.Length == source.Length)
                {
                    result = true;
                    for (int i = 0; i < source.Length; i++)
                    {
                        if (b.source[i] != source[i])
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (bool b in source)
            {
                result.Append(b? "1": "0");
            }
            return result.ToString();
        }

        public int Length
        {
            get
            {
                return source.Length;
            }
            set
            {
                source.Length = value;
            }
        }

        public byte[] ToArrayBytes()
        {
            byte[] result = new byte[source.Length];
            int i = 0;
            foreach(bool b in source)
            {
                result[i++] = (byte)((b == true) ? 1 : 0);
            }

            return result;
        }

        /// <summary>
        /// Проверка на чётность
        /// </summary>
        public bool IsParity
        {
            get
            {
                int countOnes = 0;
                for (int i = 0; i < Length; i++)
                {
                    if (this[i] == true)
                    {
                        countOnes++;
                    }
                }
                return countOnes % 2 == 0;
            }
        }

        /// <summary>
        /// Добавить бит в конец, увеличивает размер.
        /// </summary>
        /// <param name="bit">Добавляемый бит</param>
        public void PushBack(bool bit)
        {
            source.Length++;
            source[source.Length - 1] = bit;
        }

        /// <summary>
        /// Объединение массива Bitnumber в один Bitnumber
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static BitNumber Split(BitNumber[] array)
        {
            int length = 0;
            foreach (BitNumber b in array)
            {
                length += b.Length;
            }

            BitNumber result = new BitNumber(length);

            int indexInResult = 0;
            foreach (BitNumber b in array)
            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[indexInResult++] = b[i];
                }
            }

            return result;
        }
    }
}
