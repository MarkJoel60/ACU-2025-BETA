// Decompiled with JetBrains decompiler
// Type: PX.SM.Certificate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Certificate")]
public class Certificate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Name;
  protected string _Password;
  protected Guid? _NoteID;

  [PXDBString(50, IsKey = true, InputMask = "")]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Name")]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDB3DesCryphString(500, IsViewDecrypted = false, InputMask = "")]
  [PXUIField(Visibility = PXUIVisibility.Invisible, DisplayName = "Password")]
  public string Password
  {
    get => this._Password;
    set => this._Password = value;
  }

  [PXNote]
  public Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public class PK : PrimaryKeyOf<Certificate>.By<Certificate.name>
  {
    public static Certificate Find(PXGraph graph, string name, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Certificate>.By<Certificate.name>.FindBy(graph, (object) name, options);
    }
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Certificate.name>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Certificate.password>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Certificate.noteID>
  {
  }
}
