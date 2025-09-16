// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Status_Posting
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Status_Posting : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Status_Posting().ID_LIST, new ID.Status_Posting().TX_LIST)
    {
    }
  }

  public class NothingToPost : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_Posting.NothingToPost>
  {
    public NothingToPost()
      : base("NP")
    {
    }
  }

  public class PendingToPost : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_Posting.PendingToPost>
  {
    public PendingToPost()
      : base("PP")
    {
    }
  }

  public class Posted : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_Posting.Posted>
  {
    public Posted()
      : base("PT")
    {
    }
  }
}
