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
using GettingThingsDone;
using GettingThingsDone.data;
using GettingThingsDone.src.model;
using GettingThingsDone.src.model.visitor;

public class SingleTask : ISingleTask
{

    public String Title { get; set; }
    public String Description { get; set; }
    public Boolean Done { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    private DateTimeOffset creationDate;
    public DateTimeOffset CreationDate { get { return creationDate; } }
    public DateTimeOffset Reminder { get; set; }
    public String Context { get; set; }

    public SingleTask() : this("")
    {
        
    }

    public SingleTask(string title)
        : this("", "")
    {

    }

    public SingleTask(string title, string desc) : this(title, desc, null)
    {

    }

    public SingleTask(string title, string desc, DateTimeOffset? dueDate)
    {
        this.creationDate = DateTimeOffset.Now;
        this.Title = title;
        this.Description = desc;
        this.DueDate = dueDate;

       /* this.dbProxy = new Tasks();
        dbProxy.CreationDate = creationDate;
        dbProxy.Description = Description;
        dbProxy.DueDate = DueDate;
        dbProxy.Title = Title;
        dbProxy.Users = (App.Current as App).Admin;
        dbProxy.Done = false;

       

        (App.Current as App).DB.Tasks.InsertOnSubmit(dbProxy);
        (App.Current as App).DB.SubmitChanges();*/

        this.accept(GettingThingsDone.data.GTDItemSave.Instance);
    }

    public override string ToString()
    {
        return "SingleTask : " + Title + "\n" + Description;
    }

    public T accept<T>(GTDVisitor<T> v)
    {
        throw new NotImplementedException();
    }

    public virtual void accept(TaskVisitor v)
	{
		v.visit(this);
	}

    public void Delete() { }
}

