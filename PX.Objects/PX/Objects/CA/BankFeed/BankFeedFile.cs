// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BankFeedFile
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA.BankFeed;

/// <summary>
/// The class contains the information about the file that is selected on the form.
/// </summary>
[PXCacheName("BankFeedFile")]
public class BankFeedFile : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Contains the file name.</summary>
  [PXString(150, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Name")]
  public virtual 
  #nullable disable
  string FileName { get; set; }

  /// <summary>
  /// Contains the date the file was uploaded to the shared folder.
  /// </summary>
  [PXDate]
  [PXUIField(DisplayName = "Upload Date")]
  public virtual DateTime? UploadDate { get; set; }

  public abstract class fileName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedFile.fileName>
  {
  }

  public abstract class uploadDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  BankFeedFile.uploadDate>
  {
  }
}
