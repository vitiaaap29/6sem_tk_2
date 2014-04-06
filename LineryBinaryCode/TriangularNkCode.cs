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
            byte[] result = null;
            byte[] messageInBytes = message.ToArrayBytes();
            int diagonalSize;
            try
            {
                diagonalSize = getLengthAddedDiagonal(message.Length);
                result = new byte[ diagonalSize + message.Length];
            }
            catch (ImpossibleBuildTriangularException)
            {
                throw new ImpossibleBuildTriangularException("Incorrect size message for this method!");
            }

            byte[][] triangleLine = new byte[diagonalSize][];
            for (int lineSize = diagonalSize, i = 0, startLine = 0; lineSize > 0 && startLine < message.Length; lineSize--, i++)
            {
                triangleLine[i] = new byte[lineSize];
                Array.Copy(messageInBytes, startLine, triangleLine[i], 0, lineSize - 1);
                startLine += (lineSize - 1);
                triangleLine[i][lineSize - 1] = (!checkOnParity(triangleLine[i])) ? (byte)1 : (byte)0;
            }

            //check by first column
            int countOneByFirstColumn = 0;
            for (int i = 0; i < diagonalSize - 1; i++)
            {
                if (triangleLine[i][0] > 0)
                {
                    countOneByFirstColumn++;
                }
            }
            triangleLine[diagonalSize - 1] = new byte[1];
            triangleLine[diagonalSize - 1][0] = (byte)(countOneByFirstColumn % 2);

            //Split in triangle in result
            for (int i = 0, startInResult = 0; i < triangleLine.Length; i++)
            {
                Array.Copy(triangleLine[i], 0, result, startInResult, triangleLine[i].Length);
                startInResult += triangleLine[i].Length;
            }
            
            return new BitNumber(result);
        }

        public override BitNumber Decode(BitNumber codeWord)
        {
            byte[] result = null;
            byte[] codeWordBytes = codeWord.ToArrayBytes();
            if (isPossibleBuildTriangular(codeWord.Length))
            {
                int diagonalSize = getLengthDiagonal(codeWord.Length);
                result = new byte[codeWord.Length - diagonalSize];
                int countErrors = 0;
                for (int startTriangleLine = 0, lineSize = diagonalSize, i = 0; lineSize > 0 ; lineSize--)
                {
                    byte[] temp = new byte[lineSize];
                    Array.Copy(codeWordBytes, temp, lineSize);
                    startTriangleLine += lineSize;

                    if (!checkOnParity(temp))
                    {
                        countErrors++;
                    }
                    Array.Copy(temp, 0, result, i, temp.Length-1);
                    i += temp.Length;
                }

                if (countErrors > 0)
                {
                    throw new DecodeErorr("Triangular decode was fail!", countErrors);
                }
            }
            else
            {
                throw new ImpossibleBuildTriangularException("Impossible build triangular!");
            }

            return new BitNumber(result);
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
