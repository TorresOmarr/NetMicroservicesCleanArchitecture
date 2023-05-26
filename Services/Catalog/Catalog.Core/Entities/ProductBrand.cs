﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Entities
{
    public class ProductBrand : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;
    }
}
