using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp1.Models;
public class Role
{
    [PrimaryKey, AutoIncrement]
    public string Name { get; set; }
}