﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AccountProjects
    {
        [Key]
        public int Id { get; set; }

        public int AccountId { get; set; }

        public int ProjectId { get; set; }
    }
}