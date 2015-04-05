using System;

namespace TJW.Model
{
    public class Tea
    {
        public int TeaId { get; set; }

        public string TeaName { get; set; }

        public int TeaCount { get; set; }

        public float TeaOriPrice { get; set; }

        public float TeaPrice { get; set; }

        public string StuffUGUID { get; set; }

        public int TeaTypeId { get; set; }

        public int TeaYear { get; set; }

        public string TeaDescription { get; set; }

        public bool IsValid { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateUserId { get; set; }

        public DateTime ModifyDate { get; set; }

        public int ModifyUserId { get; set; }

        //for show in the page
        public string PicturePath { get; set; }
    }
}
