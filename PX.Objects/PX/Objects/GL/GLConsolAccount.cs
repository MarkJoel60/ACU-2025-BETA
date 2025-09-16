// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("GL Consolidation Account")]
[Serializable]
public class GLConsolAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AccountCD;
  protected string _Description;

  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  public virtual string AccountCD
  {
    get => this._AccountCD;
    set => this._AccountCD = value;
  }

  [PXDBString(60, IsUnicode = true)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  public class PK : PrimaryKeyOf<GLConsolAccount>.By<GLConsolAccount.accountCD>
  {
    public static GLConsolAccount Find(PXGraph graph, string accountCD, PKFindOptions options = 0)
    {
      return (GLConsolAccount) PrimaryKeyOf<GLConsolAccount>.By<GLConsolAccount.accountCD>.FindBy(graph, (object) accountCD, options);
    }
  }

  public abstract class accountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolAccount.accountCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolAccount.description>
  {
  }
}
