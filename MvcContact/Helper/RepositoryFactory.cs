using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcContact
{
    public class RepositoryFactory
    {
        static PersonRepositoryJson _repoPerson;
        public static IRepositoryPerson CreateRepo(string tip)
        {
            if (_repoPerson == null)
            {
                if (tip == "PERSON")
                {
                    lock (new object())
                    {
                        _repoPerson = new PersonRepositoryJson();
                    }
                    return new PersonRepositoryJson();
                }
                else
                    return null;
            }
            else 
                return _repoPerson;
        }
    }
}
