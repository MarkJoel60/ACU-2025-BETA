// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TXImportZipFileData
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.TX;

[DebuggerDisplay("{ZipCode}-{CountyName}:[{Plus4PortionOfZipCode}-{Plus4PortionOfZipCode2}]")]
[Serializable]
public class TXImportZipFileData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected 
  #nullable disable
  string _ZipCode;
  protected string _StateCode;
  protected string _CountyName;
  protected int? _Plus4PortionOfZipCode;
  protected int? _Plus4PortionOfZipCode2;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXUIField(DisplayName = "Zip Code")]
  [PXDBString(25)]
  [PXDefault("")]
  public virtual string ZipCode
  {
    get => this._ZipCode;
    set => this._ZipCode = value;
  }

  [PXUIField(DisplayName = "State")]
  [PXDBString(25)]
  [PXDefault("")]
  public virtual string StateCode
  {
    get => this._StateCode;
    set => this._StateCode = value;
  }

  [PXUIField(DisplayName = "Country Name")]
  [PXDBString(25)]
  [PXDefault("")]
  public virtual string CountyName
  {
    get => this._CountyName;
    set => this._CountyName = value;
  }

  [PXUIField(DisplayName = "Zip Plus Min.")]
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? Plus4PortionOfZipCode
  {
    get => this._Plus4PortionOfZipCode;
    set => this._Plus4PortionOfZipCode = value;
  }

  [PXUIField(DisplayName = "Zip Plus Max.")]
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? Plus4PortionOfZipCode2
  {
    get => this._Plus4PortionOfZipCode2;
    set => this._Plus4PortionOfZipCode2 = value;
  }

  public class PK : PrimaryKeyOf<TXImportZipFileData>.By<TXImportZipFileData.recordID>
  {
    public static TXImportZipFileData Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (TXImportZipFileData) PrimaryKeyOf<TXImportZipFileData>.By<TXImportZipFileData.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TXImportZipFileData.recordID>
  {
  }

  public abstract class zipCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportZipFileData.zipCode>
  {
  }

  public abstract class stateCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXImportZipFileData.stateCode>
  {
  }

  public abstract class countyName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXImportZipFileData.countyName>
  {
  }

  public abstract class plus4PortionOfZipCode : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TXImportZipFileData.plus4PortionOfZipCode>
  {
  }

  public abstract class plus4PortionOfZipCode2 : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TXImportZipFileData.plus4PortionOfZipCode2>
  {
  }
}
