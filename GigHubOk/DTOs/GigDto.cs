﻿using System;

namespace GigHubOk.DTOs
{
    public class GigDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public UserDto Artist { get; set; }
        public DateTime Datetime { get; set; }
        public string Venue { get; set; }
        public GenreDto Genre { get; set; }
    }
}