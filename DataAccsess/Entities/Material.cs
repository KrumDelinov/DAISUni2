﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccsess.Entities
{
    [Index("CoursesId", Name = "IX_Materials_CoursesId")]
    public partial class Material
    {
        [Key]
        public int Id { get; set; }
        public int MaterialType { get; set; }
        public string MaterialUrl { get; set; }
        public byte[] Content { get; set; }
        public int? CoursesId { get; set; }

        [ForeignKey("CoursesId")]
        [InverseProperty("Materials")]
        public virtual Course Courses { get; set; }
    }
}