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

/// <remarks>La liste dynamique se construit avec une liste cible (statique ou dynamique) et un filtre (à définir).</remarks>
public class DynamicList : IDynamicList
{
    public string Name { get; set; }
    public List<Task> list;

	public virtual TaskList target
	{
		get;
		set;
	}

    private Func<GTDItem, Boolean> filter;

    public DynamicList(TaskList target, Func<GTDItem, Boolean> filter) {
        this.target = target;
        this.filter = filter;
    }

	public virtual IEnumerator<GTDItem> GetEnumerator()
	{
		return target.Where(filter).GetEnumerator();
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
        this.target.removeTask(t);
    }


    public void AddTask(Task t)
    {
        target.AddTask(t);
    }

    public void AddSubList(TaskList l)
    {
        target.AddSubList(l);
    }

    public void removeSubList(TaskList l)
    {
        target.removeSubList(l);
    }
}

