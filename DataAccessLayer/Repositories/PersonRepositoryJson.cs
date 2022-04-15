using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PersonRepositoryJson: IRepositoryPerson
    {
        static List<Person> _people=new List<Person>();

        public void AddOrUpdate(Person person)
        {
            

            if (person.Id<=0)
            {
                person.Id = _people.Max(c => c.Id) + 1;
                _people.Add(person);
            }
            else
            {
                Person updateEdilecek = _people.First(c => c.Id == person.Id);
                updateEdilecek.Name = person.Name;
                updateEdilecek.Surname = person.Surname;
                updateEdilecek.Phone = person.Phone;
            }
            SaveChanges();
        }
        public void Delete(Person person)
        {
            _people.Remove(person);
            SaveChanges();
        }

        public void Delete(int id)
        {
            Person silinecek = _people.FirstOrDefault(c => c.Id == id);
                //First : kayıt yoksa hata alır
                //FirstOrDefault :kayıt yoksa null döner
                //Single: kayıt 1 adet değilse hata alır

            if (silinecek!=null)
            {
                Delete(silinecek);
            }
        }

            public List<Person> List()
        {
            string fileContent = File.ReadAllText(@"C:\Users\SC-205 1\Downloads\TOUSollution-master\TOUSollution-master\TuSollution\bin\Debug\net5.0-windows\Kisiler.json");
            _people = JsonSerializer.Deserialize<List<Person>>(fileContent);
            return _people.ToList();
        }

        public void SaveChanges() 
        {
            string serializedPeople=JsonSerializer.Serialize(_people);
            File.WriteAllText(@"C:\Users\SC-205 1\Downloads\TOUSollution-master\TOUSollution-master\TuSollution\bin\Debug\net5.0-windows\Kisiler.json", serializedPeople);

        }

    }
}
