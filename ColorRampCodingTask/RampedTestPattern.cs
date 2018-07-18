using System;

namespace ColorRampCodingTask
{
    class RampedTestPattern
    {

        private const int MINIMUM_WIDTH = 2;
        private const int MINIMUM_HEIGHT = 2;

        private const ushort RED_BIT_MASK = 0x1F;
        private const ushort GREEN_BIT_MASK = 0x7E0;
        private const ushort BLUE_BIT_MASK = 0xF800;

        private const ushort GREEN_BIT_SHIFT = 5;
        private const ushort BLUE_BIT_SHIFT = 11;

        private int width;
        private int height;

        private ushort top_left_red_component = 0;
        private ushort top_left_green_component = 0;
        private ushort top_left_blue_component = 0;

        private ushort top_right_red_component = 0;
        private ushort top_right_green_component = 0;
        private ushort top_right_blue_component = 0;

        private ushort bottom_left_red_component = 0;
        private ushort bottom_left_green_component = 0;
        private ushort bottom_left_blue_component = 0;

        private ushort bottom_right_red_component = 0;
        private ushort bottom_right_green_component = 0;
        private ushort bottom_right_blue_component = 0;

        public RampedTestPattern(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public ushort this[int col, int row]
        {
            get { return CalculateLocationColor(col, row); }
        }

        public int Width
        {
            get { return this.width; }
            private set
            {
                if (value < MINIMUM_WIDTH)
                {
                    this.width = MINIMUM_WIDTH;
                }
                else
                {
                    this.width = value;
                }
            }
        }

        public int Height
        {
            get { return this.height; }
            private set
            {
                if (value < MINIMUM_HEIGHT)
                {
                    this.height = MINIMUM_HEIGHT;
                }
                else
                {
                    this.height = value;
                }
            }
        }

        public UInt16 TopLeftColor
        {
            set
            {
                SeperateColorComponents(value,
                 out this.top_left_red_component,
                 out this.top_left_green_component,
                 out this.top_left_blue_component);
            }
        }

        public UInt16 TopRightColor
        {
            set
            {
                SeperateColorComponents(value,
                 out this.top_right_red_component,
                 out this.top_right_green_component,
                 out this.top_right_blue_component);
            }
        }

        public UInt16 BottomLeftColor
        {
            set
            {
                SeperateColorComponents(value,
                 out this.bottom_left_red_component,
                 out this.bottom_left_green_component,
                 out this.bottom_left_blue_component);
            }
        }

        public UInt16 BottomRightColor
        {
            set
            {
                SeperateColorComponents(value,
                 out this.bottom_right_red_component,
                 out this.bottom_right_green_component,
                 out this.bottom_right_blue_component);
            }
        }

        private void SeperateColorComponents(ushort color, out ushort red_component, out ushort green_component, out ushort blue_component)
        {
            red_component = (ushort)(color & RED_BIT_MASK);
            green_component = (ushort)((color & GREEN_BIT_MASK) >> GREEN_BIT_SHIFT);
            blue_component = (ushort)((color & BLUE_BIT_MASK) >> BLUE_BIT_SHIFT);
        }

        private ushort CombineColorComponents(ushort red_component, ushort green_component, ushort blue_component)
        {
            ushort combined_color = 0;

            combined_color += (ushort)((blue_component << BLUE_BIT_SHIFT) & BLUE_BIT_MASK);
            combined_color += (ushort)((green_component << GREEN_BIT_SHIFT) & GREEN_BIT_MASK);
            combined_color += (ushort)(red_component & RED_BIT_MASK);

            return combined_color;
        }

        private ushort CalculateLocationColor(int col, int row)
        {
            double max_range_x = (this.width - 1);
            double max_range_y = (this.height - 1);

            double top_left_influence = (1.0 - ((double)col / max_range_x)) * (1.0 - ((double)row / max_range_y));
            double top_right_influence = (1.0 - ((double)(max_range_x - col) / max_range_x)) * (1.0 - ((double)row / max_range_y));
            double bottom_left_influence = (1.0 - ((double)col / max_range_x)) * (1.0 - ((double)(max_range_y - row) / max_range_y));
            double bottom_right_influence = (1.0 - ((double)(max_range_x - col) / max_range_x)) * (1.0 - ((double)(max_range_y - row) / max_range_y));

            ushort red_component = (ushort)((top_left_red_component * top_left_influence)
                                            + (top_right_red_component * top_right_influence)
                                            + (bottom_left_red_component * bottom_left_influence)
                                            + (bottom_right_red_component * bottom_right_influence));

            ushort green_component = (ushort)((top_left_green_component * top_left_influence)
                                            + (top_right_green_component * top_right_influence)
                                            + (bottom_left_green_component * bottom_left_influence)
                                            + (bottom_right_green_component * bottom_right_influence));

            ushort blue_component = (ushort)((top_left_blue_component * top_left_influence)
                                            + (top_right_blue_component * top_right_influence)
                                            + (bottom_left_blue_component * bottom_left_influence)
                                            + (bottom_right_blue_component * bottom_right_influence));

            return CombineColorComponents(red_component, green_component, blue_component);
        }


    }
}
