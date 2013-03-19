using GettingThingsDone.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.data
{
    class DBUser : IUser
    {
        public string Username
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Users u = db.Users.Single(x => x.Id == id);
                return u.Username;
            }

            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Users u = db.Users.Single(x => x.Id == id);
                u.Username = value;
                db.SubmitChanges();
            }
        }

        public string Password
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Users u = db.Users.Single(x => x.Id == id);
                return u.Password;
            }

            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Users u = db.Users.Single(x => x.Id == id);
                u.Password = value;
                db.SubmitChanges();
            }
        }

        public string Mail
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Users u = db.Users.Single(x => x.Id == id);
                return u.Mail;
            }

            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Users u = db.Users.Single(x => x.Id == id);
                u.Mail = value;
                db.SubmitChanges();
            }
        }

        public void Delete()
        {
            DataClassesDataContext db = dbProvider.Database;
            Users u = db.Users.Single(x => x.Id == this.id);
            db.Users.DeleteOnSubmit(u);
            db.SubmitChanges();
        }

        private int id;
        public int Id { get { return id; } }

        private IDatabaseProvider dbProvider;

        public DBUser(Users user, IDatabaseProvider dbProvider)
        {
            this.id = user.Id;
            this.dbProvider = dbProvider;
        }
    }
}

