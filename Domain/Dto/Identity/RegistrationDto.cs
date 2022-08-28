﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static Entities.DatabaseConfigurationConstants;

namespace Domain.Dto.Account
{
    public class RegistrationDto
    {
        [Required]
        [MinLength(USERNAME_MIN_LENGTH)]
        [MaxLength(USERNAME_MAX_LENGTH)]
        public string Username { get; set; }

        [MinLength(FIRSTNAME_LASTNAME_MIN_LENGTH)]
        [MaxLength(FIRSTNAME_LASTNAME_MAX_LENGTH)]
        public string? FirstName { get; set; }

        [MinLength(FIRSTNAME_LASTNAME_MIN_LENGTH)]
        [MaxLength(FIRSTNAME_LASTNAME_MAX_LENGTH)]
        public string? LastName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
