using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp1.Models;
[Table("Roles")]
public class Role
{
    [MaxLength(250), Unique]
    public string Name { get; set; }
}