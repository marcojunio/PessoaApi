using System;
using System.Linq.Expressions;
using Pessoa.Domain.Entities;

namespace Pessoa.Domain.Queries{ 
    public static class UsersQueries { 

        public static Expression<Func<UserEntity,bool>> GetById(string id){
            return x => x.Id == id;
        }

        public static Expression<Func<UserEntity,bool>> GetByLoginAndPassoword(string login,string password){
            return x => x.Login == login && x.Password == password;
        }
    }
}
