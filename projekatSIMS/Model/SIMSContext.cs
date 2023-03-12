﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    
    //OVO JE KLASA KOJA PREDSTAVLJA "BAZU PODATAKA"
    public class SIMSContext
    {
        private char delimiter = '|'; //ono po cemu se splituje string u txt fajlu
        private static string _projectPath = System.Reflection.Assembly.GetExecutingAssembly().Location
            .Split(new string[] { "bin" }, StringSplitOptions.None)[0] + "\\Resources\\Data\\"; //Lokacija do txt foldera

        private static SIMSContext instance;

        //Cuvamo listu svih ENTITETA - da bi ih citali i pisali
        private List<Entity> users = new List<Entity>();

        private User loginUser;

        public static SIMSContext Instance
        { get
          {
           if (instance == null)
            {
              instance = new SIMSContext();
              instance.Load();
            }

           return instance;
          }        
        }

        public void Save()
        {
            GenericSave(users, "users.txt");
        }

        public void GenericSave(List<Entity> entitites, string fileName) //Prosledis sta sejvujes i gde sejvujes
        {
            {
                using (StreamWriter file = new StreamWriter(_projectPath + fileName)) //Cita file liniju po liniju
                {
                    foreach (Entity entity in entitites)
                    {
                        file.WriteLine(entity.ExportToString()); //Pre nego sto upise u file prebaci u FORMU za upis u file
                    }
                }


            }
        }

        public void Load()
        {
            GenericLoad(users,"users.txt",typeof(User));
        }

        public void GenericLoad(List<Entity> entities, string fileName, Type type) 
        { 
            foreach(string line in File.ReadLines(_projectPath + fileName)) 
            {
                string[] parts = line.Split(delimiter);

                if(type == typeof(User))
                {
                    User newEntity = new User();
                    newEntity.ImportFromString(parts);
                    entities.Add(newEntity);
                }
            }
        
        }

        public int GenerateId(List<Entity> entities)
        {
            int max = -1;
            foreach(Entity it in entities) 
            { 
                if(it.Id > max)
                {
                    max = it.Id;
                }
            }

            return max + 1;
        }

        public void Set(Type type, List<Entity> entities)
        {
            if(type == typeof(User))
            {
                users = entities;
                return;
            }
        }

        public List<Entity> Get(Type type) //Vraca celu listu entiteta 
        {
            if(type == typeof(User))
            {
                return users;
            }

            return null; //Mora jedan biti default return
        }

        public List<Entity> Users
        { get { return users; }
          set { users = value; }
        }
        public User LoginUser
        {
            get { return loginUser; }
            set { loginUser = value; }
        }





    }
}
