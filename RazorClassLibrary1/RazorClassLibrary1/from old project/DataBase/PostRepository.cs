using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Internal;
using WebApplication1.Database.Entites;

namespace WebApplication1.Database
{
    public class PostRepository
    {
        public PostRepository(DataBase context)
        {
            _context = context;
        }

        public PostRepository()
        {
            _context = new DataBase();
        }

        public void AddPost(string header, string body, Guid userId)
        {
            _context.Posts.Add(new Post(userId, header, body));
            _context.SaveChanges();
        }
        
        public bool ContainPost(string header) => _context.Posts.Any(p => p.Header.Equals(header));

        public Post GetPostByHeader(string header) => _context.Posts.First(p => p.Header.Equals(header));

        public List<Post> GetUsersPosts(Guid userId) => _context.Posts.Where(p => p.UserId.Equals(userId)).ToList();
        
        public List<Post> GetAllPosts() => _context.Posts.ToList();
        
        private readonly DataBase _context;
    }
}