﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Services.Catolog.Model;

namespace Services.Catolog.Dto.Course
{
    internal class CourseDto
    {
        public string Id { get; set; }

        public string CategoryId { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public DateTime CreatedAt { get; set; }

        public FeatureDto Feature { get; set; }

        public CategoryDto Category { get; set; }
    }
}
