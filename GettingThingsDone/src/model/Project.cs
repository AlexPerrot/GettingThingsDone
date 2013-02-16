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

/// <remarks>
/// En lisant le bouquin de GTD, j'ai préférer revenir à ce double composite.
/// J'explique : bien qu'un projet semble être une liste de taches, il ne fait que se comporter comme un truc dans lequel y'a des taches et n'est pas une liste de taches au sens GTD.
/// Un projet est en fait une tache qui necessite plus d'une action. Donc projet hérite de tache et gère ses taches avec une liste (au sens structure de données).
/// </remarks>
public class Project : Task
{
    private IList<Task> tasks = new List<Task>();
    public IList<Task> Tasks
    {
        get { return tasks; }
    }

    public String Title { get; set; }
    public String Description { get; set; }
    public Boolean Done { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    private DateTimeOffset creationDate;
    public DateTimeOffset CreationDate { get { return creationDate; } }

    public Project() : this("", "", null, false, DateTimeOffset.Now)
    {

    }

    public Project(string title, string desc, DateTimeOffset? dueDate, Boolean done, DateTimeOffset creationDate)
    {
        this.Title = title;
        this.Description = desc;
        this.DueDate = dueDate;
        this.Done = done;
        this.creationDate = creationDate;
    }

	public virtual T accept<T>(TaskVisitor<T> v)
	{
		return v.visit(this);
	}

}

