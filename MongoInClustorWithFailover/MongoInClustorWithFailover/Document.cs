﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoInClustorWithFailover
{
    public class Document
    {
        [BsonId]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}