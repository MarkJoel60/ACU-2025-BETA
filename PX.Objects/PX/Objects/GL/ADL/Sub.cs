// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ADL.Sub
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.Objects.GL.ADL;

public class Sub : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SubID;
  protected 
  #nullable disable
  string _SubCD;
  protected byte[] _GroupMask;

  [PXDBIdentity]
  [PXUIField]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDefault]
  [SubAccountRaw]
  public virtual string SubCD
  {
    get => this._SubCD;
    set => this._SubCD = value;
  }

  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  public class PK : PrimaryKeyOf<Sub>.By<Sub.subID>
  {
    public static Sub Find(PXGraph graph, int? subID, PKFindOptions options = 0)
    {
      return (Sub) PrimaryKeyOf<Sub>.By<Sub.subID>.FindBy(graph, (object) subID, options);
    }
  }

  public class UK : PrimaryKeyOf<Sub>.By<Sub.subCD>
  {
    public static Sub Find(PXGraph graph, string subCD, PKFindOptions options = 0)
    {
      return (Sub) PrimaryKeyOf<Sub>.By<Sub.subCD>.FindBy(graph, (object) subCD, options);
    }
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Sub.subID>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Sub.subCD>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Sub.groupMask>
  {
  }
}
