﻿using System;

namespace SportCenter.Models
{
    class PostMatchModel
    {
        public int Id { get; set; }
        public int TournamentModelId { get; set; }
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public string Stage { get; set; }
        public DateTime Date { get; set; }
        public string Winner { get; set; }
    }
}
