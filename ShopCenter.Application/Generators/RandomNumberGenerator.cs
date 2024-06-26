﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Application.Generators
{
    public static class RandomNumberGenerator
    {
        public static int GenerateRendomInteger(int minValue, int maxValue)
        {
            var random = new System.Random();
            if (minValue == 0)
                minValue = 10000;
            if (maxValue == 0)
                maxValue = 99999;
            return random.Next(minValue, maxValue);
        }
    }
}
