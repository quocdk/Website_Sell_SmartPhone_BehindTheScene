﻿using App_Data.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class Rom
    {
        [Key]
        public string? IdRom { get; set; }
        public string? MaRom { get; set; }
        public TrangThaiEnum TrangThai { get; set; }
        public DungLuongRomEnum DungLuong { get; set; }
        public string? TenRom { get; set; }
        public virtual List<SanPhamChiTiet>? SanPhamChiTiets { get; set; }
    }
}
