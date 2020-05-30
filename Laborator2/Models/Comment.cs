﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.Models
{
    public class Comment
    {

        public long Id { get; set; }

        public string Author { get; set; }
        public string Text { get; set; }
        public Movie Movie { get; set; }
   
    }
}
