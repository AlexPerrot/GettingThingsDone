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
using GettingThingsDone.src.model.visitor;

public interface GTDItem 
{
	T accept<T>(GTDVisitor<T> v);

    void Delete();

}

