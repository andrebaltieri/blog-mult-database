using System.Collections.Generic;

namespace Blog.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}