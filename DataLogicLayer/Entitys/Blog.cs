﻿using BusinessLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer.Entitys
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }



        public string CategoryTitle { get; set; }
    }
}
