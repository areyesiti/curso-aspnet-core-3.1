using CoreGram.Data;
using CoreGram.Data.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Repositories
{
    public class UserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Inicializar()
        {
            if (_context.Users.Count() <= 0)
            {
                User[] users = new User[]
                {
                    new User { Login = "User1", Password = "User1", Email = "user1@mail.com"},
                    new User { Login = "User2", Password = "User2", Email = "user2@mail.com"},
                };

                _context.Users.AddRange(users);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("No se ha encontrado el usuario con id: " + id);
            }

            return user;
        }

        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return await GetById(user.Id);            
        }

        public async Task<User> Update(int id, User user)
        {

            if (id != user.Id)
            {
                throw new Exception("El id no coincide");
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("No se ha encontrado el usuario");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
