﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CRUD.Models;

public partial class Department
{
    public int Dept_id { get; set; }

    public string Dept_Name { get; set; }

    public virtual ICollection<student> students { get; set; } = new List<student>();
}