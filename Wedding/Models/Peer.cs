﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Wedding.Models
{
    public partial class Peer
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public virtual Redeem IdNavigation { get; set; }
    }
}