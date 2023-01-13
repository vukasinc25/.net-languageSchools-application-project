using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using LanguageSchools.Repositories;
using LanguageSchools.Services;

namespace LanguageSchools.Models
{
    [Serializable]
    public class Data
    {
        [NonSerialized]
        private static Data instance;

        public List<User> Users { get; set; }
        public List<Professor> Professors { get; set; }
        public List<Student> Students { get; set; }
        public List<SchoolClass> SchoolClasses { get; set; }
        public List<Address> Adresses { get; set; }

        static Data() { }

        private Data()
        {
            Users = new List<User>();
            Professors = new List<Professor>();
            Students = new List<Student>();
            SchoolClasses = new List<SchoolClass>();
            Adresses = new List<Address>();
        }

        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Data();
                }
                
                return instance;
            }

            private set => instance = value;
        }

        public void Initialize()
        {
            Address address = new Address
            {
                City = "Novi Sad",
                Country = "Srbija",
                Street = "ulica1",
                StreetNumber = "22",
                Id = 1
            };
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.dataFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, this);
            }
        }

        public static void Load()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(Config.dataFilePath, FileMode.Open, FileAccess.Read))
                {
                    Instance = (Data)formatter.Deserialize(stream);
                }
            }
            catch (Exception ejo)
            {
                Instance = new Data();
            }

        }
    }
}
