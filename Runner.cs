using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bia
{
    public class Runner
    {
        public static void WhatTimeIs()
        {
            Speaker.Speak(DateTime.Now.ToLongTimeString());
        }
    }
}
