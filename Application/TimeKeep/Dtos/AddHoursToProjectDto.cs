﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Project.Dtos
{
    public class AddHoursToProjectDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}