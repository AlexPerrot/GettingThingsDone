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

public class StaticList : TaskList
{
	public virtual IEnumerator<GTDItem> GetEnumerator()
	{
		throw new System.NotImplementedException();
	}

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

	public virtual void AddItem(GTDItem item)
	{
		throw new System.NotImplementedException();
	}

	public virtual void RemoveItem(GTDItem item)
	{
		throw new System.NotImplementedException();
	}

	public virtual T accept<T>(TaskVisitor<T> v)
	{
		throw new System.NotImplementedException();
	}

}

