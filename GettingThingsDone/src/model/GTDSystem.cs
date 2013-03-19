﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil
//     Les modifications apportées à ce fichier seront perdues si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GettingThingsDone.src.model;
using GettingThingsDone.src.model.visitor;
using System.Collections.ObjectModel;

/// <remarks>Cette classe décrit le système général de GTD, avec la boite de reception et les listes de l'utilisateur. On ajoutera surement le calendrier ici.</remarks>
public class GTDSystem : IGTDSystem
{

    public string Name { get; set; }
    private TaskList inbox ;//= new StaticList("Inbox");

    private List<Task> projects = new List<Task>();
    public List<Task> Projects { get { return projects; } }
    
    public virtual TaskList Inbox
	{
        get { return inbox; }
        set { inbox = value; }
	}

    private TaskList someday;// = new StaticList("Someday");
    public TaskList Someday
    { 
        get { return someday; }
        set { someday = value; }
    }

    private TaskList waiting;// = new StaticList("Waiting");
    public TaskList Waiting 
    { 
        get { return waiting; }
        set { waiting = value; }
    }


    private ObservableCollection<TaskList> contexts = new ObservableCollection<TaskList>();

    public ObservableCollection<TaskList> Contexts { get { return contexts; } }

    public GTDSystem()
    {
       
    }

	public virtual IEnumerator<GTDItem> GetEnumerator()
	{
        yield return Inbox;
        yield return Waiting;
        yield return Someday;
        foreach (TaskList item in contexts)
            yield return item;
	}

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

	public virtual T accept<T>(GTDVisitor<T> v)
	{
        return v.visit(this);
    }

    public void removeTask(Task t)
    {
        foreach (TaskList l in this)
            l.removeTask(t);
        foreach (IProject p in Projects)
            p.RemoveTask(t);
        t.Delete();
    }

    public void removeContext(IStaticList l)
    {
        foreach (Task t in l.ToList())
            removeTask(t);

        contexts.Remove(l);

        l.Delete();

        if (PropertyChanged != null)
            PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Contexts"));
    }
    
    public void removeProject(IProject project)
    {
        Projects.Remove(project);
        IEnumerable<Task> tl = project.Tasks.ToList(); // on copie la liste des taches, sinon l'itération lance une exception
        foreach (Task task in tl)
        {
            removeTask(task);
        }
        project.Delete();
    }

    public void AddTask(Task t)
    {
        Inbox.AddTask(t);
    }

    public void AddSubList(TaskList l)
    {
        Contexts.Add(l);
    }

    public void removeSubList(TaskList l)
    {
        Contexts.Remove(l);
    }


    public void Delete()
    {
        // do nothing
    }

    public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
}

