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

/// <remarks>Cette classe décrit le système général de GTD, avec la boite de reception et les listes de l'utilisateur. On ajoutera surement le calendrier ici.</remarks>
public class GTDSystem : TaskList
{
	public virtual TaskList Inbox
	{
		get;
		set;
	}

	public virtual List<TaskList> Lists
	{
		get;
		set;
	}

	public virtual IEnumerator<Task> GetEnumerator()
	{
		throw new System.NotImplementedException();
	}

	public virtual T accept(GTDModel::TaskVisitor<T> v)
	{
		throw new System.NotImplementedException();
	}

}

