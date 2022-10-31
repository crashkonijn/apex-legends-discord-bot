﻿using Domain.Enums;

namespace Domain.Models;

public class Stat
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public int Season { get; set; }
    public string Legend { get; set; }
    public int Rank { get; set; }
    public string RankName { get; set; }
    public int Kills { get; set; }
}