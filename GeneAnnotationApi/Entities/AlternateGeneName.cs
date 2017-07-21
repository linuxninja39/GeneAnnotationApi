﻿using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class AlternateGeneName
    {
        public int Id { get; set; }
        public int GeneId { get; set; }
        public string Name { get; set; }

        public virtual Gene Gene { get; set; }
    }
}
