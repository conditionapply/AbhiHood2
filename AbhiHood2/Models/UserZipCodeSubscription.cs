using System;
using System.Collections.Generic;

namespace AbhiHood2.Models
{
    public partial class UserZipCodeSubscription
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int ZipCode { get; set; }

        //public virtual AspNetUser User { get; set; } = null!;
    }
}
