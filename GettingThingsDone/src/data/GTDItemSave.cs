//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a �t� g�n�r� par un outil
//     Les modifications apport�es � ce fichier seront perdues si le code est r�g�n�r�.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Linq;

namespace GettingThingsDone.data
{
    /// <remarks>Charg� de l'enregistrement des taches dans la bdd locale et de la r�cup�ration de taches dans la bdd locale.</remarks>
    public class GTDItemSave : TaskVisitor<GTDItem>
    {
        private static Lazy<GTDItemSave> lazy { get; set; }

        public static GTDItemSave Instance
        {
            get { return lazy.Value; }
        }

        private GTDItemSave() {}

        public GTDItem visit(TaskList list)
        {
            DataClassesDataContext local = LocalDatabaseProvider.Instance;

            Lists newList = new Lists();
            newList.Title = list.Name;
            /*newList.Description = list.description;
            newList.CreationDate = DateTime.Now;*/

            return list;
        }

        public GTDItem visit(SingleTask task)
        {
            DataClassesDataContext local = LocalDatabaseProvider.Instance;

            Tasks newTask = new Tasks();
            newTask.Title = task.Title;
            newTask.Description = task.Description;
            newTask.CreationDate = DateTime.Now;
            newTask.DueDate = task.DueDate;
            newTask.Owner = 42; //TODO: simplify the client database. This identifier is useless in local mode.
            //TODO: save task.context and task.reminder (add context to the Tasks table in both databases).

            return task;
        }

        public GTDItem visit(Project project)
        {
            DataClassesDataContext local = LocalDatabaseProvider.Instance;

            Projects newProject = new Projects();
            newProject.Title = project.Title;
            newProject.Description = project.Description;

            return project;
        }

    }
}
