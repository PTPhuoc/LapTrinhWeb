using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebBaoTanNgheThuat.Models
{
    public class TaiKhoanModel : Controller
    {
        // GET: TaiKhoanModel
        
    }

    public class DangNhapViewModel
    {
        [Required]
        [Display(Name = "Ten")]
        public String Ten { get; set; }

        [Required]
        [Display(Name = "Gmail")]
        [EmailAddress]
        public String Gmail { get; set; }

        [Required]
        [Display(Name = "MatKhau")]
        public String MatKhau { get; set; }
    }

    public class DangKyViewModel
    {
        [Required]
        [Display(Name = "Ten")]
        public String Ten { get; set; }

        [Required]
        [Display(Name = "Gmail")]
        [EmailAddress]
        public String Gmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} phải có {2} chữ cái.", MinimumLength = 6)]
        [Display(Name = "MatKhau")]
        public String MatKhau { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "XatNhanMk")]
        public String XatNhanMk { get; set; }
    }

    public class ConfirmCodeViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "VerifyCode")]
        public String VerifyCode { get; set; }

        public Boolean StatusCode { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [Display(Name = "MatKhau")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} phải có {2} chữ cái.", MinimumLength = 6)]
        public String MatKhau { get; set; }

        [Required]
        [Display(Name = "MatKhau")]
        [DataType(DataType.Password)]
        public String XatNhanMk { get; set; }
    }

    public class DoiTenViewModel
    {
        [Required]
        public String Ten { get; set; }

        [Required]
        [EmailAddress]
        public String Gmail { get; set; }
    }

    public class DoiMatKhauViewModel
    {
        [Required]
        [Display(Name = "MatKhauCu")]
        [DataType(DataType.Password)]
        public String MatKhauCu { get; set; }

        [Required]
        [Display(Name = "MatKhauMoi")]
        [DataType(DataType.Password)]
        public String MatKhauMoi { get; set; }

        [Required]
        [Display(Name = "XacNhanMk")]
        [DataType(DataType.Password)]
        public String XacNhanMk { get; set; }
    }
}