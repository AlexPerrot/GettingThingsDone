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
using System.Collections.ObjectModel;
using GettingThingsDone.src.model;
using GettingThingsDone.src.model.visitor;

public class StaticList : IStaticList
{
    private ObservableCollection<GTDItem> list = new ObservableCollection<GTDItem>();
    public ObservableCollection<GTDItem> List { get { return list; } }
    public String Name { get; set; }

    public StaticList(String name)
    {
        this.Name = name;
    }

	public virtual IEnumerator<GTDItem> GetEnumerator()
	{
        return list.GetEnumerator();
	}

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

	public virtual void AddItem(GTDItem item)
	{
        list.Add(item);
    }

	public virtual void RemoveItem(GTDItem item)
	{
        list.Remove(item);
	}

	public virtual T accept<T>(GTDVisitor<T> v)
	{
        return v.visit(this);
    }

    public override string ToString()
    {
        return this.Name;
    }

    public void removeTask(Task t)
    {
        list.Remove(t);
    }


    public void AddTask(Task t)
    {
        this.AddItem(t);
    }

    public void AddSubList(TaskList l)
    {
        this.AddItem(l);
    }

    public void removeSubList(TaskList l)
    {
        this.RemoveItem(l);
    }


    public void Delete()
    {
        // do nothing
    }
}

