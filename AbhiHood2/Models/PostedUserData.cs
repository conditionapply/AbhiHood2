using System.Web;

namespace AbhiHood2.Models
{
    public partial class PostedUserData
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string PostedText { get; set; } = null!;
        public string PicturePath { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public int ZipCode { get; set; }
        public string? SysAddress { get; set; }
        public string? SysCity { get; set; }
        public string? SysState { get; set; }
        public int? SysZipCode { get; set; }
        public string? SysPicCarNumber { get; set; }
        public string? SysPicInfo1 { get; set; }
        public string? SysPicInfo2 { get; set; }
        //public HttpPostedFileBase PicturePathData { get; set; }
        //public virtual AspNetUser User { get; set; } = null!;
    }
}
