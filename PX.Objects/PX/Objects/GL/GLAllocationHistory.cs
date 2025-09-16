// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLAllocationHistory
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

[PXCacheName("GL Allocation History")]
[Serializable]
public class GLAllocationHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _GLAllocationID;
  protected string _Module;
  protected string _BatchNbr;

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string GLAllocationID
  {
    get => this._GLAllocationID;
    set => this._GLAllocationID = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (Batch))]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (Batch))]
  [PXParent(typeof (Select<Batch, Where<Batch.batchNbr, Equal<Current<GLAllocationHistory.batchNbr>>, And<Batch.module, Equal<Current<GLAllocationHistory.module>>>>>))]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  public class PK : 
    PrimaryKeyOf<GLAllocationHistory>.By<GLAllocationHistory.module, GLAllocationHistory.batchNbr>
  {
    public static GLAllocationHistory Find(
      PXGraph graph,
      string module,
      string batchNbr,
      PKFindOptions options = 0)
    {
      return (GLAllocationHistory) PrimaryKeyOf<GLAllocationHistory>.By<GLAllocationHistory.module, GLAllocationHistory.batchNbr>.FindBy(graph, (object) module, (object) batchNbr, options);
    }
  }

  public static class FK
  {
    public class Allocation : 
      PrimaryKeyOf<GLAllocation>.By<GLAllocation.gLAllocationID>.ForeignKeyOf<GLAllocationHistory>.By<GLAllocationHistory.gLAllocationID>
    {
    }

    public class Batch : 
      PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>.ForeignKeyOf<GLAllocationHistory>.By<GLAllocationHistory.module, GLAllocationHistory.batchNbr>
    {
    }
  }

  public abstract class gLAllocationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationHistory.gLAllocationID>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLAllocationHistory.module>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLAllocationHistory.batchNbr>
  {
  }
}
