using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using LanguageSchools.Repositories;
using LanguageSchools.Services;

namespace LanguageSchools.Models
{
    public class Data
    {
        private static Data instance;

        //public List<User> Users { get; set; }
        //public List<Professor> Professors { get; set; }
        //public List<Student> Students { get; set; }
        //public List<SchoolClass> SchoolClasses { get; set; }
        //public List<Address> Adresses { get; set; }

        public User loggedUser { get; set; }

        static Data() { }

        private Data()
        {
            loggedUser = new User();
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
}
