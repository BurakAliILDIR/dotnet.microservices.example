﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Message
{
    public class CourseNameChangedEvent
    {
        public string CourseId { get; set; }
        public string CourseUpdateName { get; set; }
    }
}