﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EktronRich : Attribute
    {
        public EktronRich()
        {

        }
    }
}