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

/// <remarks>Cette classe décrit le système général de GTD, avec la boite de reception et les listes de l'utilisateur. On ajoutera surement le calendrier ici.</remarks>
public class GTDSystem : IGTDSystem
{

    public string Name { get; set; }
    private StaticList inbox = new StaticList("Inbox");
    
    public virtual StaticList Inbox
	{
        get { return inbox; }
	}

    private TaskList today = new StaticList("Today");
    public TaskList Today { get { return today; } }

    private TaskList tomorrow = new StaticList("Tomorrow");
    public TaskList Tomorrow { get { return tomorrow; } }

    private TaskList someday = new StaticList("Someday");
    public TaskList Someday { get { return someday; } }

    private TaskList waiting = new StaticList("Waiting");
    public TaskList Waiting { get { return waiting; } }

    private List<StaticList> contexts = new List<StaticList>() 
        {
            new StaticList("Work"),
            new StaticList("Home"),
            new StaticList("Phone"),
            new StaticList("Computer"),
            new StaticList("Errands")
        };

    public List<StaticList> Contexts { get { return contexts; } }

	public virtual IEnumerator<GTDItem> GetEnumerator()
	{
        yield return Inbox;
        yield return Today;
        yield return Tomorrow;
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
        t.Delete();
    }
}

