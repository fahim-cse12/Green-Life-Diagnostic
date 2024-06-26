﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Doctor : BaseEntity
    {
        [Column("DoctorId")]
        public Guid Id { get; set; }
        [MaxLength(60)]
        public string Name { get; set; }
        public decimal Fee { get; set; }
        [MaxLength(200)]
        public string Specialty { get; set; }
        [MaxLength(100)]
        public string ScheduledDay { get; set; }
    }
}
