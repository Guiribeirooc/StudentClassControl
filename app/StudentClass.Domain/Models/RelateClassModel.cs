﻿namespace StudentClass.Domain.Models
{
    public class RelateClassModel
    {
        public int IdStudent { get; set; }
        public int IdClass { get; set; }
        public ClassModel? Class { get; set; }
        public StudentModel? Student { get; set; }
    }
}
