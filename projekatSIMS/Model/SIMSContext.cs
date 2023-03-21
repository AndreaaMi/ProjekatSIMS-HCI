using System;
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
        private List<Entity> tours = new List<Entity>();
        private List<Entity> accommodations = new List<Entity>();
        private List<Entity> keypoints = new List<Entity>();
        private List<Entity> accommodationReservations = new List<Entity>();

        private User loginUser;

        public static SIMSContext Instance
        {
            get
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
            GenericSave(accommodations, "accommodations.txt");
            GenericSave(tours, "tours.txt");
            GenericSave(keypoints, "keypoints.txt");
            GenericSave(accommodationReservations, "accommodation_reservations.txt");

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
            GenericLoad(users, "users.txt", typeof(User));
            GenericLoad(accommodations, "accommodations.txt", typeof(Accommodation));
            GenericLoad(tours, "tours.txt", typeof(Tour));
            GenericLoad(keypoints, "keypoints.txt", typeof(KeyPoints));
            GenericLoad(accommodationReservations, "accommodation_reservations.txt", typeof(AccommodationReservation));
        }

        public void GenericLoad(List<Entity> entities, string fileName, Type type)
        {
            foreach (string line in File.ReadLines(_projectPath + fileName))
            {
                string[] parts = line.Split(delimiter);

                if (type == typeof(User))
                {
                    User newEntity = new User();
                    newEntity.ImportFromString(parts);
                    entities.Add(newEntity);
                }
                if (type == typeof(Accommodation))
                {
                    Accommodation newAccommodation = new Accommodation();
                    newAccommodation.ImportFromString(parts);
                    entities.Add(newAccommodation);
                }
                if (type == typeof(Tour))
                {
                    Tour newEntity = new Tour();
                    newEntity.ImportFromString(parts);
                    entities.Add(newEntity);
                }
                if (type == typeof(KeyPoints))
                {
                    KeyPoints newEntity = new KeyPoints();
                    newEntity.ImportFromString(parts);
                    entities.Add(newEntity);
                }
                if (type == typeof(AccommodationReservation))
                {
                    AccommodationReservation newEntity = new AccommodationReservation();
                    newEntity.ImportFromString(parts);
                    entities.Add(newEntity);
                }
            }

            
        }
public int GenerateId(List<Entity> entities)
    {
        int max = -1;
        foreach (Entity it in entities)
        {
            if (it.Id > max)
            {
                max = it.Id;
            }
        }

        return max + 1;
    }

    public void Set(Type type, List<Entity> entities)
    {
        if (type == typeof(User))
        {
            users = entities;
            return;
        }

        if (type == typeof(Accommodation))
        {
            accommodations = entities;
            return;
        }
        if (type == typeof(Tour))
        {
            tours = entities;
            return;
        }
        if(type == typeof(KeyPoints))
        {
            keypoints = entities;
            return;
        }
        if(type == typeof(AccommodationReservation))
        {
            accommodationReservations = entities;
            return;
        }
    }

    public List<Entity> Get(Type type) //Vraca celu listu entiteta 
    {
        if (type == typeof(User))
        {
            return users;
        }
        if (type == typeof(Accommodation))
        {
            return accommodations;
        }
        if (type == typeof(KeyPoints))
        {
            return keypoints;
        }
        if (type == typeof(AccommodationReservation))
        {
            return accommodationReservations;
        }
        if (type == typeof(Tour))
        {
            return tours;
        }

            return null; //Mora jedan biti default return
    }

    public List<Entity> Users
    {
        get { return users; }
        set { users = value; }
    }

    public List<Entity> Accommodations
    {
        get { return accommodations; }
        set { accommodations = value; }
    }

    public User LoginUser
    {
        get { return loginUser; }
        set { loginUser = value; }
    }

    public List<Entity> Tours
    {
        get { return tours; }
        set { tours = value; }
    }

    public List<Entity> Keypoints
    { get { return keypoints; }
      set {  keypoints = value; }
    }

    public List<Entity> AccommodationReservations
    { 
      get { return accommodationReservations; }
      set {  accommodationReservations = value; }
    }
    }

    


}



       


