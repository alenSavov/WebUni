using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Web.Models
{
    public class CreateResourceInputViewModel
    {
        public string Name { get; set; }

        public byte[] Image { get; set; }
    }
}
