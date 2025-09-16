// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolBatch
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

[PXHidden]
[Serializable]
public class GLConsolBatch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _BatchNbr;
  protected int? _SetupID;
  protected byte[] _tstamp;

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (Batch.batchNbr))]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBInt]
  [PXDefault]
  public virtual int? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<GLConsolBatch>.By<GLConsolBatch.batchNbr>
  {
    public static GLConsolBatch Find(PXGraph graph, string batchNbr, PKFindOptions options = 0)
    {
      return (GLConsolBatch) PrimaryKeyOf<GLConsolBatch>.By<GLConsolBatch.batchNbr>.FindBy(graph, (object) batchNbr, options);
    }
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolBatch.batchNbr>
  {
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolBatch.setupID>
  {
  }
}
