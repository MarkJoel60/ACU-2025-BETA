// Decompiled with JetBrains decompiler
// Type: PX.SM.Alias.CustProject
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM.Alias;

[Serializable]
public class CustProject : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _ProjID;
  protected 
  #nullable disable
  string _Name;
  protected Guid? _ParentID;
  protected string _Description;

  [PXDBGuid(false)]
  [PXDefault]
  [PXUIField(Visibility = PXUIVisibility.Visible)]
  public virtual Guid? ProjID
  {
    get => this._ProjID;
    set => this._ProjID = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Project Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (CustProject.name))]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? ParentID
  {
    get => this._ParentID;
    set => this._ParentID = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  public abstract class projID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CustProject.projID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustProject.name>
  {
  }

  public abstract class parentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CustProject.parentID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustProject.description>
  {
  }
}
