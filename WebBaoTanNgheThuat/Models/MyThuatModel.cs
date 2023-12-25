using System;
using System.ComponentModel.DataAnnotations;

namespace WebBaoTanNgheThuat.Models
{

    public class DangBaiViewModel
    {
        [Required]
        [Display(Name = "Ten")]
        public String Ten { get; set; }
        [Required]
        [Display(Name = "TenTacGia")]
        public String TenTacGia { get; set; }
        [Required]
        [Display(Name = "NoiDung")]
        public String NoiDung { get; set; }
        [Required]
        [Display(Name = "SRC")]
        public String SRC { get; set; }
    }
}