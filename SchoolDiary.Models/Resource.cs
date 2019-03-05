using System;

namespace SchoolDiary.Models
{
    public class Resource
    {
        public Resource()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public byte[] File { get; set; }
    }
}
