using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineryBinaryCode
{
    public class TriangularNkCode : NkCode
    {
        public class ImpossibleBuildTriangularException : Exception
        {
            public ImpossibleBuildTriangularException(string message) 
                : base(message)
            {

            }
        }

        public override BitNumber Generate(BitNumber message)
        {
            BitNumber[] triangle = null;
            int diagonalSize;
            try
            {
                diagonalSize = getLengthAddedDiagonal(message.Length) - 1;
                triangle = new BitNumber[diagonalSize + 1]; //+1 на проверку первого столба
            }
            catch (ImpossibleBuildTriangularException)
            {
                throw new ImpossibleBuildTriangularException("Incorrect size message for this method!");
            }

            //Генерация треугольника
            for (int i = 0, start = 0, lengthLine = diagonalSize; i < diagonalSize; lengthLine--, i++)
            {
                triangle[i] = new BitNumber(message, start, start + lengthLine - 1);
                start += lengthLine;
            }

            //Установка проверочного в каждой строке
            for (int i = 0; i < diagonalSize; i++)
            {
                int countOnesByRow = 0;
                for (int j = 0; j > i; j++)
                {
                    if (triangle[j][triangle[i].Length] == true)
                    {
                        countOnesByRow++;
                    }
                }
                bool ckeckedBit = triangle[i].IsParity == (countOnesByRow % 2 == 0);
                triangle[i].PushBack(ckeckedBit);
            }

            //проверка первого столбца
            int onesByFirstRow = 0;
            for (int i = 0; i < triangle.Length - 1; i++)
            {
                if (triangle[i][0] == true)
                {
                    onesByFirstRow++;
                }
            }
            triangle[triangle.Length - 1][0] = (onesByFirstRow % 2 == 0);

            return BitNumber.Split(triangle);
        }

        public override BitNumber Decode(BitNumber codeWord)
        {
            BitNumber result = null;
            if (isPossibleBuildTriangular(codeWord.Length))
            {
                int diagonalSize = getLengthDiagonal(codeWord.Length);
                BitNumber[] triangle = messageToTriangle(codeWord);
                //Создаём исходную матрицу (если не было ошибки)
                BitNumber[] sourceTriange = new BitNumber[triangle.Length - 1];
                Array.Copy(triangle, sourceTriange, sourceTriange.Length);
                for (int i = 0; i < sourceTriange.Length; i++)
                {
                    sourceTriange[i].Length--;
                }

                BitNumber maybeCodeWord = Generate(BitNumber.Split(sourceTriange));
                if (codeWord.Equals(maybeCodeWord))
                {
                    result = BitNumber.Split(sourceTriange);
                }
                else //возникла ошибка
                {

                }
            }
            else
            {
                throw new ImpossibleBuildTriangularException("Impossible build triangular!");
            }

            return result;
        }

        public bool isPossibleBuildTriangular(int sizeMessage)
        {
            bool result = false;
            for (int subtrahend = 1; sizeMessage > 0; subtrahend++)
            {
                sizeMessage -= subtrahend;
                if (sizeMessage == 0)
                {
                    result = true;
                }
            }

            return result;
        }

        private BitNumber[] messageToTriangle(BitNumber message)
        {
            BitNumber[] triangle = null;
            int diagonalSize;
            try
            {
                diagonalSize = getLengthAddedDiagonal(message.Length) - 1;
                triangle = new BitNumber[diagonalSize];
            }
            catch (ImpossibleBuildTriangularException)
            {
                throw new ImpossibleBuildTriangularException("Incorrect size message for this method!");
            }

            //Генерация треугольника
            for (int i = 0, start = 0, lengthLine = diagonalSize; i < diagonalSize; lengthLine--, i++)
            {
                triangle[i] = new BitNumber(message, start, start + lengthLine - 1);
                start += lengthLine;
            }

            return triangle;
        }

        /// <summary>
        /// Длина даигонали дополненного треугольника
        /// </summary>
        /// <param name="startSize"></param>
        /// <returns></returns>
        private int getLengthAddedDiagonal(int startSize)
        {
            int result = 1;
            for (; startSize > 0; result++)
            {
                startSize -= result;
            }

            if (startSize != 0)
            {
                throw new ImpossibleBuildTriangularException("getSizeAfterAddDiagonalToTriangle");
            }

            return result;
        }

        /// <summary>
        /// Просто длинна диагонали
        /// </summary>
        /// <param name="lengthTriangle"></param>
        /// <returns></returns>
        private int getLengthDiagonal(int lengthTriangle)
        {
            int result = 0;

            try
            {
                result = getLengthAddedDiagonal(lengthTriangle) - 1;
            }
            catch (ImpossibleBuildTriangularException)
            {
                throw new ImpossibleBuildTriangularException("getLengthDiagonal");
            }
            return result;
        }
    }
}
