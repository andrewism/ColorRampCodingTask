using System;

namespace ColorRampCodingTask
{
    class Program
    {
        const int width = 15;
        const int height = 9;

        static void Main(string[] args)
        {
            ushort top_left_color = Convert.ToUInt16(args[0], 16);
            ushort top_right_color = Convert.ToUInt16(args[1], 16);
            ushort bottom_left_color = Convert.ToUInt16(args[2], 16);
            ushort bottom_right_color = Convert.ToUInt16(args[3], 16);

            RampedTestPattern rtp = new RampedTestPattern(width, height);

            rtp.TopLeftColor = top_left_color;
            rtp.TopRightColor = top_right_color;
            rtp.BottomLeftColor = bottom_left_color;
            rtp.BottomRightColor = bottom_right_color;

            for (int row = 0; row < rtp.Height; row++)
            {
                for (int col = 0; col < rtp.Width; col++)
                {
                    Console.Write(String.Format("{0:X4} ", rtp[col, row]));
                }
                Console.WriteLine();
            }
        }
    }
}